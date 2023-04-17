using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
	public float mSpeed = 50f;
	public float collideCD = 0f;
    public int HP = 150;
	public GameObject MyTarget;
	private GameController gameController = null;
	public AudioClip Explode;
    private float spawnTimer = 10f;

    public Slider healthBarSlider;
    private int MaxHP;
    public GameObject BossHP;

    public GameObject BackgroundMusic;
    public GameObject BossMusic;
    public GameObject VictoryScreen;

    public GameObject CheatedScreen;
    public GameObject but;


    public bool Dead = false;

    public GameObject shield;
    public bool Shield = true;

    public GameObject fire;
    public bool Fire = false;

    void Start()
    {
        NewDirection();
        gameController = FindObjectOfType<GameController>();
        MaxHP = HP;
    }

    void Update()
    {
        collideCD -= 1;
        transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;
        GlobalBehavior globalBehavior = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalBehavior>();
        GlobalBehavior.WorldBoundStatus status = globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
        if (status != GlobalBehavior.WorldBoundStatus.Inside && collideCD <= 0)
        {
            if (status == GlobalBehavior.WorldBoundStatus.CollideLeft)
            {
                transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, -180f));
            }
            if (status == GlobalBehavior.WorldBoundStatus.CollideRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 180f));
            }
            if (status == GlobalBehavior.WorldBoundStatus.CollideTop)
            {
                transform.rotation = Quaternion.Euler(0, 0, Random.Range(90f, 270f));
            }
            if (status == GlobalBehavior.WorldBoundStatus.CollideBottom)
            {
                transform.rotation = Quaternion.Euler(0, 0, Random.Range(-90f, 90f));
            }
            if (status == GlobalBehavior.WorldBoundStatus.Outside)
            {
                Destroy(gameObject);
            }
            collideCD = 50;
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            HP = 4;
            GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
            gameController.Cheated = true;
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemies();
            spawnTimer = 10f;
        }
    }

    public void NewDirection()
    {
        Vector2 v = Random.insideUnitCircle;
        transform.up = new Vector3(v.x, v.y, 0.0f);
    }

    public void ReceiveDamage(int damage = 1)
    {
        if (HP != 0)
        {
            HP -= damage;
           
        } if (HP <= 0 && Shield)
        {
            HP = MaxHP;
            shield.SetActive(false);
            Shield = false;
        } if (HP <= 0 && !Shield && !Fire)
        {
            HP = 20;
            mSpeed *= 12;
            fire.SetActive(true);
            Fire = true;
        } if (HP <= 0 && Fire)
        {
            BackgroundMusic.SetActive(true);
            BossMusic.SetActive(false);
            VictoryScreen.SetActive(true);
            if (gameController.Cheated == true)
            {
                CheatedScreen.SetActive(true);
                but.SetActive(true);
            }
            HeroBehaviour hero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBehaviour>();
            hero.PlaySound(Explode);
            Destroy(gameObject);
            BossHP.SetActive(false);
            gameController.BossHP.SetActive(false);
        }
        UpdateHealthBar();

    }

    void SpawnEnemies()
    {
        GameObject hero = GameObject.Find("Hero");
        if (Shield)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject Enemy = Instantiate(Resources.Load("Prefabs/BossSpawns") as GameObject);
                Enemy.GetComponent<EnemyBehaviour>().MyTarget = hero;
                Instantiate(Enemy, transform.position, Quaternion.identity);
            }
        } else
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject Enemy = Instantiate(Resources.Load("Prefabs/BossSpawns (Barrier)") as GameObject);
                Enemy.GetComponent<EnemyBehaviour>().MyTarget = hero;
                Instantiate(Enemy, transform.position, Quaternion.identity);
            }
        }

    }

    void UpdateHealthBar()
    {
        float healthPercentage = (float)HP / MaxHP;
        healthBarSlider.value = healthPercentage;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
	public float mSpeed = 50f;
	public float collideCD = 0f;
	public int HP = 50;
	public GameObject MyTarget;
	private GameController gameController = null;
	public AudioClip Explode;
    private float spawnTimer = 10f;

    public Slider healthBarSlider;
    private float initialHP;
    public GameObject BossHP;

    public GameObject BackgroundMusic;
    public GameObject BossMusic;
    void Start()
    {
        NewDirection();
        gameController = FindObjectOfType<GameController>();
        initialHP = HP;
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
        }
        if (HP <= 0)
        {
            BackgroundMusic.SetActive(true);
            BossMusic.SetActive(false);
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
        for (int i = 0; i < 4; i++)
        {
            GameObject Enemy = Instantiate(Resources.Load("Prefabs/BossSpawns") as GameObject);
            Enemy.GetComponent<EnemyBehaviour>().MyTarget = hero;
            Instantiate(Enemy, transform.position, Quaternion.identity);
        }
    }

    void UpdateHealthBar()
    {
        float healthPercentage = (float)HP / initialHP;
        healthBarSlider.value = healthPercentage;
    }
}


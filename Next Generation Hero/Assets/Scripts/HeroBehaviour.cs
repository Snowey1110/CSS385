using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBehaviour : MonoBehaviour
{

    public int MaxHP = 3;
    public int CurrentHP = 3;
    public float speed = 20f;
    public float HeroRotateSpeed = 100f / 2f; // 90-degrees in 2 seconds
    public bool FollowMousePosition = true;
    public float fireRate = 0.2F;
    public float nextFire = 0;
    public Text HPStats;
    public float tracerFireRate = 1f;
    public float tracerNextFire = 0;
    private AudioSource audioSrc;
    public AudioClip EggSound;
    public AudioClip TracerEggSound;
    public AudioClip Explode;
    public AudioClip Heal;
    public AudioClip GetDamaged;
    public CooldownVisualizer Egg;
    public CooldownVisualizer TracerEgg;
    private GameController gameController = null;

    // Start is called before the first frame update


    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.forward, -HeroRotateSpeed * Time.smoothDeltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.forward, HeroRotateSpeed * Time.smoothDeltaTime);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            FollowMousePosition = !FollowMousePosition;
            if (gameController.MouseMode == true)
            {
                gameController.switchMode();
                gameController.MouseMode = false;
            }
            else
            {
                gameController.switchMode();
                gameController.MouseMode = true;
            }
        }
        Vector3 pos = transform.position;

        if (FollowMousePosition)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f; 
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                pos += ((speed * Time.smoothDeltaTime) * transform.up);
            }

            if (Input.GetKey(KeyCode.S))
            {
                pos -= ((speed * Time.smoothDeltaTime) * transform.up);
            }

        }
        if (Input.GetKey(KeyCode.Space))
        {
            FireEgg();
            
        }
        transform.position = pos;
        if (Input.GetKey(KeyCode.C))
        {
            
            FireTracerEgg();
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            gameController.EnemyDestroyed();
            PlaySound(GetDamaged);
            CurrentHP -= 1;
            HPStats.text = "X" + CurrentHP;
            if (CurrentHP == 0)
            {
                PlaySound(Explode);
                Destroy(gameObject);
                gameController.gameOver();
            }
            
        }
        if (collision.CompareTag("Heart"))
        {
            Destroy(collision.gameObject);
            if (CurrentHP < MaxHP)
            {
                PlaySound(Heal);
                CurrentHP += 1;
                HPStats.text = "X" + CurrentHP;
            }
        }
        
    }

    public void FireEgg()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            PlaySound(EggSound);
            GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject);
            e.transform.localPosition = transform.localPosition;
            e.transform.rotation = transform.rotation;
            gameController.addEgg();
            Egg.StartCooldown();
        }
        
    }

    public void FireTracerEgg()
    {
        if (Time.time > tracerNextFire)
        {
            tracerNextFire = Time.time + tracerFireRate;
            PlaySound(TracerEggSound);
            GameObject b = Instantiate(Resources.Load("Prefabs/TracerEgg") as GameObject);
            b.transform.localPosition = transform.localPosition;
            b.transform.rotation = transform.rotation;
            TracerEgg.StartCooldown();
        }
        
    }
    public void PlaySound(AudioClip s)
    {
        audioSrc.PlayOneShot(s);
    }




}

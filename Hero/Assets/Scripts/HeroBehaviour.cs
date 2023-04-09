using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBehaviour : MonoBehaviour
{

    

    public float speed = 20f;
    public float HeroRotateSpeed = 100f / 2f; // 90-degrees in 2 seconds
    public bool FollowMousePosition = true;
    public float fireRate = 0.2F;
    public float nextFire = 0;
    // Start is called before the first frame update



    private GameController gameController = null;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
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
            } else
            {
                gameController.switchMode();
                gameController.MouseMode = true;
            }
        }
        Vector3 pos = transform.position;

        if (FollowMousePosition)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;  // <-- this is VERY IMPORTANT!
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
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            // Prefab MUST BE locaed in Resources/Prefab folder!
            nextFire = Time.time + fireRate;
            GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject); 
            e.transform.localPosition = transform.localPosition;
            e.transform.rotation = transform.rotation;
            gameController.addEgg();
        }
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            gameController.EnemyDestroyed();
        }
        
    }




}

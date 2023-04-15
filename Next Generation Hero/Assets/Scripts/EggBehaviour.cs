using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour
{
    public const float kEggSpeed = 40f;
    private GameController gameController = null;
    // public const int kLifeTime = 1000; // Alife for this number of cycles


    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);
        GlobalBehavior globalBehavior = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalBehavior>();
        GlobalBehavior.WorldBoundStatus status = globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);

        if (status != GlobalBehavior.WorldBoundStatus.Inside)
        {
            Destroy(gameObject);
            gameController.removeEgg();
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            EnemyBehaviour enemy = collision.GetComponent<EnemyBehaviour>();
            enemy.ReceiveDamage();
            gameController.removeEgg();
        }
        if (collision.CompareTag("Way Point"))
        {
            Destroy(gameObject);
            WayPointBehaviour waypoint = collision.GetComponent<WayPointBehaviour>();
            waypoint.ReceiveDamage();
            gameController.removeEgg();
        }
        if (collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
            BossBehaviour Boss = collision.GetComponent<BossBehaviour>();
            Boss.ReceiveDamage();
            gameController.removeEgg();
        }


    }


}

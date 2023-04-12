using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerEggBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 100f;
    private GameObject target;
    private Vector3 targetDirection;
    void Start()
    {
        // Find the closest enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = enemy;
            }
        }

        // Set the target direction to the closest enemy
        if (target != null)
        {
            targetDirection = (target.transform.position - transform.position).normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Update the target direction as the target moves
            targetDirection = (target.transform.position - transform.position).normalized;

            // Rotate the bullet to face the target
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            // Move the bullet towards the target direction
            transform.position += targetDirection * speed * Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyBehaviour enemy = collision.GetComponent<EnemyBehaviour>();
            enemy.ReceiveDamage(5);
            Destroy(gameObject);
            
        }
    }
}
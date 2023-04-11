using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.0f;
    private GameObject currentTarget;
    public float reachedDistanceThreshold = 0.1f;
    private List<GameObject> visitedPaths = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            currentTarget = FindClosestBlock("Enemy Path");
        }
        else
        {
            float distanceToCurrentTarget = Vector2.Distance(transform.position, currentTarget.transform.position);
            if (distanceToCurrentTarget <= reachedDistanceThreshold)
            {
                if (currentTarget.CompareTag("Base")) // Stop when reaching the "Base" object
                {
                    enabled = false;
                    return;
                }

                visitedPaths.Add(currentTarget);
                currentTarget = FindClosestBlock("Enemy Path");
            }

            if (currentTarget != null) // Add null check before calling MoveTowardsPath
            {
                MoveTowardsPath(currentTarget);
            }
        }

    }

    public GameObject FindClosestBlock(string tag)
    {
        GameObject[] enemyPathObjects;
        enemyPathObjects = GameObject.FindGameObjectsWithTag(tag);

        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject enemyPath in enemyPathObjects)
        {
            if (visitedPaths.Contains(enemyPath))
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, enemyPath.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = enemyPath;
            }
        }

        return closestObject;
    }

    public void MoveTowardsPath(GameObject target)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    
}

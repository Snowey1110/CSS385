using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
	// Start is called before the first frame update
	public float minSpeed = 50;
	public float maxSpeed = 100;
	public float changeDirectionInterval = 1f;

	private Vector3 targetDirection;
	private float currentSpeed;

	void Start()
    {
		InvokeRepeating(nameof(ChangeDirection), 0f, changeDirectionInterval);
	}

    // Update is called once per frame
    void Update()
    {
        GlobalBehavior globalBehavior = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalBehavior>();

        GlobalBehavior.WorldBoundStatus status = globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);

        transform.position += targetDirection * currentSpeed * Time.deltaTime;
        if (status == GlobalBehavior.WorldBoundStatus.Outside)
        {
            Destroy(gameObject);
        }
	}

    private void ChangeDirection()
    {
        // Generate a random direction
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
   

        targetDirection = new Vector2(x, y).normalized;

        // Generate a random speed
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
	public float mSpeed = 100f;
	public float collideCD = 0f;
	public int HP = 3;
	private GameController gameController = null;
	public GameObject Hero;
	public Text mEnemyCountText = null;

	// Use this for initialization
	void Start()
	{
		NewDirection();
		gameController = FindObjectOfType<GameController>();

	}

	// Update is called once per frame
	void Update()
	{
		collideCD -= 1;

		transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;
		GlobalBehavior globalBehavior = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalBehavior>();

		GlobalBehavior.WorldBoundStatus status = globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);

		if (status != GlobalBehavior.WorldBoundStatus.Inside && collideCD <= 0)
		{
			// Debug.Log("collided position: " + status);
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
				GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
				gameController.SpawnPlane();
			}
			collideCD = 100;
		}

	}

	// New direction will be something completely random!
	private void NewDirection()
	{
		Vector2 v = Random.insideUnitCircle;
		transform.up = new Vector3(v.x, v.y, 0.0f);
	}

	public void ReceiveDamage()
    {
        if (HP != 0)
        {
            HP -= 1;
			color();
        }
		else if (HP == 0)
        {
			Destroy(gameObject);
			gameController.EnemyDestroyed();
		}
    }

	private void color()
    {
		SpriteRenderer s = GetComponent<SpriteRenderer>();
		Color c = s.color;
		const float delta = 0.8f;
		c.a *= delta;
		s.color = c;

    }

}

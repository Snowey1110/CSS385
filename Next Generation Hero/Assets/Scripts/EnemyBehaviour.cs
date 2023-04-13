using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
	public float mSpeed = 100f;
	public float collideCD = 0f;
	public int HP = 4;
	public float mTurnRate = 2;  // multiplied with elapseTime  range: 2 to 60
	public GameObject MyTarget;
	private GameController gameController = null;
	public GameObject Hero;
	public GameObject FloatHeart;
	public AudioClip Explode;

	public bool randomWaypoint = false;

	// Use this for initialization
	void Start()
	{
		NewDirection();
		gameController = FindObjectOfType<GameController>();
		findRandomWayPoint();

	}

	// Update is called once per frame
	void Update()
	{
		collideCD -= 1;

		PointAtPosition(MyTarget.transform.position, mTurnRate * Time.smoothDeltaTime);
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
			collideCD = 50;
		}

	}

	// New direction will be something completely random!
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
			color();
        }
		if (HP <= 0)
        {
			int rand = Random.Range(0, 3);
			if (rand == 1)
            {
				Instantiate(FloatHeart, transform.position, Quaternion.identity);
			}
			HeroBehaviour hero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBehaviour>();
			hero.PlaySound(Explode);
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

	// Code stolen from exercise 
	private void PointAtPosition(Vector3 p, float r)
	{
		Vector3 v = p - transform.position;
		v.z = 0;
		transform.up = Vector3.LerpUnclamped(transform.up, v, r);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("Way Point"))
        {
			findRandomWayPoint();

		}
    }

	public void findRandomWayPoint()
    {
		GameObject[] Target = GameObject.FindGameObjectsWithTag("Way Point");
		int rand = Random.Range(0, 6);
		Debug.Log(Target.Length);
		Debug.Log("Looking for " + rand);
		GameObject NewTarget = Target[rand];
		
		if (NewTarget == null)
        {
			MyTarget = NewTarget;

		} else if (NewTarget == MyTarget)
		{
			findRandomWayPoint();
        } else
        {
			MyTarget = NewTarget;
		}
	}
}

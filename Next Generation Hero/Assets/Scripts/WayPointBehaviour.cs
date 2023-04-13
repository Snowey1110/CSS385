using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPointBehaviour : MonoBehaviour
{
	public int HP = 4;
	public string WayPointLetter;
	private GameController gameController = null;
	public GameObject Hero;
	public GameObject FloatHeart;
	public AudioClip Explode;



	// Use this for initialization
	void Start()
	{
		gameController = FindObjectOfType<GameController>();
		
	}

	// Update is called once per frame
	void Update()
	{



	}

	// New direction will be something completely random!

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
			resetcolor();
			respawn();
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

	private void resetcolor()
    {
		SpriteRenderer s = GetComponent<SpriteRenderer>();
		Color c = s.color;
		c.a = 1;
		s.color = c;
	}

	public string getLetter() {return WayPointLetter;}
	private void respawn()
    {
		HP = 4;

		Vector2 temp = transform.position;
		Vector2 temp2 = transform.position;
		temp.x = temp.x + Random.Range(-15f, 15f);
		temp.y = temp.y + Random.Range(-15f, 15f);
		transform.position = temp;
		GlobalBehavior globalBehavior = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalBehavior>();

		GlobalBehavior.WorldBoundStatus status = globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
		if (status != GlobalBehavior.WorldBoundStatus.Inside)
        {
			transform.position = temp2;
			respawn();
        }



	}

	

}

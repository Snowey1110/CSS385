using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
	public int HP = 1;



	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		

	}



	public void ReceiveDamage(int damage = 1)
    {

        if (HP != 0)
        {
            HP -= damage;
        }
		if (HP <= 0)
        {
			Destroy(gameObject);
		}
    }

	// Make it fade out 
	private void color()
    {
		SpriteRenderer s = GetComponent<SpriteRenderer>();
		Color c = s.color;
		const float delta = 0.8f;
		c.a *= delta;
		s.color = c;

    }

}

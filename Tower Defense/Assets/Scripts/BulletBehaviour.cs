using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public float damage;
    public int hits;
    int hitsDone;

	void Start ()
    {
        hitsDone = 0;
	}
	
	void Update ()
    {
        if (hitsDone >= hits)
            Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            hitsDone++;
            collision.GetComponent<EnemyBehaviour>().hp -= damage;
        }
    }
}

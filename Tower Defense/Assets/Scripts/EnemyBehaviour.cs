using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {

    public float speed;
    public float hp;
    Vector2[] path;
    public int currentPoint;
    public float totalDistanceTraveled;
    public Slider sl;

	void Start ()
    {
        path = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>().GetPath();
        currentPoint = 0;
        sl = GetComponentInChildren<Slider>();
        sl.maxValue = hp;
        sl.value = hp;
	}
	
	void Update ()
    {
		if(amICloseEnough())
        {
            currentPoint++;
        }
        if (currentPoint >= path.Length)
        {
            Debug.Log("Umieram, tracisz HP");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>().health -= 1;
            Death();
            return;
        }
        totalDistanceTraveled += Vector2.MoveTowards(transform.position, path[currentPoint], speed * Time.deltaTime).magnitude;
        transform.position = Vector2.MoveTowards(transform.position, path[currentPoint], speed * Time.deltaTime);
        if (hp <= 0)
        {
            Death();
        }
        sl.value = hp;
	}

    public void Death()
    {
        Destroy(gameObject);
    }

    public void Death(GameObject child)
    {
        Instantiate(child, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    bool amICloseEnough()
    {
        bool r = false;
        if(currentPoint < path.Length && (transform.position -  (new Vector3(path[currentPoint].x, path[currentPoint].y, 0))).magnitude < 0.001f)
        {
            r = true;
        }
        return r;
    }
}

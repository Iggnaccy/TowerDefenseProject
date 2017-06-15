using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {

    public GameObject Path;
    Vector2[] path;
    public int health;
    public int cash;
    public float maxX, maxY;
    public bool amIPlacingATower;

	void Awake ()
    {
        Path = GameObject.FindGameObjectWithTag("Path");
        path = Path.GetComponent<EdgeCollider2D>().points;
	}

    public Vector2[] GetPath()
    {
        return path;
    }
}

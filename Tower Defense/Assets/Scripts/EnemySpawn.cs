using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    Vector2 spawn;
    public GameObject enemy;

    private void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Path").GetComponent<EdgeCollider2D>().points[0];
    }

    public void Spawn()
    {
        Instantiate(enemy, spawn, Quaternion.identity);
    }
}

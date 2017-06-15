using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Coś próbowało się wydostać!");
        if(collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}

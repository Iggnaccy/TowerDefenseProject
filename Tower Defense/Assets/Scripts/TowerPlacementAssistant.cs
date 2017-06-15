using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementAssistant : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Tower")
        {
            collision.GetComponent<TowerPlacing>().overlayingObjects--;
            Debug.Log("Pozwolenie na budowę");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tower")
        {
            collision.GetComponent<TowerPlacing>().overlayingObjects++;
            Debug.Log("Zakaz budowy");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTowerPlacement : MonoBehaviour
{
    public GameObject tower;
    public int cost;
    GameControllerScript gc;

    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    public void StartPlacement()
    {
        if(!gc.amIPlacingATower && gc.cash >= cost)
        {
            gc.amIPlacingATower = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Trafiłem " + hit.transform.name + ", tworzę obiekt");
                Instantiate(tower, hit.point, Quaternion.identity);
            }
        }
    }
}

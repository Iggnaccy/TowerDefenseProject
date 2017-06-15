using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterProperties : MonoBehaviour
{
    TowerPropertiesPanelControl tp;

    private void Start()
    {
        tp = GameObject.FindGameObjectWithTag("Properties").GetComponent<TowerPropertiesPanelControl>();
    }

    public void ChangeActive()
    {
        if (tp.active != null)
        {
            tp.active.GetComponent<SpriteRenderer>().color = Color.yellow;
            tp.active.GetComponent<LineRenderer>().enabled = false;
        }
        Debug.Log("Zmieniam");
        tp.active = GetComponentInChildren<BasicTower>();
        tp.active.GetComponent<SpriteRenderer>().color = Color.red;
        tp.active.GetComponent<LineRenderer>().enabled = true;
        tp.bestToggle.isOn = tp.active.shootFirst;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPropertiesPanelControl : MonoBehaviour
{
    public BasicTower active;
    Text towerName, damage, attackspeed, range, shootType;
    public Toggle bestToggle;
    public float sellMult;
    GameControllerScript gc;

    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        Text[] texts = GetComponentsInChildren<Text>();
        towerName = texts[0];
        damage = texts[2];
        attackspeed = texts[4];
        range = texts[6];
        shootType = texts[7];
        bestToggle = GetComponentInChildren<Toggle>();
    }

    private void Update()
    {
        if (active == null)
        {
            towerName.text = "Tower Details";
            damage.text = "x";
            attackspeed.text = "y";
            range.text = "z";
            shootType.text = "Shoot type";
            return;
        }
        towerName.text = active.towerName;
        damage.text = active.damage.ToString();
        attackspeed.text = active.attackSpeed.ToString();
        range.text = active.range.ToString();
        switch (active.priorityType)
        {
            case 0:
                {
                    shootType.text = "Closest to exit";
                    break;
                }
            case 1:
                {
                    shootType.text = "Strongest";
                    break;
                }
            case 2:
                {
                    shootType.text = "Closest to tower";
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void ChangeShootType(int value)
    {
        active.ChangePriority(value);
    }

    public void ChangeBest(bool val)
    {
        active.ChangePriorityFirst(val);
    }

    public void Sell()
    {
        if (active == null) return;
        gc.cash += (int)(active.totalCost * sellMult);
        Destroy(active.transform.parent.gameObject);
    }

    public void Deselect()
    {
        active.GetComponent<SpriteRenderer>().color = Color.yellow;
        active.GetComponent<LineRenderer>().enabled = false;
        active = null;
    }
}

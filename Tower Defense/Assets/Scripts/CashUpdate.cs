using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashUpdate : MonoBehaviour
{
    public Text text;
    GameControllerScript gc;

    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    private void Update()
    {
        text.text = gc.cash.ToString();
    }
}

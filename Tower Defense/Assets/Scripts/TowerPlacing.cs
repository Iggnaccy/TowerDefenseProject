using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacing : MonoBehaviour
{
    public int overlayingObjects;
    public int cost;
    BasicTower me;
    GameControllerScript gc;
    public Transform parent;
    public GameObject canvas;

    private void Start()
    {
        me = GetComponent<BasicTower>();
        overlayingObjects = 0;
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        DoRenderer();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gc.amIPlacingATower = false;
            Destroy(parent.gameObject);
            return;
        }
        if (overlayingObjects == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
        }
        if (overlayingObjects == 0 && Input.GetMouseButtonUp(0))
        {
            GetComponent<Collider2D>().isTrigger = true;
            me.enabled = true;
            enabled = false;
            gc.cash -= cost;
            GetComponent<BasicTower>().totalCost += cost;
            gc.amIPlacingATower = false;
            LineRenderer lr = GetComponent<LineRenderer>();
            lr.enabled = false;
            canvas.SetActive(true);
            return;
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                parent.position = hit.point;
                KeepInside();
            }
        }
    }

    private void KeepInside()
    {
        Vector2 vec = new Vector2();
        vec.x = Mathf.Clamp(parent.position.x, -gc.maxX, gc.maxX);
        vec.y = Mathf.Clamp(parent.position.y, -gc.maxY, gc.maxY);
        parent.position = vec;
    }

    [Range(3, 256)]
    public int numSegments = 128;

    public void DoRenderer()
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startColor = lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = lineRenderer.endWidth = 0.025f;
        lineRenderer.positionCount = (numSegments + 1);
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (float)(2.0 * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = me.range * Mathf.Cos(theta);
            float z = me.range * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, z, 0);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}

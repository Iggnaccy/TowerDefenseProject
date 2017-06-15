using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour {

    public string towerName;
    public float range;
    public float attackSpeed;
    public float damage;
    public float bulletSpeed;
    public int bulletHits;
    Collider2D[] enemies;
    Collider2D currentEnemy;
    public GameObject bullet;
    public bool shootFirst;
    public int totalCost;
    public int priorityType;
    bool canAttack;

    [Range(3, 256)]
    public int numSegments = 128;



    void Start()
    {
        StartCoroutine("Attack");
        StartCoroutine("AttackSpeed");
        DoRenderer();
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void ChangePriority(int value)
    {
        priorityType += value;
        priorityType %= 3;
    }

    public void ChangePriorityFirst(bool first)
    {
        shootFirst = first;
    }

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
            float x = range * Mathf.Cos(theta);
            float z = range * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, z, 0);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
    IEnumerator Attack()
    {
        while (true)
        {
            if (!canAttack)
            {
                yield return null;
                continue;
            }
            enemies = Physics2D.OverlapCircleAll(transform.position, 2 * range);
            if (enemies.Length == 0)
            {
                yield return null;
                continue;
            }
            currentEnemy = enemies[0];
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.tag == "Enemy")
                {
                    if (currentEnemy.tag != "Enemy")
                    {
                        currentEnemy = enemy;
                    }
                    else
                    {
                        bool check = false;
                        if (priorityType == 0)
                        {
                            check = enemy.GetComponent<EnemyBehaviour>().totalDistanceTraveled < currentEnemy.GetComponent<EnemyBehaviour>().totalDistanceTraveled ^ shootFirst;
                        }
                        if (priorityType == 1)
                        {
                            check = enemy.GetComponent<EnemyBehaviour>().hp < currentEnemy.GetComponent<EnemyBehaviour>().hp ^ shootFirst;
                        }
                        if (priorityType == 2)
                        {
                            float d1 = (transform.position - enemy.transform.position).magnitude;
                            float d2 = (transform.position - currentEnemy.transform.position).magnitude;
                            check = (d1 > d2) ^ shootFirst;
                        }

                        if (check)
                        {
                            currentEnemy = enemy;
                        }
                    }
                }
            }
            if (currentEnemy == null || currentEnemy.tag != "Enemy")
            {
                yield return null;
            }
            else
            {
                Debug.Log("Strzelam!");
                Rigidbody2D temp = ((Instantiate(bullet, transform.position, Quaternion.identity)) as GameObject).GetComponent<Rigidbody2D>();
                temp.velocity = new Vector2(currentEnemy.transform.position.x - transform.position.x, currentEnemy.transform.position.y - transform.position.y).normalized * bulletSpeed * Time.fixedDeltaTime;
                temp.GetComponent<BulletBehaviour>().damage = damage;
                temp.GetComponent<BulletBehaviour>().hits = bulletHits;
                canAttack = false;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    IEnumerator AttackSpeed()
    {
        while (true)
        {
            if (canAttack)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(1f / attackSpeed);
                canAttack = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemyHealth;
    [SerializeField]
    private float movementSpeed;
    
    private int killReward;
    private int damage;

    private GameObject targetTile;

    private void Start()
    {
        initiliazedEnemy();
    }

    private void initiliazedEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    private void takeDamage(float amount)
    {
        enemyHealth -= amount;

        if (enemyHealth <= 0) 
        {
            die();
        }
    }

    private void die()
    {
        Destroy(transform.gameObject);
    }

    private void moveEnemy()
    {
        Debug.Log(targetTile);
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, movementSpeed * Time.deltaTime);
    }

    private void checkPosition()
    {
        if (targetTile != null && targetTile != MapGenerator.endTile) 
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;

            if (distance < 0.001f) 
            {
                int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile);

                targetTile = MapGenerator.pathTiles[currentIndex + 1];
            }
        }
    }

    private void Update()
    {
        checkPosition();
        moveEnemy();
    }


}

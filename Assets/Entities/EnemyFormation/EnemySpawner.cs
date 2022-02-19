using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float height = 5f;
    public float width = 10f;
    public float speed = 5.0f;
    public float spawnDelay = 0.5f;

    private bool movingRight = false;
    private float xmax, xmin;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Start is called before the first frame update
    void Start()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmin = leftBoundry.x;
        xmax = rightBoundry.x;
        SpawnEnemies();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        /*if (leftEdgeOfFormation < xmin || rightEdgeOfFormation > xmax)
        {
            movingRight = !movingRight;
        } */
        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }

        if(AllMembersDead())
        {
            SpawnEnemies();
            Debug.Log("Empty Formation");
        }    
    }
    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount>0)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    Transform NextFreePosition()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;

        
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            foreach (Transform child in transform)
            {
                GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = freePosition;
            }
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    [SerializeField] private GameObject[] WayPoints;
    [SerializeField] private float speed = 2f;
    private SpriteRenderer sprite;
    private int currentWayPointIndex = 0;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Vector2.Distance(WayPoints[currentWayPointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWayPointIndex++;
            if(currentWayPointIndex >= WayPoints.Length)
            {
                currentWayPointIndex = 0;
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, WayPoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
}

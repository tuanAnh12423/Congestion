using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private float speed = 2;
    private SpriteRenderer sprite;
    private int currentWayPointIndex = 0;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector2.Distance(wayPoints[currentWayPointIndex].transform.position, transform.position) < 0.01f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= wayPoints.Length)
            {
                currentWayPointIndex = 0;
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPointIndex].
            transform.position, Time.deltaTime * speed);
    }
}

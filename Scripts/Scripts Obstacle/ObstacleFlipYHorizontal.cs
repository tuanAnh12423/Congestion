using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFlipYHorizontal : MonoBehaviour
{
    [SerializeField] private GameObject[] Waypoints;
    [SerializeField] private float speed = 4f;
    private SpriteRenderer sprite;
    private int currentWayPointIndex = 0;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Vector2.Distance(Waypoints[currentWayPointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWayPointIndex++;
            if(currentWayPointIndex >= Waypoints.Length)
            {
                currentWayPointIndex = 0;
                sprite.flipY = true;
            }
            else
            {
                sprite.flipY = false;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, Waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
}

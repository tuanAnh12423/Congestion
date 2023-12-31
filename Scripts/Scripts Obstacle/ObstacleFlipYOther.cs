using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFlipYOther : MonoBehaviour
{
    [SerializeField] private GameObject[] Waypoints;
    [SerializeField] private float speed = 5f;
    private SpriteRenderer sprite;
    private int currentWaypointIndex = 0;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Vector2.Distance(Waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= Waypoints.Length)
            {
                currentWaypointIndex = 0;
                sprite.flipY = true;
            }
            else
            {
                sprite.flipY = false;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, Waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}

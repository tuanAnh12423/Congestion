using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float waitTime = 2f; // Thời gian nghỉ khi đến điểm xuất phát

    private bool movingToB = true;
    private Animator animator;
    private SpriteRenderer sprite;
    private float waitTimer = 0f;
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        UpdateAnimatorState();
    }
    void Update()
    {
        MoveBetweenPoints();
    }
    void MoveBetweenPoints()
    {
        Transform targetPoint = movingToB ? pointB : pointA;

        Vector3 movementDirection = targetPoint.position - transform.position;
        float movementX = movementDirection.x;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, movementSpeed * Time.deltaTime);

        if (Mathf.Abs(movementX) > 0.01f)
        {
            if (movementX > 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            if (waitTimer <= 0f)
            {
                movingToB = !movingToB;
                waitTimer = waitTime;
                UpdateAnimatorState();
            }
            else
            {
                waitTimer -= Time.deltaTime;
                animator.SetBool("IsMoving", false); // Dừng trạng thái di chuyển
            }
        }
        else
        {
            animator.SetBool("IsMoving", true); // Kích hoạt trạng thái di chuyển
        }
    }
    void UpdateAnimatorState()
    {
        animator.SetBool("IsMoving", true);
    }
}

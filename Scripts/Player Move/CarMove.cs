using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float rotationSpeed = 90f;
    private Rigidbody2D rb;

    private float _horizontal;
    private float _vertical;
    private Vector2 direction = Vector2.up;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
        RotateCar();
    }
    private void Move()
    {
        _vertical = Input.GetAxisRaw("Vertical");
        if (_vertical != 0)
        {
            Vector2 movement = direction * _vertical * speed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
    private void RotateCar()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        if(_horizontal != 0)
        {
            float rotationAmount = -_horizontal * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationAmount);
            direction = Quaternion.Euler(0, 0, rotationAmount) * direction;
        }
    }
}

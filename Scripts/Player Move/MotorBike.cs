using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using Unity.VisualScripting;

public class MotorBike : MonoBehaviour
{
    [SerializeField] private float[] speedByGear = { 0, 2, 4, 6f };
    [SerializeField] private TMP_Text gearText;
    [SerializeField] private float decelerationRate = 5f; // Tốc độ giảm khi không áp lực lên phím
    [SerializeField] private float brakeForce = 100f; // Lực phanh
    [SerializeField] private AudioSource BikeIdle;
    private bool isIncreasing = true;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private enum Gear
    {
        Neutral = 0,
        First = 1,
        Second = 2,
        Third = 3
    }
    private Gear currentGear = Gear.Neutral; // Hộp số hiện tại
    private float _horizontal;
    private float _vertical;
    private bool isBraking = false; // Trạng thái phanh
    private bool isMoving = false;
    private movementState verticalState = movementState.side;
    private enum movementState { side, up, down };
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        BikeIdle.Play();
    }
    private void Update()
    {
        UpdateState();
        UpdateGearText(); // Cập nhật hiển thị hộp số
        AdjustGear(); // Tăng hoặc giảm hộp số
        Brake();
        Move();
    }

    private void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(_horizontal, _vertical) * GetSpeedByGear(currentGear); //Tính toán Vector di chuyển dựa
                                                                                              // Dựa trên vận tốc của xe
                                                                                              // trong mỗi hộp số
        
        if (_horizontal == 0 && _vertical == 0)
        {
            Decelerate();
        }
        else
        {
            rb.velocity = movement;
        }
    }
    private void Decelerate()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * decelerationRate);
    }

    private void UpdateState()
    {
        movementState state;

        if (_horizontal != 0)
        {
            state = movementState.side;
            sprite.flipX = (_horizontal < 0);
            verticalState = movementState.side;
        }
        else
        {
            if (_vertical > 0)
            {
                state = movementState.up;
                verticalState = movementState.up;
            }
            else if (_vertical < 0)
            {
                state = movementState.down;
                verticalState = movementState.down;
            }
            else
            {
                state = verticalState;
            }
        }
        anim.SetInteger("state", (int)state);
    }

    private void UpdateGearText()
    {
        gearText.text = "Gear: " + ((int)currentGear).ToString(); // Hiển thị hộp số đang chọn
    }

    private void AdjustGear()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isIncreasing)
            {
                SetNextGear();
            }
            else
            {
                SetPreviousGear();
            }
        }
    }
    private void SetNextGear()
    {
        if (currentGear < Gear.Third )
        {
            currentGear++;
        }
        else
        {
            isIncreasing = false;
            currentGear--;
        }
    }
    private void SetPreviousGear()
    {
        if(currentGear > Gear.Neutral)
        {
            currentGear--;
        }
        else
        {
            isIncreasing = true;
            currentGear++;
        }
    }
    private void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isBraking = true; // Đang phanh
        }
        else
        {
            isBraking = false; // Không phanh
        }

        // Nếu đang phanh, giảm dần tốc độ xe
        if (isBraking)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * brakeForce);
        }
    }
    private float GetSpeedByGear(Gear gear)
    {
        return (int)gear * speedByGear[(int)gear];
    }
}

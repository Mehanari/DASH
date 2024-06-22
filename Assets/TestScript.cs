using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovementController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float deceleration = 1f;
    private int _direction = 0; //If 0, then no movement. If 1, then right. If -1, then left.
    private float _speedXOnStop = 0; //Speed on x axis when the player stops moving. Needed for smooth deceleration.
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnHorizontalMovement(InputValue value)
    {
        _direction = (int)value.Get<float>();
        if (_direction == 0)
        {
            _speedXOnStop = _rigidbody2D.velocity.x;
        }
    }

    void Update()
    {
        if (_direction != 0)
        {
            float targetSpeed = maxSpeed * _direction;
            ChangeSpeedX(targetSpeed, acceleration);
        }
        else
        {
            ChangeSpeedX(0, deceleration);
        }
    }

    private void ChangeSpeedX(float targetSpeed, float acceleration)
    {
        float speedX = _rigidbody2D.velocity.x;
        if (speedX < targetSpeed)
        {
            speedX += acceleration * Time.deltaTime;
            if (speedX > targetSpeed)
            {
                speedX = targetSpeed;
            }
        }
        else if (speedX > targetSpeed)
        {
            speedX -= acceleration * Time.deltaTime;
            if (speedX < targetSpeed)
            {
                speedX = targetSpeed;
            }
        }
        _rigidbody2D.velocity = new Vector2(speedX, _rigidbody2D.velocity.y);
    }
}

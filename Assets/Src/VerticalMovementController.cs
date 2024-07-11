using Src.GroundChecking;
using Src.MetronomeLogic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class VerticalMovementController : MonoBehaviour
{
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private float minimumMetronomeHitResult = 0.05f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float gravityAcceleration = 10f;
    private readonly float _jumpTime = 0.001f;
    private float _jumpElapsedTime;
    private bool _isJumping;
    private int _gravityDirection = 1;
    private Rigidbody2D _rigidbody2D;
    private Metronome _metronome;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Init(Metronome metronome)
    {
        _metronome = metronome;
    }

    private void FixedUpdate()
    {
        var velocity = _rigidbody2D.velocity;
        if (groundChecker.IsGrounded() && !_isJumping)
        {
            velocity.y = 0f;
        }
        else
        {
            if (_isJumping)
            {
                _jumpElapsedTime += Time.fixedDeltaTime;
                if (_jumpElapsedTime >= _jumpTime)
                {
                    _isJumping = false;
                    _jumpElapsedTime = 0f;
                }
            }
            velocity.y -= _gravityDirection * gravityAcceleration * Time.fixedDeltaTime;
        }
        _rigidbody2D.velocity = velocity;
    }

    private void OnJump(InputValue value)
    {
        if(!JumpCondition(value)) return;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _gravityDirection * jumpVelocity);
        _gravityDirection = -_gravityDirection;
        _isJumping = true;
        _jumpElapsedTime = 0f;
    }

    private bool JumpCondition(InputValue value)
    {
        if (!value.isPressed) return false;
        if (!groundChecker.IsGrounded()) return false;
        var metronomeHitResult = _metronome.TryHit();
        if (metronomeHitResult < minimumMetronomeHitResult) return false;
        return true;
    }
}

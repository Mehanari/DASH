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
        if (!groundChecker.IsGrounded())
        {
            velocity.y -= gravityAcceleration * Time.fixedDeltaTime;
        }
        else if (velocity.y <= 0f)
        {
            velocity.y = 0f;
        }
        _rigidbody2D.velocity = velocity;
    }

    private void OnJump(InputValue value)
    {
        if (!value.isPressed) return;
        if (!groundChecker.IsGrounded()) return;
        var metronomeHitResult = _metronome.TryHit();
        if (metronomeHitResult > minimumMetronomeHitResult)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
        }
    }
}

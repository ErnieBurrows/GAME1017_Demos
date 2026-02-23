using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IState currentState;

    [Header("Tuning")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Refs")]
    public Rigidbody rb;

    private void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ChangeState(new IdleState());
    }

    private void Update()
    {
        currentState?.Tick(this);

        if (Input.GetKeyDown(KeyCode.Space))
            ChangeState(new JumpState());

        float x = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(x) > 0.01f && !(currentState is MoveState))
            ChangeState(new MoveState());

        if (Mathf.Abs(x) <= 0.01f && (currentState is MoveState))
            ChangeState(new IdleState());
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }
}

public interface IState
{
    void Enter(PlayerStateMachine player);
    void Tick(PlayerStateMachine player);
    void Exit(PlayerStateMachine player);
}

public class IdleState : IState
{
    public void Enter(PlayerStateMachine player) => Debug.Log("State: Idle");
    public void Tick(PlayerStateMachine player) { /* do nothing */ }
    public void Exit(PlayerStateMachine player) { }
}

public class MoveState : IState
{
    public void Enter(PlayerStateMachine player) => Debug.Log("State: Move");

    public void Tick(PlayerStateMachine player)
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 vel = player.rb.linearVelocity;
        vel.x = x * player.moveSpeed;
        player.rb.linearVelocity = vel;
    }

    public void Exit(PlayerStateMachine player) { }
}

public class JumpState : IState
{
    public void Enter(PlayerStateMachine player)
    {
        Debug.Log("State: Jump");
        Vector3 vel = player.rb.linearVelocity;
        vel.y = player.jumpForce;
        player.rb.linearVelocity = vel;
    }

    public void Tick(PlayerStateMachine player)
    {
        // When going down and near ground, return to Idle (super simple)
        if (player.rb.linearVelocity.y <= 0.01f && player.transform.position.y <= 0.6f)
            player.ChangeState(new IdleState());
    }

    public void Exit(PlayerStateMachine player) { }
}
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private Rigidbody2D _rb;
    private Vector2 _inputVec;
    public Vector2 InputVec => _inputVec;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnMove(InputValue value)
    {
        _inputVec = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 destVec = _inputVec * (moveSpeed * Time.deltaTime);
        _rb.MovePosition(_rb.position + destVec);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ 
    private Rigidbody2D _rigidbody;
    private WaitForFixedUpdate _wait;

    public void StopSimulation()
    {
        _rigidbody.simulated = false;
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _wait = new WaitForFixedUpdate();
    }

    public void Move(Vector2 dir, float moveSpeed)
    {
        var curPos = _rigidbody.position;
        
        Vector2 dest = dir.normalized * (moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(curPos + dest);
        
    }

    public void MoveToTargetPosition(Vector2 targetPos, float moveSpeed)
    {
        var curPos = _rigidbody.position;

        Vector2 dir = targetPos - curPos;
        Vector2 dest = dir.normalized * (moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(curPos + dest);
        
    }
    
    //TODO: Velocity should be zero after knock back
    public IEnumerator KnockBack()
    {
        yield return _wait;
        var playerPos = GameManager.Instance.playerRb.position;
        var dir = _rigidbody.position - playerPos;
        _rigidbody.AddForce(dir.normalized * 3, ForceMode2D.Impulse);
        
    }

    public bool IsFacingRight(Vector2 targetPos)
    {
        return targetPos.x - _rigidbody.position.x < 0;
    }
}
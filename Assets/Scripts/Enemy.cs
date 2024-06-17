using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private CapsuleCollider2D capsuleCollider2D;

    private enum EnemyState { idle, run, die };

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

    }
    private void FixedUpdate()
    {
        EnemyAnim();
    }

    private void EnemyAnim()
    {
        EnemyState enemyState;
        if (Mathf.Abs(rigidbody2D.velocity.x) > 0)
        {
            enemyState = EnemyState.run;
        }
        else
        {
            enemyState = EnemyState.idle;
        }
        animator.SetInteger("state", (int)enemyState);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("killPoint")){
            animator.SetInteger("state", (int)EnemyState.die);
        }
    }
}

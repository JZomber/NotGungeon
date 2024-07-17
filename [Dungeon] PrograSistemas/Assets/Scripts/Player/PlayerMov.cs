using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private float speedMov;
    [SerializeField] private Animator animator;
    private bool isDead = false;
    
    private bool facingRight = true;
    private SpriteRenderer sprite;
    
    public event Action OnPlayerVictory;

    private void Start()
    {
        Time.timeScale = 1;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isDead)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(inputX * speedMov * Time.deltaTime, inputY * speedMov * Time.deltaTime, 0);

            if (inputX > 0 && !facingRight)
            {
                Flip();
            }
            else if (inputX < 0 && facingRight)
            {
                Flip();
            }

            if (inputY != 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("Speed", Mathf.Abs(inputY));
            }
            else if (inputX != 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("Speed", Mathf.Abs(inputX));
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetFloat("Speed", 0);
            }
        }
    }

    public void SetPlayerIsDead()
    {
        isDead = true;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        sprite.flipX = !sprite.flipX;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Victory"))
        {
            OnPlayerVictory?.Invoke();
        }
    }
}
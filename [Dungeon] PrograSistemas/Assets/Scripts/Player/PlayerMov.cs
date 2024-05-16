using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private float speedMov;
    //private Animator animator;
    [SerializeField] private Animator animator;

    public bool isDead = false;
    
    private LevelManager lvlManager;
    
    // Start is called before the first frame update
    void Start()
    {
        lvlManager = FindObjectOfType<LevelManager>();
        
        Time.timeScale = 1;
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(inputX, inputY, 0) * (speedMov * Time.deltaTime);
        
            if (inputY > 0 || inputY < 0)
            {
                animator.SetBool("isRunning", true); // Condición para la transición
                animator.SetFloat("Speed", Mathf.Abs(inputY)); //Independientemente del input, siempre da positivo
            }
            else if (inputX > 0 || inputX < 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("Speed", Mathf.Abs(inputX));
            }
            else
            {
                //Vuelve a "Idle"
                animator.SetBool("isRunning", false);
                animator.SetFloat("Speed", 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Victory"))
        {
            StartCoroutine(lvlManager.VictoryScreen(1f));
        }

        if (other.CompareTag("TpCollider"))
        {
            lvlManager.TpWaypoint();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    private Transform teleportPosition;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Initialize(Transform position)
    {
        teleportPosition = position;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shield"))
        {
            player.transform.position = teleportPosition.position;
        }
    }
}

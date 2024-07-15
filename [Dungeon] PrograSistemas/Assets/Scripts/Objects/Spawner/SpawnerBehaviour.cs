using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    private IEnumerator SelfDeactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        StartCoroutine(SelfDeactivate(1.5f));
    }
}

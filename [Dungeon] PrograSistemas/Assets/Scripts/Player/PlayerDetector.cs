using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    [SerializeField] private LineRenderer linePrefab;
    [SerializeField] private float laserDuration = 0.5f;
    [SerializeField] private float laserRange = 100f;
    [SerializeField] private float detectionRadius = 50f;

    private List<LineRenderer> activeLasers = new List<LineRenderer>();

    void Update()
    {
        // Input (L)
        if (Input.GetKeyDown(KeyCode.L))
        {
            FireLasers();
            
        }
    }

    void FireLasers()
    {
        // Find all the enemies inside the detectionRadious
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                StartCoroutine(FireLaserAtEnemy(hitCollider.gameObject));
            }
        }
    }

    IEnumerator FireLaserAtEnemy(GameObject enemy)
    {
        LineRenderer line = Instantiate(linePrefab);
        activeLasers.Add(line);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, enemy.transform.position);

        // Detect collision
        Vector2 direction = enemy.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, laserRange);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit: " + enemy.name);
        }
        
        yield return new WaitForSeconds(laserDuration);
        line.enabled = false;

        activeLasers.Remove(line);
        Destroy(line.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

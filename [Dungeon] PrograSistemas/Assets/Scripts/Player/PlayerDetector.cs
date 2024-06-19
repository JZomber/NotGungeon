using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    public LineRenderer linePrefab; // Prefab del LineRenderer con material asignado
    public float laserDuration = 0.5f; // Duraci�n del l�ser
    public float laserRange = 100f; // Rango del l�ser
    public float detectionRadius = 50f; // Radio de detecci�n de enemigos cercanos

    private List<LineRenderer> activeLasers = new List<LineRenderer>();

    void Update()
    {
        // Detectar la pulsaci�n del bot�n (por ejemplo, la tecla 'L')
        if (Input.GetKeyDown(KeyCode.L))
        {
            FireLasers();
            
        }
    }

    void FireLasers()
    {
        // Encontrar todos los enemigos con el tag "Enemy" dentro del radio de detecci�n
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
        // Crear una instancia del LineRenderer
        LineRenderer line = Instantiate(linePrefab);
        activeLasers.Add(line);

        // Configurar la posici�n de inicio y fin del LineRenderer
        line.SetPosition(0, transform.position);
        line.SetPosition(1, enemy.transform.position);

        // Detectar colisi�n con el enemigo usando Raycast
        Vector2 direction = enemy.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, laserRange);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            // Aqu� puedes a�adir el da�o al enemigo
            Debug.Log("Enemy hit: " + enemy.name);
        }

        // Esperar la duraci�n del l�ser y luego desactivarlo
        yield return new WaitForSeconds(laserDuration);
        line.enabled = false;

        // Eliminar el LineRenderer de la lista de activos
        activeLasers.Remove(line);
        Destroy(line.gameObject);
    }

    // Dibujar el c�rculo de detecci�n en la vista de escena para depuraci�n
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float disappearTime = 2f; // Tiempo que el enemigo permanece desaparecido
    public float appearTime = 5f; // Tiempo que el enemigo permanece visible

    private Renderer enemyRenderer;
    private Collider enemyCollider;

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        enemyCollider = GetComponent<Collider>();

        if (enemyRenderer == null || enemyCollider == null)
        {
            Debug.LogError("El enemigo necesita un Renderer y un Collider.");
            return;
        }

        // Iniciar el ciclo de aparecer/desaparecer
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // Desaparecer
            SetEnemyActive(false);
            yield return new WaitForSeconds(disappearTime);

            // Aparecer
            SetEnemyActive(true);
            yield return new WaitForSeconds(appearTime);
        }
    }

    void SetEnemyActive(bool isActive)
    {
        enemyRenderer.enabled = isActive;
        enemyCollider.enabled = isActive;
    }
}

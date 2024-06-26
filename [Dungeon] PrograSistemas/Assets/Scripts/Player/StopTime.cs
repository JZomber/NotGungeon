using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StopTime : MonoBehaviour
{
    public KeyCode timeControlKey = KeyCode.Q; // Tecla para activar el poder
    public float slowMotionDuration = 2f; // Duración del efecto de ralentización
    public float cooldownTime = 5f; // Tiempo de espera antes de poder usar el poder nuevamente
    public float slowMotionScale = 0.5f; // Escala de tiempo para el efecto de ralentización

    private bool isCooldown = false; // Bandera para verificar si está en cooldown
    private bool isSlowMotionActive = false; // Bandera para verificar si el efecto está activo

    public GameObject powerIndicator; // Referencia al objeto de la UI que indica la disponibilidad del poder

    void Update()
    {
        if (Input.GetKeyDown(timeControlKey) && !isCooldown && !isSlowMotionActive)
        {
            StartCoroutine(SlowMotionCoroutine());
        }
    }

    IEnumerator SlowMotionCoroutine()
    {
        isSlowMotionActive = true;
        Time.timeScale = slowMotionScale; // Activar el efecto de ralentización
        UpdatePowerIndicator(false); // Actualizar el indicador UI

        yield return new WaitForSecondsRealtime(slowMotionDuration); // Esperar la duración del efecto en tiempo real

        Time.timeScale = 1f; // Reanudar el tiempo normal
        isSlowMotionActive = false;

        StartCoroutine(CooldownCoroutine()); // Iniciar el cooldown
    }

    IEnumerator CooldownCoroutine()
    {
        isCooldown = true; // Establecer la bandera de cooldown
        yield return new WaitForSeconds(cooldownTime); // Esperar el tiempo de cooldown
        isCooldown = false; // Reiniciar la bandera de cooldown
        UpdatePowerIndicator(true); // Actualizar el indicador UI
    }

    void UpdatePowerIndicator(bool isActive)
    {
        if (powerIndicator != null)
        {
            powerIndicator.SetActive(isActive); // Activar o desactivar el indicador UI dependiendo de si hay usos disponibles
        }
    }
}

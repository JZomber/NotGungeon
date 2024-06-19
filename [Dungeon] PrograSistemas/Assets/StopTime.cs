using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StopTime : MonoBehaviour
{
    public KeyCode timeControlKey = KeyCode.Q; // Tecla para pausar y reanudar el tiempo
    public float maxUses = 3; // Máximo número de usos antes de esperar para incrementar nuevamente
    public float cooldownTime = 5f; // Tiempo de espera antes de incrementar el contador nuevamente
    private float currentUses; // Número actual de usos disponibles

    private bool isCooldown = false; // Bandera para verificar si está en espera de incremento

    public GameObject powerIndicator; // Referencia al objeto de la UI que indica la disponibilidad del poder

    void Start()
    {
        currentUses = maxUses; // Inicializar el número de usos
        UpdatePowerIndicator(); // Actualizar el indicador UI al inicio
    }

    void Update()
    {
        // Detectar si la tecla está presionada para pausar y reanudar el tiempo
        if (Input.GetKeyDown(timeControlKey))
        {
            if (currentUses > 0)
            {
                StopTimeAction();
            }
        }
        else if (Input.GetKeyUp(timeControlKey))
        {
            ResumeTime();
        }
    }

    void StopTimeAction()
    {
        Time.timeScale = 0; // Detener el tiempo
        currentUses--; // Reducir la cantidad de usos
        UpdatePowerIndicator(); // Actualizar el indicador UI
        Debug.Log("Usos restantes: " + currentUses);

        // Verificar si se necesita iniciar el cooldown para incrementar los usos nuevamente
        if (currentUses <= 0 && !isCooldown)
        {
            StartCoroutine(CooldownCoroutine());
        }
    }

    void ResumeTime()
    {
        Time.timeScale = 1; // Reanudar el tiempo
    }

    IEnumerator CooldownCoroutine()
    {
        isCooldown = true; // Establecer la bandera de cooldown

        yield return new WaitForSeconds(cooldownTime); // Esperar el tiempo de cooldown

        currentUses++; // Incrementar los usos
        UpdatePowerIndicator(); // Actualizar el indicador UI
        Debug.Log("Usos aumentados a: " + currentUses);

        // Reiniciar la bandera de cooldown
        isCooldown = false;
    }

    void UpdatePowerIndicator()
    {
        if (powerIndicator != null)
        {
            if (currentUses > 0)
            {
                powerIndicator.SetActive(true); // Activar el indicador
            }
            else
            {
                powerIndicator.SetActive(false); // Desactivar el indicador
            }
        }
        // Activar o desactivar el indicador UI dependiendo de si hay usos disponibles
       
    }
}

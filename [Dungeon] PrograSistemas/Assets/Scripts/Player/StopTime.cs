using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StopTime : MonoBehaviour
{
    [SerializeField] private KeyCode timeControlKey = KeyCode.Q; // Input
    [SerializeField] private float slowMotionDuration = 2f; // Power's duration
    [SerializeField] private float cooldownTime = 5f; // Power's Cooldown
    [SerializeField] private float slowMotionScale = 0.5f;
    [SerializeField] private GameObject powerIndicator;

    private bool isCooldown = false;
    private bool isSlowMotionActive = false;


    private void Update()
    {
        if (Input.GetKeyDown(timeControlKey) && !isCooldown && !isSlowMotionActive)
        {
            StartCoroutine(SlowMotionCoroutine());
        }
    }

    private IEnumerator SlowMotionCoroutine()
    {
        isSlowMotionActive = true;
        Time.timeScale = slowMotionScale;
        UpdatePowerIndicator(false);

        yield return new WaitForSecondsRealtime(slowMotionDuration);

        Time.timeScale = 1f;
        isSlowMotionActive = false;

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
        UpdatePowerIndicator(true);
    }

    private void UpdatePowerIndicator(bool isActive)
    {
        if (powerIndicator != null)
        {
            powerIndicator.SetActive(isActive);
        }
    }
}

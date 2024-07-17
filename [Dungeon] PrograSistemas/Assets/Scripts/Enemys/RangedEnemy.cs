using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] shootingOrig;
    [SerializeField] private GameObject bulletPrefab; // Bullet prefab (enemy)
    private int totalShootOrigin;

    private float coolDown;
    [SerializeField] private float shootCoolDown;

    [SerializeField] private GameObject weapon; // Weapon prefab
    private bool isSmg;
    public bool canShoot = true;
    public bool isWeaponActive = true;
    
    private void Start()
    {
        if (weapon.GameObject().name == "BasicSMG")
        {
            isSmg = true;
        }
        
        coolDown = shootCoolDown;

        totalShootOrigin = weapon.transform.childCount;
        shootingOrig = new Transform[totalShootOrigin];

        for (int i = 0; i < totalShootOrigin; i++)
        {
            shootingOrig[i] = weapon.transform.GetChild(i).transform;
        }
        
    }
    
    private void Update()
    {
        coolDown -= Time.deltaTime;

        if (coolDown <= 0f && canShoot && !isSmg)
        {
            for (int i = 0; i < shootingOrig.Length; i++)
            {
                var rotation = shootingOrig[i].rotation;
                rotation *= Quaternion.Euler(0, 0, -90);
                Instantiate(bulletPrefab, shootingOrig[i].position, rotation);
            }
            
            coolDown = shootCoolDown;
        }
        else if (coolDown <= 0f && canShoot && isSmg)
        {
            StartCoroutine(ShootSMG(0.2f));
            
            coolDown = shootCoolDown;
        }
    }

    private IEnumerator ShootSMG(float delay)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(delay);
        
            var rotation = shootingOrig[0].rotation;
            rotation *= Quaternion.Euler(0, 0, -90);
            Instantiate(bulletPrefab, shootingOrig[0].position, rotation);
        }
    }

    public IEnumerator UpdateWeaponStatus(float delay)
    {

        yield return new WaitForSeconds(delay);
        
        if (!canShoot && !isWeaponActive)
        {
            weapon.GameObject().SetActive(false);
        }
        
        if (!canShoot && isWeaponActive)
        {
            weapon.GameObject().SetActive(true);
        }
        else if (canShoot && isWeaponActive)
        {
            weapon.GameObject().SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour,IGun
{
    [SerializeField] WeaponData weaponData;
    SpriteRenderer spriteRenderer;
    AudioSource myAudio;
    List<GameObject> bullets = new List<GameObject>();
    int poolSize = 3;

    private bool isShooting = false; // Bandera para controlar el estado de disparo

    private void Start()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(weaponData.GetBulletPrefab);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null) return;

        myAudio = GetComponent<AudioSource>();

        if (myAudio == null) return;

        ChangeWeaponSprite();

    }

    public void Shoot(Transform orig)
    {
        if (!isShooting) // Solo disparar si no se está disparando actualmente
        {
            

            if (weaponData.GetBulletsPerShoot > 1) 
            {
                ShootShotgun(orig);
            }
            else if (weaponData.GetRoundsBullets > 0)
            {
                StartCoroutine(ShootSmg(weaponData.GetCadency, orig));
            }
            else
            {

                GameObject bullet = GetPooledBullet();
                if (bullet != null)
                {
                    bullet.transform.position = orig.position;
                    bullet.transform.rotation = orig.rotation * Quaternion.Euler(0, 0, -90);
                    bullet.SetActive(true);
                }

                PutShootSound();
            }
        }
    }

    public void ShootShotgun(Transform orig)
    {
        if (!isShooting) // Solo disparar si no se está disparando actualmente
        {
            StartCoroutine(ShootShotgunCoroutine(orig));
        }
    }

    private IEnumerator ShootShotgunCoroutine(Transform orig)
    {
        isShooting = true; // Establecer la bandera a true para indicar que está disparando

        float angleStep = weaponData.GetSpreadAngle / (weaponData.GetBulletsPerShoot - 1);
        float angle = -weaponData.GetSpreadAngle / 2;

        for (int i = 0; i < weaponData.GetBulletsPerShoot; i++)
        {
            GameObject bullet = GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.position = orig.position;
                bullet.transform.rotation = orig.rotation * Quaternion.Euler(0, 0, angle - 90);
                bullet.SetActive(true);
            }
            angle += angleStep;
        }

        PutShootSound();

        yield return new WaitForSeconds(weaponData.GetCadency); // Espera para la cadencia de disparo

        isShooting = false; // Restablecer la bandera a false para indicar que ha terminado de disparar
    }

    private GameObject GetPooledBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(weaponData.GetBulletPrefab);
        newBullet.SetActive(false);
        bullets.Add(newBullet);
        return newBullet;
    }

    private IEnumerator ShootSmg(float delay, Transform orig)
    {
        isShooting = true; // Establecer la bandera a true para indicar que está disparando

        for (int i = 0; i < weaponData.GetRoundsBullets; i++) // Cantidad de veces que disparará el arma (3 disparos - efecto ráfaga)
        {
            GameObject bullet = GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.position = orig.position;
                bullet.transform.rotation = orig.rotation * Quaternion.Euler(0, 0, -90);
                bullet.SetActive(true);
            }

            PutShootSound();

            yield return new WaitForSeconds(delay);
        }

        isShooting = false; // Restablecer la bandera a false para indicar que ha terminado de disparar
    }

    public void ChangeWeaponSprite() 
    {
    
        if(spriteRenderer != null && weaponData != null) 
        {
            spriteRenderer.sprite = weaponData.GetWeaponSprite;
        }
        PutReload();
    }

    public void PutReload() 
    {
        if (weaponData.GetReloadSound != null && myAudio != null) 
        { 
            myAudio.PlayOneShot(weaponData.GetReloadSound); 
        }   

    }

    public void PutShootSound() 
    {
        if (weaponData.GetShotSound != null && myAudio != null)
        {
            myAudio.PlayOneShot(weaponData.GetShotSound);
        }

    }

    public void ChangeWeaponData(WeaponData weaponData) 
    {
        this.weaponData = weaponData;
        ChangeWeaponSprite();
        
    }
}

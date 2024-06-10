using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour,IGun
{
    [SerializeField] WeaponData weaponData;
    //[SerializeField] SpriteRenderer spriteRenderer;
    List<GameObject> bullets = new List<GameObject>();
    int poolSize = 3;

    private void Start()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(weaponData.GetBulletPrefab);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }
    public void Shoot(Transform orig)
    {
        GameObject bullet = GetPooledBullet();
        if (weaponData.GetRoundsBullets > 0)
        {
            StartCoroutine(ShootSmg(weaponData.GetCadency, orig));
            
        }
        else 
        {
            if (bullet != null)
            {
                bullet.transform.position = orig.position;
                bullet.transform.rotation = orig.rotation * Quaternion.Euler(0, 0, -90);
                bullet.SetActive(true);
            }
        }
        
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

        for (int i = 0; i < weaponData.GetRoundsBullets; i++) //Cantidad de veces que disparará el arma (3 disparos - efecto ráfaga)
        {
            GameObject bullet = GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.position = orig.position;
                bullet.transform.rotation = orig.rotation * Quaternion.Euler(0, 0, -90);
                bullet.SetActive(true);
            }
            yield return new WaitForSeconds(delay);
        }
    }


}

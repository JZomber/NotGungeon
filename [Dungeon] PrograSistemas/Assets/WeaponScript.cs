using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour,IGun
{
    [SerializeField] WeaponData weaponData;
    //[SerializeField] SpriteRenderer spriteRenderer;
    List<GameObject> bullets = new List<GameObject>();
    int poolSize = 5;

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

        if (bullet != null)
        {
            bullet.transform.position = orig.position;
            bullet.transform.rotation = orig.rotation * Quaternion.Euler(0, 0, -90);
            bullet.SetActive(true);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]

public class WeaponData : ScriptableObject
{
    [SerializeField] float damage;
    [SerializeField] float cadency;
    [SerializeField] float roundsBullets;
    [SerializeField] float bulletsPerShoot;
    [SerializeField] float spreadAngle;
   
    
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip shotSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] Sprite weaponSprite;

    public float GetBulletsPerShoot => bulletsPerShoot; // N�mero de balas por disparo (para escopetas)
    public float GetSpreadAngle => spreadAngle; // �ngulo de dispersi�n del arco de las balas
    public GameObject GetBulletPrefab => bulletPrefab;
    public float GetRoundsBullets => roundsBullets;
    public float GetCadency => cadency;

    public AudioClip GetShotSound => shotSound;

    public AudioClip GetReloadSound => reloadSound;

    public Sprite GetWeaponSprite => weaponSprite;

}
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

    public float GetBulletsPerShoot => bulletsPerShoot; // Número de balas por disparo (para escopetas)
    public float GetSpreadAngle => spreadAngle; // Ángulo de dispersión del arco de las balas
    public GameObject GetBulletPrefab => bulletPrefab;
    public float GetRoundsBullets => roundsBullets;

    public float GetCadency => cadency;
}

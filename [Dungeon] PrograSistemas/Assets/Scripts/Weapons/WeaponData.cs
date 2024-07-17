using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]

public class WeaponData : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float cadency;
    [SerializeField] private float roundsBullets;
    [SerializeField] private float bulletsPerShoot;
    [SerializeField] private float spreadAngle;
   
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private Sprite weaponUiSprite;

    public float GetBulletsPerShoot => bulletsPerShoot;
    public float GetSpreadAngle => spreadAngle;
    public GameObject GetBulletPrefab => bulletPrefab;
    public float GetRoundsBullets => roundsBullets;
    public float GetCadency => cadency;

    public AudioClip GetShotSound => shotSound;

    public AudioClip GetReloadSound => reloadSound;

    public Sprite GetWeaponSprite => weaponSprite;

    public Sprite GetweaponUiSprite => weaponUiSprite;

}

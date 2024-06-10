using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]

public class WeaponData : ScriptableObject
{
    [SerializeField] float damage;
    [SerializeField] float cadency;
    [SerializeField] float roundsBullets;
    [SerializeField] GameObject bulletPrefab;
    

    public GameObject GetBulletPrefab => bulletPrefab;
    public float GetRoundsBullets => roundsBullets;

    public float GetCadency => cadency;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]

public class WeaponData : ScriptableObject
{
    [SerializeField] float damage;
    [SerializeField] float cadency;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Sprite image;

    public GameObject GetBulletPrefab => bulletPrefab;
}

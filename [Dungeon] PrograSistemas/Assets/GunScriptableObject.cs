using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "NewGunScriptableObject", menuName = "ScriptableObjects/GunScriptableObject")]
public class GunScriptableObject : ScriptableObject, IGun
{
    [SerializeField] float damage;
    [SerializeField] float cadency;
    [SerializeField] GameObject bulletPrefab;
    //[SerializeField] Sprite sprite;

    

    public void Shoot(UnityEngine.Transform orig)
    {
        var rotation = orig.rotation;
        rotation *= Quaternion.Euler(0, 0, -90);
        Instantiate(bulletPrefab, orig.position, rotation);
    }

    
}

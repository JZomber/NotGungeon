using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] shootingOrig; //Origen de las balas
    [SerializeField] private GameObject bulletPrefab; //Prefab de bullet (enemy)
    private int totalShootOrigin; //Total de or�genes (depende del arma)

    private float coolDown;
    [SerializeField] private float shootCoolDown;

    [SerializeField] private GameObject weapon; //Prefab del arma
    WeaponScript weaponScript;
    public bool canShoot = true;
    public bool isWeaponActive = true;


    // Start is called before the first frame update
    void Start()
    {       
        
        coolDown = shootCoolDown; //Cooldown entre disparos

        totalShootOrigin = weapon.transform.childCount; //Obtengo el n�mero de or�genes de balas
        shootingOrig = new Transform[totalShootOrigin]; //Inicializo la lista seg�n la cantidad

        for (int i = 0; i < totalShootOrigin; i++)
        {
            shootingOrig[i] = weapon.transform.GetChild(i).transform; //Obtengo la posici�n de cada origen
        }
        
        weaponScript = weapon.GetComponent<WeaponScript>();

    }
    
    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;

        if (coolDown <= 0f && canShoot)
        {
            
            for (int i = 0; i < shootingOrig.Length; i++)
            {
                if (weaponScript != null) 
                {
                    weaponScript.Shoot(shootingOrig[i]);
                }
               
                
            }
            
            coolDown = shootCoolDown;
        }      
    }
   

    public IEnumerator UpdateWeaponStatus(float delay)
    {

        yield return new WaitForSeconds(delay);
        
        if (!canShoot && !isWeaponActive)
        {
            weapon.GameObject().SetActive(false);
        }
        else if (!canShoot && isWeaponActive)
        {
            weapon.GameObject().SetActive(true);
        }
    }
}

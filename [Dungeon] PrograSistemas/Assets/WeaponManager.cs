using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<WeaponData> weapons = new List<WeaponData>();
    [SerializeField] private WeaponScript weapon;
    [SerializeField] private float scrollSensitivity = 1f;
    private int currentWeaponIndex = 0;
    private float scrollInput;

    private void Update()
    {
        // Capturar el input de la rueda del ratón
        scrollInput += Input.GetAxis("Mouse ScrollWheel");

        // Si el input es mayor que la sensibilidad, cambiar arma
        if (scrollInput >= scrollSensitivity)
        {
            NextWeapon();
            scrollInput = 0f; // Reiniciar el input después de cambiar de arma
        }
        else if (scrollInput <= -scrollSensitivity)
        {
            PreviousWeapon();
            scrollInput = 0f; // Reiniciar el input después de cambiar de arma
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            NextWeapon();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            PreviousWeapon();
        } 


    }

    private void NextWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        EquipWeapon(currentWeaponIndex);
    }

    private void PreviousWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Count) % weapons.Count;
        EquipWeapon(currentWeaponIndex);
    }

    private void EquipWeapon(int index)
    {
        weapon.ChangeWeaponData(weapons[index]);
    }


}

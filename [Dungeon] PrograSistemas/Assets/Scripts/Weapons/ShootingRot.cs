using UnityEngine;

namespace Weapons
{
    public class ShootingRot : MonoBehaviour
    {
        private Camera mainCam;
        private Vector3 mousePos;

        private GameObject target;
        [SerializeField] private GameObject user;
        [SerializeField] private GameObject weapon;

        private bool isRotated180 = false;

        private void Start()
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            target = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (user.CompareTag("Player"))
            {
                mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                Vector3 rotation = mousePos - transform.position;
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                if (Mathf.Abs(rotZ) > 90 && weapon != null)
                {
                    if (!isRotated180)
                    {
                        weapon.transform.localRotation = Quaternion.Euler(180, weapon.transform.localRotation.y, weapon.transform.localRotation.z);
                        isRotated180 = true;
                    }
                }
                else
                {
                    if (isRotated180)
                    {
                        weapon.transform.localRotation = Quaternion.Euler(0, weapon.transform.localRotation.y, weapon.transform.localRotation.z);
                        isRotated180 = false;
                    }
                }

            }
            else if (user.CompareTag("Enemy"))
            {
                Vector3 rotation = target.transform.position - transform.position;
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);

                if (Mathf.Abs(rotZ) > 90 && weapon != null)
                {
                    if (!isRotated180)
                    {
                        weapon.transform.localRotation = Quaternion.Euler(180, weapon.transform.localRotation.y, weapon.transform.localRotation.z);
                        isRotated180 = true;
                    }
                }
                else
                {
                    if (isRotated180)
                    {
                        weapon.transform.localRotation = Quaternion.Euler(0, weapon.transform.localRotation.y, weapon.transform.localRotation.z);
                        isRotated180 = false;
                    }
                }
            }
        }
    }
}

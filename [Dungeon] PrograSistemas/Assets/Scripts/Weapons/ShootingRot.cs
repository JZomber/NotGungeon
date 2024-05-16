using UnityEngine;

namespace Weapons
{
    public class ShootingRot : MonoBehaviour
    {
        // Start is called before the first frame update
        private Camera mainCam;
        private Vector3 mousePos;

        private GameObject target;
        public GameObject user;
        void Start()
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
            target = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            if (user.CompareTag("Player"))
            {
                mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                Vector3 rotation = mousePos - transform.position;
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
            }
            else if (user.CompareTag("Enemy"))
            {
                Vector3 rotation = target.transform.position - transform.position;
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
            }
        }

    }
}

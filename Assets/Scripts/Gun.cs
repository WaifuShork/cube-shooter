using System.Security;
using UnityEngine;

namespace Shork
{
    public class Gun : MonoBehaviour
    {

        public UnityEngine.Camera cam;
        public ParticleSystem muzzleFlash;
        public ParticleSystem bullet;

        [SerializeField]
        private float damage = 10f;
        [SerializeField]
        private float range = 100f;
        [SerializeField]
        private float fireRate = 15f;
        [SerializeField]
        private float nextTimeToFire = 0f;




        void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }

        }


        void Shoot()
        {

            FindObjectOfType<AudioManager>().Play("BlasterShot");
            muzzleFlash.Play();
            bullet.Play();

            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
    }
}
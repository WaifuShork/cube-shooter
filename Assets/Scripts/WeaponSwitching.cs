using UnityEngine;

namespace Shork
{
    public class WeaponSwitching : MonoBehaviour
    {
        [SerializeField]
        private int selectedWeapon = 0;

        private void Start()
        {
            SelectWeapon();
        }

        // Handling input and logic for weapon switching (there's probably a better way to do this but I don't know how
        private void Update()
        {
            int previousSelectedWeapon = selectedWeapon;

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

            if (previousSelectedWeapon != selectedWeapon)
            {
                SelectWeapon();
            }


        }

        // Execution data for switching weapons
        private void SelectWeapon()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                    weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
                i++;
            }
        }
    }
}
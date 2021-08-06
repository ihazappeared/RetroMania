using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

    // Start is called before the first frame update
    void Start()
    {
        current_Weapon_Index = 0;
        if (weapons[current_Weapon_Index].hasWeapon)
        {
            weapons[current_Weapon_Index].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            turnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            turnOnSelectedWeapon(1);
        }
    }

    void turnOnSelectedWeapon(int weaponIndex)
    {
        if(weapons[weaponIndex].hasWeapon)
        {
            weapons[current_Weapon_Index].gameObject.SetActive(false);

            weapons[weaponIndex].gameObject.SetActive(true);

            current_Weapon_Index = weaponIndex;
        }
    }

    public int GetCurrentWeapon()
    {
        return current_Weapon_Index;
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_Weapon_Index];
    }

}

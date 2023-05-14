using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private string shootKey = null;

    private bool weaponIsActive;
    private PlayerController playerController;
    private WeaponFunctions weapon = null;

    private void Start()
    {
        weaponIsActive = false;
        playerController = GetComponent<PlayerController>();
    }
    public void Update()
    {
        if (Input.GetButtonDown(shootKey))
        {
            weaponAction();
        }

        if (weaponIsActive)
        {
            if (Input.GetButton(playerController.getDownInput()) && playerController.getIsGrounded() == false)
            {
                gameObject.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            }
            else if (playerController.getIsFronted() == true)
            {
                gameObject.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else
            {
                gameObject.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

    }
    private void weaponAction()
    {
        if (weaponIsActive == true)
        {
            weapon.hit();
        }
    }

    public void armNewWeapon(GameObject newGun)
    {
        weaponIsActive = true;
        weapon = newGun.GetComponent<WeaponFunctions>();
    }

    public void armOldWeapon() 
    { 
        if(weapon != null) weaponIsActive = true; 
    }

    public void disarmWeapon() { weaponIsActive = false; }
    public bool getWeaponIsActive() { return weaponIsActive; }

    public void setWeapon(WeaponFunctions w) { weapon = w; }
}

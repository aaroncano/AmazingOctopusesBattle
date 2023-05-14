using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWeapon : MonoBehaviour
{
    [SerializeField] private Collider2D colliderToDisable = null;
    [SerializeField] private Collider2D colliderToDestroy = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("pickUpGun");
            PlayerShooting otherPlayerShooting = other.gameObject.GetComponent<PlayerShooting>();

            if(otherPlayerShooting.getWeaponIsActive() == false)
            {
                activateWeapon(other.gameObject);
                changeProperties();
            }
            else if(otherPlayerShooting.getWeaponIsActive() == true)
            {
                WeaponFunctions otherWeaponFuntions = other.gameObject.GetComponentInChildren<WeaponFunctions>();
                otherWeaponFuntions.throwWeapon();

                activateWeapon(other.gameObject);
                changeProperties();
            }
        }
        
    }

    private void changeProperties()
    {
        colliderToDisable.enabled = false; 
        Destroy(colliderToDestroy);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
    }
    private void activateWeapon(GameObject player)
    {
        gameObject.transform.position = player.transform.GetChild(0).transform.GetChild(0).position;
        gameObject.transform.parent = player.transform.GetChild(0).transform.GetChild(0);
        gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        player.GetComponent<PlayerShooting>().armNewWeapon(gameObject);
        gameObject.GetComponent<WeaponFunctions>().setAnimatorAndParent(player.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Animator>(), player);
    }

    public Collider2D getColliderToDisable() { return colliderToDisable; }
}

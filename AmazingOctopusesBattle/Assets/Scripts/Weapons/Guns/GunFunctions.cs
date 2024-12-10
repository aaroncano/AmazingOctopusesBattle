using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFunctions : WeaponFunctions
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private ParticleSystem smokePrefab = null;
    [SerializeField] private float bulletForce = 0f;
    [SerializeField] private Transform[] firePoints = null;
    [SerializeField] private int maxAmmo = 0;
    [SerializeField] private float bulletRange = 0f;

    private int ammo;

    private void Start()
    {
        ammo = maxAmmo;
    }
    override public void hit()
    {
        if(ammo > 0)
        {
            if(canHit <= Time.time)
            {
                smokePrefab.time = 0f;
                smokePrefab.Play();

                foreach(Transform firePoint in firePoints)
                {
                    FindObjectOfType<AudioManager>().Play(nombreIndex);
                    GameObject bulletP = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    Destroy(bulletP, bulletRange);
                    Rigidbody2D BulletRb = bulletP.GetComponent<Rigidbody2D>();
                    bulletP.GetComponent<Rigidbody2D>().AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
                }
                animator.SetTrigger(nombreIndex);
                ammo--;
                canHit = Time.time + waitForHit;
            }
        }
        else if (ammo <= 0 && canHit <= Time.time)
        {
            throwWeapon();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && thrownOut == true)
        {
            if (!(other.gameObject.Equals(dadPlayer)))
            {
                other.gameObject.GetComponent<PlayerController>().getParalyzed();
                FindObjectOfType<AudioManager>().Play("WeaponGround");
                Destroy(Instantiate(weaponParticles, transform.position, Quaternion.identity), 1.5f);
                Destroy(gameObject);
            }
        }
        else if(thrownOut == true && other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
        else if(thrownOut == true)
        {
            FindObjectOfType<AudioManager>().Play("weaponGround");
            Destroy(Instantiate(weaponParticles, transform.position, Quaternion.identity), 1.5f);
            Destroy(gameObject);
        }
    }

    public bool isFull()
    {
        if (ammo == maxAmmo) return true;
        else return false;
    }
}

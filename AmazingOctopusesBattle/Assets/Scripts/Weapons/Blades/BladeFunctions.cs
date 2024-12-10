using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeFunctions : WeaponFunctions
{
    [SerializeField] private Transform attackPoint = null;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private LayerMask attackLayer = default;
    [SerializeField] private int damage = 0;

    override public void hit()
    {
        if (canHit <= Time.time)
        {
            FindObjectOfType<AudioManager>().Play(nombreIndex);
            animator.SetTrigger(nombreIndex);
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
            Destroy(Instantiate(weaponParticles, attackPoint.position, Quaternion.identity), 1.5f);
            foreach (Collider2D hit in hits)
            {
                if (!(hit.gameObject.Equals(dadPlayer)) && hit.gameObject.CompareTag("Player"))
                {
                    hit.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
                }
            }
            canHit = Time.time + waitForHit;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && thrownOut == true)
        {
            if (!(other.gameObject.Equals(dadPlayer)))
            {
                FindObjectOfType<AudioManager>().Play("WeaponGround");
                other.gameObject.GetComponent<PlayerController>().getParalyzed();
                other.gameObject.GetComponent<PlayerHealth>().takeDamage();
                Destroy(Instantiate(weaponParticles, transform.position, Quaternion.identity), 1.5f);
                Destroy(gameObject);
            }
        }
        else if (thrownOut == true && other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
        else if (thrownOut == true)
        {
            FindObjectOfType<AudioManager>().Play("WeaponGround");
            Destroy(Instantiate(weaponParticles, transform.position, Quaternion.identity), 1.5f);
            Destroy(gameObject);
        }
    } 
    private void OnDrawGizmosSelected()
    { 
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}

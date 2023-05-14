using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletParticles = null;
    [SerializeField] private int damage = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("weaponGround");
        }
        Destroy(Instantiate(bulletParticles, gameObject.transform.position, Quaternion.identity), 1f);
        Destroy(gameObject);
    }
}

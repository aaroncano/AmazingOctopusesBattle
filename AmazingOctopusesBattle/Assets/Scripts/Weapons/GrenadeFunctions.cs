using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeFunctions : WeaponFunctions
{
    [SerializeField] private GameObject smokeParticles = null;
    [SerializeField] private int damage = 0;
    [SerializeField] private GameObject effectorPrefab = null;
    [SerializeField] private float timeToExplosion = 0f;
    [SerializeField] private float explosionRange = 0f;
    [SerializeField] private bool isSticky = false;
    [SerializeField] private LayerMask explosionLayer = default;

    private bool hasTouchedGround = false;
    private bool hasExploted = false;

    private void Awake()
    {
        hasExploted = false;
        hasTouchedGround = false;
    }
    override public void hit()
    {
        base.throwWeapon();
    }
    public IEnumerator explosion()
    {
        yield return new WaitForSeconds(timeToExplosion);
        FindObjectOfType<AudioManager>().Play(nombreIndex);
        Destroy(Instantiate(weaponParticles, transform.position, Quaternion.identity), 1.5f);
        Destroy(Instantiate(smokeParticles, transform.position, Quaternion.identity), 1.5f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRange, explosionLayer);
        Destroy(Instantiate(effectorPrefab, transform.position, Quaternion.identity), 0.1f);
        hasExploted = true;
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                hit.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            }
            if (hit.gameObject.CompareTag("Gun"))
            {
                GrenadeFunctions gf = hit.gameObject.GetComponent<GrenadeFunctions>();
                if (gf != null)
                {
                    if (gf.hasExploted == false) gf.chainExplosion();
                }
            }
        }
        Destroy(gameObject);
    }
    
    public void chainExplosion()
    {
        FindObjectOfType<AudioManager>().Play(nombreIndex);
        Destroy(Instantiate(weaponParticles, transform.position, Quaternion.identity), 1.5f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRange, explosionLayer);
        Destroy(Instantiate(effectorPrefab, transform.position, Quaternion.identity), 0.1f);
        hasExploted = true;
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                hit.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            }
            if (hit.gameObject.CompareTag("Gun"))
            {
                GrenadeFunctions gf = hit.gameObject.GetComponent<GrenadeFunctions>();
                if (gf != null)
                {
                    if (gf.hasExploted == false) gf.chainExplosion();
                }
            }
        }
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (thrownOut == true && hasTouchedGround == false)
        {
            if (!(other.gameObject.Equals(dadPlayer)))
            {
                if(isSticky == true)
                {
                    FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                    joint.connectedBody = other.rigidbody;
                }
                else if(other.gameObject.CompareTag("Player")) other.gameObject.GetComponent<PlayerController>().getParalyzed();
                hasTouchedGround = true;
                FindObjectOfType<AudioManager>().Play("weaponGround");
                StartCoroutine(explosion());
            }
        }
        else if (thrownOut == true && other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

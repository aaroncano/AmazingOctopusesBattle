using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponFunctions : MonoBehaviour
{
    [SerializeField] protected string nombreIndex = null;
    [SerializeField] protected GameObject weaponParticles = null;
    [SerializeField] protected float launchForce = 0f;
    [SerializeField] protected float waitForHit = 0f;

    protected float canHit = 0f;
    protected bool thrownOut = false;
    protected GameObject dadPlayer = null;
    protected Animator animator = null;

    public abstract void hit();
    public virtual void throwWeapon()
    {
        gameObject.transform.SetParent(null);
        dadPlayer.GetComponent<PlayerShooting>().disarmWeapon();
        dadPlayer.GetComponent<PlayerShooting>().setWeapon(null);

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<TriggerWeapon>().getColliderToDisable().enabled = true;

        FindObjectOfType<AudioManager>().Play("throwWeapon");
        rb.AddForce(gameObject.GetComponentInParent<Transform>().right * launchForce, ForceMode2D.Impulse);
        thrownOut = true;
    }
    public void setAnimatorAndParent(Animator ani, GameObject dad)
    {
        animator = ani;
        dadPlayer = dad;
    }

    public string getNombreIndex() { return nombreIndex; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth=0;
    [SerializeField] private ParticleSystem bloodParticles = null;
    [SerializeField] private int health;
    [SerializeField] private int index = 0;

    //hit flash
    [SerializeField] private Material flashMaterial = null;
    private Material dftMaterial;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private bool playerIsDead = false;

    void Start()
    {
        health = maxHealth;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        dftMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        playerController = gameObject.GetComponent<PlayerController>();
    }

    public void takeDamage()
    {
        bloodParticles.time = 0f;
        bloodParticles.Play();
        FindObjectOfType<AudioManager>().Play("hit");
        GameManagement.inst.loseHearts(index, maxHealth, health, 1);
        health--;
        if (health<=0) playerDeath();
        else
        {
            spriteRenderer.material = flashMaterial;
            StartCoroutine(defaultMaterial());
        }
    }
    public void takeDamage(int damage)
    {
        bloodParticles.time = 0f;
        bloodParticles.Play();
        FindObjectOfType<AudioManager>().Play("hit");
        GameManagement.inst.loseHearts(index, maxHealth, health, damage);
        health -= damage;
        if (health <= 0) playerDeath();
        else
        {
            spriteRenderer.material = flashMaterial;
            StartCoroutine(defaultMaterial());
        }
    }
    public void instantDeath()
    {
        FindObjectOfType<AudioManager>().Play("dead");
        GameManagement.inst.loseHearts(index, maxHealth, health, maxHealth);
        health = 0;
        playerDeath();
    }
    public void playerDeath()
    {
        if(playerIsDead == false)
        {
            FindObjectOfType<AudioManager>().Play("dead");
            playerIsDead = true;
            playerController.getStuned();
            CameraFocus cf = FindObjectOfType<CameraFocus>();
            if(cf != null) cf.changeToOneTarget(gameObject);

            //añade un punto al pulpo correspondiente
            GameManagement.inst.addScore(index);
        }
    }

    IEnumerator defaultMaterial()
    {
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.material = dftMaterial;
    }

    public int getHealth() { return health; }
}

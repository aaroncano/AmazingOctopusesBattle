using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private GameObject gun = null;
    public bool isLoser = false;

    private const string idle = "Player_Idle";
    private const string dead = "Player_Dead";

    private string currentAnim;

    public void activateGun() { gun.SetActive(true); }
    public void changeRotation()
    {
        if(gameObject.transform.position.x < 0 && !isLoser) transform.Rotate(0f, 180f, 0f);
        else if(gameObject.transform.position.x > 0 && isLoser) transform.Rotate(0f, 180f, 0f);
    }

    private void changeAnim(string newAnim)
    {
        if (currentAnim == newAnim) return;
        animator.Play(newAnim);
        currentAnim = newAnim;
    }
    
    public void changeAnimToIdle()
    {
        changeAnim(idle);
    }
    public void changeAnimToDead()
    {
        transform.Rotate(0f, 0f, 90f);
        changeAnim(dead);
    }
}

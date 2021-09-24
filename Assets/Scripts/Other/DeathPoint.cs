using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DeathPoint : MonoBehaviour
{
    private int deadCount = 0;
    [SerializeField] private CinemachineVirtualCamera camera2 = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deadCount++;
            other.gameObject.GetComponent<PlayerHealth>().instantDeath();
        }
        if(deadCount > 1)
        {
            camera2.Follow = null;
        }
    }
}

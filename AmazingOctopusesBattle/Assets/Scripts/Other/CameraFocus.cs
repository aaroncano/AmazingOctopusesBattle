using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFocus : MonoBehaviour
{
    private GameObject[] players = null;
    [SerializeField] private CinemachineVirtualCamera camera2 = null;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void changeToOneTarget(GameObject player)
    {
        foreach (GameObject p in players)
        {
            if (!(p.Equals(player)) && p.activeSelf == true)
            {
                camera2.Follow = p.transform;
                camera2.LookAt = p.transform;
                camera2.Priority = 20;
            }
        }
    }

    public void stowFollowing()
    {
        camera2.Follow = null;
    }
}

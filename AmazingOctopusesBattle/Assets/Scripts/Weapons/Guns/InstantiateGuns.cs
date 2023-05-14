using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InstantiateGuns : MonoBehaviour
{
    private float timeToNewGun = 0f;
    [SerializeField] private float waitToNewGun = 0f;
    [SerializeField] private GameObject[] GunPrefabs = null;
    [SerializeField] private Transform gunsSpot = null;
    [SerializeField] private LayerMask ground = default;
    [SerializeField] private float minX=0f, maxX=0f, minY=0f, maxY=0f;
     
    void Start()
    {
        timeToNewGun = Time.time + waitToNewGun;
    }

    void Update()
    {
        if(timeToNewGun <= Time.time)
        {
            if (GameObject.FindGameObjectsWithTag("Gun").Length < 4)
            {
                spawnGun();
            }
            else timeToNewGun = Time.time + waitToNewGun;
        }
        //if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void spawnGun()
    {
        gunsSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        bool canSpawn = Physics2D.OverlapCircle(gunsSpot.position, 2f, ground);
        if (canSpawn == false)
        {
            Instantiate(GunPrefabs[Random.Range(0, GunPrefabs.Length)], gunsSpot.position, Quaternion.identity);
            timeToNewGun = Time.time + waitToNewGun;
        }
    }
}

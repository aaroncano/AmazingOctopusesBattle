using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraPos : MonoBehaviour
{
    [SerializeField] private Transform player1 = null, player2 = null;
    [SerializeField] private float minSizeY = 0f;

    void Update()
    {
        cameraPosition();
        cameraSize();

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void cameraPosition()
    {
        Vector2 middlePos = (player1.position + player2.position) * 0.5f;
        Camera.main.transform.position = new Vector3(middlePos.x, middlePos.y, Camera.main.transform.position.z);
    }
    private void cameraSize()
    {
        float minSizeX = minSizeY * Screen.width / Screen.height;
        float with = Mathf.Abs(player1.position.x - player2.position.x) * 0.5f;
        float heith = Mathf.Abs(player1.position.y - player2.position.y) * 0.5f;

        float camSizeX = Mathf.Max(with, minSizeX);
        Camera.main.orthographicSize = Mathf.Max(heith, (camSizeX * Screen.height / Screen.width) + 3, minSizeY);

    }
}

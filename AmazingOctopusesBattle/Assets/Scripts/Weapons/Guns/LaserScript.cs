using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField] private LineRenderer line = null;
    [SerializeField] private float dist = 5f;
    [SerializeField] private Transform laserPoint = null;


    private void Update()
    {
        if(Physics2D.Raycast(transform.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserPoint.position, transform.right);
            DrawLaser(laserPoint.position, hit.point);
        }
        else
        {
            DrawLaser(laserPoint.position, laserPoint.right * dist);
        }
    }
    void DrawLaser(Vector2 startPos, Vector2 endPos)
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }
}

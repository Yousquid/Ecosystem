using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMover : MonoBehaviour
{
    public float speed;

    private bool isMoving = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isMoving = true;

        if (isMoving)
            MoveCircle();
    }

    private void MoveCircle()
    {
        this.transform.position += new Vector3(speed, 0);
    }

    private void TankFrameRate()
    {
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log("slowing down framerate");
        }
    }
}

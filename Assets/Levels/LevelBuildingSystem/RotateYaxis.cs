using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYaxis : MonoBehaviour
{
    public GameObject plane;
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.RotateAround(plane.transform.position, -transform.up, Time.deltaTime * 90f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(plane.transform.position, transform.up, Time.deltaTime * 90f);
        }
    }
}

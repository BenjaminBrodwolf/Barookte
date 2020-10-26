using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool upSet = false;
    private bool downSet = false;
    private bool leftSet = false;
    private bool rightSet = false;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);

        var newPosition = _rigidbody.position;


        if (up)
        {
            var allowed = Physics.Raycast(newPosition, new Vector3(0, -0.5f, 1));
            if (allowed)
                newPosition += new Vector3(0, 0, 1);
        }

        if (down)
        {
            var allowed = Physics.Raycast(newPosition, new Vector3(0, -0.5f, -1));
            if (allowed)
                newPosition += new Vector3(0, 0, -1);
        }

        if (left)
        {
            var allowed = Physics.Raycast(newPosition, new Vector3(-1, -0.5f, 0));
            if (allowed)
                newPosition += new Vector3(-1, 0, 0);
        }

        if (right)
        {
            var allowed = Physics.Raycast(newPosition, new Vector3(1, -0.5f, 0));
            if (allowed)
                newPosition += new Vector3(1, 0, 0);
        }

        _rigidbody.MovePosition(newPosition);
    }
}
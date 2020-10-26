using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    enum MoveDirections
    {
        UP,
        RIGTH,
        DOWN,
        LEFT
    }

    private bool CanMove(Vector3 currentPosition, MoveDirections md)
    {
        var raycastDirection = new Vector3(0, -2, 0);
        
        switch (md)
        {
            case MoveDirections.UP:
            {
                return Physics.Raycast(currentPosition + new Vector3(0, 1, 1), raycastDirection)
                       && !Physics.Raycast(currentPosition, new Vector3(0, 0, 1), 1f);
            }
            case MoveDirections.DOWN:
            {
                return Physics.Raycast(currentPosition + new Vector3(0, 1, -1), raycastDirection)
                       && !Physics.Raycast(currentPosition, new Vector3(0, 0, -1), 1f);
            }
            case MoveDirections.LEFT:
            {
                return Physics.Raycast(currentPosition + new Vector3(-1, 1, 0), raycastDirection)
                       && !Physics.Raycast(currentPosition, new Vector3(-1, 0, 0), 1f);
            }
            case MoveDirections.RIGTH:
            {
                return Physics.Raycast(currentPosition + new Vector3(1, 1, 0), raycastDirection)
                       && !Physics.Raycast(currentPosition, new Vector3(1, 0, 0), 1f);
            }
        }
        return false;
    }

    // Update is called once per frame
    private void Update()
    {
        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);

        var newPosition = _rigidbody.position;


        // Debug.DrawRay(newPosition + new Vector3(0, 1, 1), new Vector3(0, -2, 0), Color.red);
        if (up)
        {
            Debug.Log("up");
            if (CanMove(newPosition, MoveDirections.UP))
                newPosition += new Vector3(0, 0, 1);
        }

        if (down)
        {
            Debug.Log("down");
            if (CanMove(newPosition, MoveDirections.DOWN))
                newPosition += new Vector3(0, 0, -1);
        }

        if (left)
        {
            Debug.Log("left");
            if (CanMove(newPosition, MoveDirections.LEFT))
                newPosition += new Vector3(-1, 0, 0);
        }

        if (right)
        {
            Debug.Log("right");
            if (CanMove(newPosition, MoveDirections.RIGTH))
                newPosition += new Vector3(1, 0, 0);
        }

        _rigidbody.MovePosition(newPosition);
    }
}
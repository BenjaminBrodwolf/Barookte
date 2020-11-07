using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public enum MoveDirections
    {
        Up,
        Rigth,
        Down,
        Left
    }

    public bool CanMove(Vector3 currentPosition, MoveDirections md)
    {
        var raycastDirection = new Vector3(0, -2, 0);
        
        switch (md)
        {
            case MoveDirections.Up:
            {
                return Physics.Raycast(currentPosition + new Vector3(0, 1, 1), raycastDirection)
                       && !Physics.Raycast(currentPosition, new Vector3(0, 0, 1), 1f);
            }
            case MoveDirections.Down:
            {
                return Physics.Raycast(currentPosition + new Vector3(0, 1, -1), raycastDirection)
                       && !Physics.Raycast(currentPosition, new Vector3(0, 0, -1), 1f);
            }
            case MoveDirections.Left:
            {
                return Physics.Raycast(currentPosition + new Vector3(-1, 1, 0), raycastDirection)
                       && !Physics.Raycast(currentPosition, new Vector3(-1, 0, 0), 1f);
            }
            case MoveDirections.Rigth:
            {
                return Physics.Raycast(currentPosition + new Vector3(1, 1, 0), raycastDirection)
                       && !Physics.Raycast(currentPosition, new Vector3(1, 0, 0), 1f);
            }
        }
        return false;
    }

    public void MovePlayer(MoveDirections moveDirection)
    {
        var newPosition = _rigidbody.position;
        switch (moveDirection)
        {
            case MoveDirections.Left:
                newPosition += new Vector3(-1, 0, 0);
                break;
            case MoveDirections.Up:
                newPosition += new Vector3(0, 0, 1);
                break;
            case MoveDirections.Rigth:
                newPosition += new Vector3(1, 0, 0);
                break;
            case MoveDirections.Down:
                newPosition += new Vector3(0, 0, -1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
        }

        _rigidbody.MovePosition(newPosition);
    }
    
}
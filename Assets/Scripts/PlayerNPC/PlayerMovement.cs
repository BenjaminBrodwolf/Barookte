using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public enum Directions
    {
        Up,
        Rigth,
        Down,
        Left
    }


    public bool CanMove(Vector3 currentPosition, Directions md)
    {
        var raycastDirection = new Vector3(0, -1, 0);

        switch (md)
        {
            case Directions.Up:
            {
                var wannaMoveDirection = new Vector3(0, 0, 1);
                ReactToMoveableItem(currentPosition, wannaMoveDirection);


                Debug.DrawRay(currentPosition + wannaMoveDirection, raycastDirection, Color.red, 20);
                
                return Physics.Raycast(currentPosition + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(currentPosition, new Vector3(0, 0, 1), 1f);
            }
            case Directions.Down:
            {
                var wannaMoveDirection = new Vector3(0, 0, -1);
                ReactToMoveableItem(currentPosition, wannaMoveDirection);


                return Physics.Raycast(currentPosition + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(currentPosition, new Vector3(0, 0, -1), 1f);
            }
            case Directions.Left:
            {
                var wannaMoveDirection = new Vector3(-1, 0, 0);
                ReactToMoveableItem(currentPosition, wannaMoveDirection);


                return Physics.Raycast(currentPosition + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(currentPosition, new Vector3(-1, 0, 0), 1f);
            }
            case Directions.Rigth:
            {
                var wannaMoveDirection = new Vector3(1, 0, 0);
                ReactToMoveableItem(currentPosition, wannaMoveDirection);

                return Physics.Raycast(currentPosition + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(currentPosition, new Vector3(1, 0, 0), 1f);
            }
        }

        return false;
    }

    public Vector3 MovePlayer(Directions direction)
    {
        var newPosition = _rigidbody.position;
        switch (direction)
        {
            case Directions.Left:
                newPosition += new Vector3(-1, 0, 0);
                break;
            case Directions.Up:
                newPosition += new Vector3(0, 0, 1);
                break;
            case Directions.Rigth:
                newPosition += new Vector3(1, 0, 0);
                break;
            case Directions.Down:
                newPosition += new Vector3(0, 0, -1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        _rigidbody.MovePosition(newPosition);
        return newPosition;
    }

    public void ReactToMoveableItem(Vector3 currentPosition, Vector3 newDirection)
    {
        RaycastHit hit;

        if (Physics.Raycast(currentPosition, newDirection, out hit, 1f, LayerMask.GetMask("MoveableItem")))
        {
            var moveableItemScript = hit.collider.GetComponent<MoveableItem>();

            if (moveableItemScript.IsPlayerInTriggerToItem())
            {
                moveableItemScript.PushItemInDirection(newDirection);
            }
        }
    }
}
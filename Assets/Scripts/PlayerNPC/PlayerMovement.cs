using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public int speed = 10;

    public Vector3 Position { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Position = transform.position;
    }

    public enum Directions
    {
        Up,
        Rigth,
        Down,
        Left
    }


    public bool CanMove(Directions md)
    {
        var raycastDirection = new Vector3(0, -1, 0);

        switch (md)
        {
            case Directions.Up:
            {
                var wannaMoveDirection = new Vector3(0, 0, 1);
                ReactToMoveableItem(Position, wannaMoveDirection);


                Debug.DrawRay(Position + wannaMoveDirection, raycastDirection, Color.red, 20);
                
                return Physics.Raycast(Position + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(Position, new Vector3(0, 0, 1), 1f, LayerMask.GetMask("MovebleItem", "Enemy"));
            }
            case Directions.Down:
            {
                var wannaMoveDirection = new Vector3(0, 0, -1);
                ReactToMoveableItem(Position, wannaMoveDirection);


                return Physics.Raycast(Position + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(Position, new Vector3(0, 0, -1), 1f, LayerMask.GetMask("MovebleItem", "Enemy"));
            }
            case Directions.Left:
            {
                var wannaMoveDirection = new Vector3(-1, 0, 0);
                ReactToMoveableItem(Position, wannaMoveDirection);


                return Physics.Raycast(Position + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(Position, new Vector3(-1, 0, 0), 1f, LayerMask.GetMask("MovebleItem", "Enemy"));
            }
            case Directions.Rigth:
            {
                var wannaMoveDirection = new Vector3(1, 0, 0);
                ReactToMoveableItem(Position, wannaMoveDirection);

                return Physics.Raycast(Position + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(Position, new Vector3(1, 0, 0), 1f, LayerMask.GetMask("MovebleItem", "Enemy")); 
            }
        }

        return false;
    }

    public Vector3 UpdatePosition(Directions direction)
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
        
        Position = newPosition;
        return newPosition;
    }

    public bool UpdatePositionPerFrame(double animationAccuracy = 0.005)
    {
        var moveDirection = Position - _rigidbody.position;
        var deltaMovement = moveDirection * (speed * Time.deltaTime);
        _rigidbody.MovePosition(_rigidbody.position + deltaMovement);
        
        return (_rigidbody.transform.position - Position).magnitude < animationAccuracy;
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
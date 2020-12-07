using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject gameManagerObject;
    private GameManager gameManagerScript;
    public int speed = 10;
    private bool isAnimating = false;

    public Vector3 TurnPosition { get; private set; }

    private void Awake()
    {
        TurnPosition = transform.position;
        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
        Debug.Log(gameManagerScript);
    }

    private void Update()
    {
        if (isAnimating)
        {
            isAnimating = UpdatePositionPerFrame();
        }
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
                ReactToMoveableItem(TurnPosition, wannaMoveDirection);
                
                Debug.DrawRay(TurnPosition + wannaMoveDirection, raycastDirection, Color.red, 20);
                
                return Physics.Raycast(TurnPosition + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(TurnPosition, new Vector3(0, 0, 1), 1f, LayerMask.GetMask("MoveableItem"));
            }
            case Directions.Down:
            {
                var wannaMoveDirection = new Vector3(0, 0, -1);
                ReactToMoveableItem(TurnPosition, wannaMoveDirection);
                
                return Physics.Raycast(TurnPosition + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(TurnPosition, new Vector3(0, 0, -1), 1f, LayerMask.GetMask("MoveableItem"));
            }
            case Directions.Left:
            {
                var wannaMoveDirection = new Vector3(-1, 0, 0);
                ReactToMoveableItem(TurnPosition, wannaMoveDirection);
                
                return Physics.Raycast(TurnPosition + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(TurnPosition, new Vector3(-1, 0, 0), 1f, LayerMask.GetMask("MoveableItem"));
            }
            case Directions.Rigth:
            {
                var wannaMoveDirection = new Vector3(1, 0, 0);
                ReactToMoveableItem(TurnPosition, wannaMoveDirection);

                return Physics.Raycast(TurnPosition + wannaMoveDirection, raycastDirection, LayerMask.GetMask("Earth"))
                       && !Physics.Raycast(TurnPosition, new Vector3(1, 0, 0), 1f, LayerMask.GetMask("MoveableItem")); 
            }
        }

        return false;
    }

    public Vector3 UpdatePosition(Directions direction)
    {
        var newPosition = TurnPosition;
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
        
        TurnPosition = newPosition;
        isAnimating = true;
        return newPosition;
    }

    public bool UpdatePositionPerFrame(double animationAccuracy = 0.005)
    {
        var actualPosition = transform.position;
        var moveDirection = TurnPosition - actualPosition;
        var deltaMovement = moveDirection * (speed * Time.deltaTime);
        actualPosition += deltaMovement;
        transform.position = actualPosition;

        return (actualPosition - TurnPosition).magnitude >= animationAccuracy;
    }

    public void ReactToMoveableItem(Vector3 currentPosition, Vector3 newDirection)
    {
        if (Physics.Raycast(currentPosition, newDirection, out var hit, 1f, LayerMask.GetMask("MoveableItem")))
        {
            var moveableItemScript = hit.collider.GetComponent<MoveableItem>();
           
            if (moveableItemScript.IsPlayerInTriggerToItem())
            {
                // TODO: execute next round, so that Enemy do the next turn
                moveableItemScript.PushItemInDirection(newDirection);
                gameManagerScript.EnemyTurn();
            }
        }
    }
    
  
    

}
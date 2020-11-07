using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private BoardBuilder boardManager;

    private GameObject player;
    private PlayerInput _playerInput;

    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        boardManager = GetComponentInChildren<BoardBuilder>();
        boardManager.buildBoard();

        player = GameObject.FindGameObjectWithTag("Player");
        _playerInput = player.GetComponent<PlayerInput>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);

        var hasPlayerMoved = false;
        var canPlayerMove = false;

        if (up)
        {
            Debug.Log("up");
            canPlayerMove = _playerInput.CanMove(player.transform.position, PlayerInput.MoveDirections.Up);
            if (canPlayerMove)
            {
                _playerInput.MovePlayer(PlayerInput.MoveDirections.Up);
                hasPlayerMoved = true;
            }
        }

        if (down)
        {
            Debug.Log("down");
            canPlayerMove = _playerInput.CanMove(player.transform.position, PlayerInput.MoveDirections.Down);
            if (canPlayerMove)
            {
                _playerInput.MovePlayer(PlayerInput.MoveDirections.Down);
                hasPlayerMoved = true;
            }
        }

        if (left)
        {
            Debug.Log("left");
            canPlayerMove = _playerInput.CanMove(player.transform.position, PlayerInput.MoveDirections.Left);
            if (canPlayerMove)
            {
                _playerInput.MovePlayer(PlayerInput.MoveDirections.Left);
                hasPlayerMoved = true;
            }
        }

        if (right)
        {
            Debug.Log("right");
            canPlayerMove = _playerInput.CanMove(player.transform.position, PlayerInput.MoveDirections.Rigth);
            if (canPlayerMove)
            {
                _playerInput.MovePlayer(PlayerInput.MoveDirections.Rigth);
                hasPlayerMoved = true;
            }
        }
       
        
        if (hasPlayerMoved)
        {
            //trigger movement on enemies
        }
    }
}
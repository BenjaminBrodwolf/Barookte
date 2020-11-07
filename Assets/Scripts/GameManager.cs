using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private BoardBuilder boardManager;

    private GameObject player;
    private PlayerMovement _playerMovement;

    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        boardManager = GetComponentInChildren<BoardBuilder>();
        boardManager.buildBoard();

        player = GameObject.FindGameObjectWithTag("Player");
        _playerMovement = player.GetComponent<PlayerMovement>();
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
            canPlayerMove = _playerMovement.CanMove(player.transform.position, PlayerMovement.MoveDirections.Up);
            if (canPlayerMove)
            {
                _playerMovement.MovePlayer(PlayerMovement.MoveDirections.Up);
                hasPlayerMoved = true;
            }
        }

        if (down)
        {
            Debug.Log("down");
            canPlayerMove = _playerMovement.CanMove(player.transform.position, PlayerMovement.MoveDirections.Down);
            if (canPlayerMove)
            {
                _playerMovement.MovePlayer(PlayerMovement.MoveDirections.Down);
                hasPlayerMoved = true;
            }
        }

        if (left)
        {
            Debug.Log("left");
            canPlayerMove = _playerMovement.CanMove(player.transform.position, PlayerMovement.MoveDirections.Left);
            if (canPlayerMove)
            {
                _playerMovement.MovePlayer(PlayerMovement.MoveDirections.Left);
                hasPlayerMoved = true;
            }
        }

        if (right)
        {
            Debug.Log("right");
            canPlayerMove = _playerMovement.CanMove(player.transform.position, PlayerMovement.MoveDirections.Rigth);
            if (canPlayerMove)
            {
                _playerMovement.MovePlayer(PlayerMovement.MoveDirections.Rigth);
                hasPlayerMoved = true;
            }
        }
       
        
        if (hasPlayerMoved)
        {
            //trigger movement on enemies
        }
    }
}
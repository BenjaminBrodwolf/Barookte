using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BoardBuilder _boardManager;

    private GameObject _player;
    private PlayerMovement _playerMovement;

    private GameObject[] _enemies;
    private EnemyMovement[] _enemyMovements;

    // Start is called before the first frame update
    void Start()
    {
        _boardManager = GetComponentInChildren<BoardBuilder>();
        _boardManager.buildBoard();

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _enemyMovements = new EnemyMovement[_enemies.Length];
        for (var index = 0; index < _enemies.Length; index++)
        {
            var enemy = _enemies[index];
            _enemyMovements[index] = enemy.GetComponent<EnemyMovement>();
        }
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
        var newPlayerPosition = Vector3.zero;

        if (up)
        {
            Debug.Log("up");
            canPlayerMove = _playerMovement.CanMove(_player.transform.position, PlayerMovement.MoveDirections.Up);
            if (canPlayerMove)
            {
                newPlayerPosition = _playerMovement.MovePlayer(PlayerMovement.MoveDirections.Up);
                hasPlayerMoved = true;
            }
        }

        if (down)
        {
            Debug.Log("down");
            canPlayerMove = _playerMovement.CanMove(_player.transform.position, PlayerMovement.MoveDirections.Down);
            if (canPlayerMove)
            {
                newPlayerPosition = _playerMovement.MovePlayer(PlayerMovement.MoveDirections.Down);
                hasPlayerMoved = true;
            }
        }

        if (left)
        {
            Debug.Log("left");
            canPlayerMove = _playerMovement.CanMove(_player.transform.position, PlayerMovement.MoveDirections.Left);
            if (canPlayerMove)
            {
                newPlayerPosition = _playerMovement.MovePlayer(PlayerMovement.MoveDirections.Left);
                hasPlayerMoved = true;
            }
        }

        if (right)
        {
            Debug.Log("right");
            canPlayerMove = _playerMovement.CanMove(_player.transform.position, PlayerMovement.MoveDirections.Rigth);
            if (canPlayerMove)
            {
                newPlayerPosition = _playerMovement.MovePlayer(PlayerMovement.MoveDirections.Rigth);
                hasPlayerMoved = true;
            }
        }
        
        if (hasPlayerMoved)
        {
            for (var index = 0; index < _enemies.Length; index++)
            {
                var moved = _enemyMovements[index].MoveEnemy(newPlayerPosition);
                
                if (Math.Abs(moved.x - newPlayerPosition.x) < 0.01 && Math.Abs(moved.z - newPlayerPosition.z) < 0.01)
                {
                    Debug.Log("Game Over!");
                }
            }
        }
    }
}
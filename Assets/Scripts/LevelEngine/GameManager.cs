using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    // private BoardBuilder boardManager;
    private LevelGenerator levelGenerator;
    
    private GameObject player;
    private PlayerMovement playerMovement;

    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        levelGenerator = GetComponentInChildren<LevelGenerator>();
        levelGenerator.GenerateLevel();

        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        
    }

    // Update is called once per frame
    private void Update()
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
            canPlayerMove = playerMovement.CanMove(player.transform.position, PlayerMovement.Directions.Up);
            if (canPlayerMove)
            {
                newPlayerPosition = playerMovement.MovePlayer(PlayerMovement.Directions.Up);
                hasPlayerMoved = true;
            }
        }

        if (down)
        {
            // Debug.Log("down");
            canPlayerMove = playerMovement.CanMove(player.transform.position, PlayerMovement.Directions.Down);
            if (canPlayerMove)
            {
                newPlayerPosition = playerMovement.MovePlayer(PlayerMovement.Directions.Down);
                hasPlayerMoved = true;
            }
        }

        if (left)
        {
            // Debug.Log("left");
            canPlayerMove = playerMovement.CanMove(player.transform.position, PlayerMovement.Directions.Left);
            if (canPlayerMove)
            {
                newPlayerPosition = playerMovement.MovePlayer(PlayerMovement.Directions.Left);
                hasPlayerMoved = true;
            }
        }

        if (right)
        {
            // Debug.Log("right");
            canPlayerMove = playerMovement.CanMove(player.transform.position, PlayerMovement.Directions.Rigth);
            if (canPlayerMove)
            {
                newPlayerPosition = playerMovement.MovePlayer(PlayerMovement.Directions.Rigth);
                hasPlayerMoved = true;
            }
        }
       
       
        if (hasPlayerMoved)
        {
            var newEnemyPositions = new List<Vector2>();
            foreach (var enemy in enemies)
            {
                var enemyMovement = enemy.GetComponent<EnemyMovement>();
                var moved = enemyMovement.MoveEnemy(newPlayerPosition, newEnemyPositions);
                newEnemyPositions.Add(new Vector2(moved.x, moved.z));
                Debug.Log($"{enemy} moved");
            }
            
        }
    }
}
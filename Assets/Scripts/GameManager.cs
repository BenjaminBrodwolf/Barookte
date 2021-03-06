﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameLevel;
    private GameObject player;
    private PlayerMovement playerMovement;
    private Camera playerCamera;
    public double animationAccuracy = 0.05;

    private Dictionary<GameObject, EnemyMovement> enemies;


    // UI
    private UIManager uiManager;

    public List<string> gameLevels;
    void Start()
    {
        playerCamera = Camera.main;
        gameLevel = GameObject.FindGameObjectWithTag("GameLevel");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera.transform.position += player.transform.position;
        playerCamera.transform.SetParent(player.transform);
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = true;
        var enemiesGOs = GameObject.FindGameObjectsWithTag("Enemy");
        this.enemies = new Dictionary<GameObject, EnemyMovement>();
        foreach (var enemy in enemiesGOs)
        {
            var enemyScript = enemy.GetComponent<EnemyMovement>();
            enemyScript.enabled = true;
            this.enemies.Add(enemy, enemyScript);
        }

        // UI
        uiManager = FindObjectOfType<UIManager>();

        // WaitForSecondsFunction(10);
        
        EndBlackout();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            uiManager.StartBlackout();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            uiManager.EndBlackout();
        }

        // if (playerMovement.isAlreadyMoving)
        // {
        //     return;
        // }

        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);

        var hasPlayerMoved = false;
        var canPlayerMove = false;
        
        if (up)
        {
            Debug.Log("up");
            canPlayerMove = playerMovement.CanMove(PlayerMovement.Directions.Up);
            if (canPlayerMove)
            {
                playerMovement.UpdatePosition(PlayerMovement.Directions.Up);
                hasPlayerMoved = true;
            }
        }

        if (down)
        {
            // Debug.Log("down");
            canPlayerMove = playerMovement.CanMove(PlayerMovement.Directions.Down);
            if (canPlayerMove)
            {
                playerMovement.UpdatePosition(PlayerMovement.Directions.Down);
                hasPlayerMoved = true;
            }
        }

        if (left)
        {
            // Debug.Log("left");
            canPlayerMove = playerMovement.CanMove(PlayerMovement.Directions.Left);
            if (canPlayerMove)
            {
                playerMovement.UpdatePosition(PlayerMovement.Directions.Left);
                hasPlayerMoved = true;
            }
        }

        if (right)
        {
            // Debug.Log("right");
            canPlayerMove = playerMovement.CanMove(PlayerMovement.Directions.Rigth);
            if (canPlayerMove)
            {
                playerMovement.UpdatePosition(PlayerMovement.Directions.Rigth);
                hasPlayerMoved = true;
            }
        }


        if (hasPlayerMoved)
        {
            EnemyTurn();
        }
    }

    public void EnemyTurn()
    {
        Debug.Log("enemy need to be moved");
        var enemyPo = new List<Vector2>();
        foreach (var enemy in enemies)
        {
            var newEnemyPosition = enemy.Value.UpdateEnemyPosition(playerMovement.TurnPosition, enemyPo);
            enemyPo.Add(new Vector2(newEnemyPosition.x, newEnemyPosition.z));
            Debug.Log($"{enemy} moved");
        }
    }

    // UI
    public void StartBlackout() => uiManager.StartBlackout();
    public void EndBlackout() => uiManager.EndBlackout();


    public void WaitForSeconds(int seconds) =>
        StartCoroutine(ExampleCoroutine(seconds));
    
    IEnumerator ExampleCoroutine(int seconds )
    {
        yield return new WaitForSeconds(seconds);
  
    }
}
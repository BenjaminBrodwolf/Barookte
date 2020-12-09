using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameLevel;
    private GameObject player;
    private PlayerMovement playerMovement;
    public Camera playerCamera;
    public double animationAccuracy = 0.05;

    private Dictionary<GameObject, EnemyMovement> enemies;


    // UI
    private UIManager uiManager;


    void Start()
    {
        gameLevel = GameObject.FindGameObjectWithTag("GameLevel");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera.transform.SetParent(player.transform);
        playerMovement = player.GetComponent<PlayerMovement>();
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        this.enemies = new Dictionary<GameObject, EnemyMovement>();
        foreach (var enemy in enemies)
        {
            this.enemies.Add(enemy, enemy.GetComponent<EnemyMovement>());
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


    public void WaitForSecondsFunctionAndRestart(int seconds) =>
        StartCoroutine(ExampleCoroutine(seconds));
    
    IEnumerator ExampleCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Baröökte_Scene1"); //Load scene called Game.
        EndBlackout();
    }
}
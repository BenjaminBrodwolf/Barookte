using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveableItem : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private bool playerTrigger;
    private Vector3 turnPosition;
    
    //For Animation
    private bool isAnimating = false;
    public float speed = 10f;
    
    
    public bool IsPlayerInTriggerToItem() => playerTrigger;
    
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene ();
        
        if (currentScene.name != "LevelBuilder")
        {
            rigidbody.isKinematic = false;  //myPrefab.GetComponent<Rigidbody>().isKinematic;
        }

        turnPosition = rigidbody.position;
    }
    
    private void Update()
    {
        if (isAnimating)
        {
            isAnimating = !UpdatePositionEveryFrame();
            
            //stop animating if they fall of the map
            if (rigidbody.position.y < 1)
            {
                isAnimating = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Trigger with Player !");
            playerTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = false;
        }
    }
    
    public void PushItemInDirection(Vector3 newDirection)
    {
        if (!Physics.Raycast(turnPosition, newDirection, 1f))
        {
            turnPosition = turnPosition + newDirection;
            isAnimating = true;
        }
    }

    private bool UpdatePositionEveryFrame(double animationAccuracy = 0.05)
    {
        var rigidbodyPosition = rigidbody.position;
        var moveDirection = turnPosition - rigidbodyPosition;
        var deltaMovement = moveDirection * (speed * Time.deltaTime);
        rigidbody.MovePosition(rigidbodyPosition + deltaMovement);
        
        return (rigidbody.transform.position - turnPosition).magnitude < animationAccuracy;
    }

   
}
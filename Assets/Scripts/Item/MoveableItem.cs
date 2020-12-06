using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveableItem : MonoBehaviour
{
    private bool playerTrigger;
    private Vector3 turnPosition;

    //For Animation
    private bool isAnimating = false;
    public float speed = 10f;


    public bool IsPlayerInTriggerToItem() => playerTrigger;

    private void Start()
    {
        turnPosition = transform.position;
    }

    private void Update()
    {
        if (isAnimating)
        {
            isAnimating = UpdatePositionEveryFrame();

            //stop animating if they fall of the map
            if (transform.position.y < 1)
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
        var moveDirection = turnPosition - transform.position;
        var deltaMovement = moveDirection * (speed * Time.deltaTime);
        transform.position += deltaMovement;

        return (transform.position - turnPosition).magnitude >= animationAccuracy;
    }
}
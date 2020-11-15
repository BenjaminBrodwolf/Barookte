using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private bool playerTrigger;
    
    public bool IsPlayerInTriggerToItem() => playerTrigger;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger with Player !");
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
        if (!Physics.Raycast(transform.position, newDirection, 1f))
        {
            rigidbody.MovePosition(transform.position + newDirection);
        }
    }
}
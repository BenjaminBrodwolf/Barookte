using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool playerTrigger;
    
    public bool IsPlayerInTriggerToItem() => playerTrigger;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Trigger with Player !");
            playerTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerTrigger = false;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PushItemInDirection(Vector3 newDirection)
    {
        rigidbody.MovePosition(transform.position + newDirection);
    }
}
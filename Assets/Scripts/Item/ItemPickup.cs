using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool itemIsPicekdUp = false;
    private void OnTriggerEnter(Collider other)
    {
   
        if (!itemIsPicekdUp && other.CompareTag("Player"))
        {
            Debug.Log("Item picked up");
            itemIsPicekdUp = true;
            
            // Item add as ChildObject to Player
            gameObject.transform.parent = other.transform;
        }
    }
}

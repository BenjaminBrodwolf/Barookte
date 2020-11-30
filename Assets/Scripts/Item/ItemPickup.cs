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
            Debug.Log(other.gameObject); // player(clone) Gameobject
            itemIsPicekdUp = true;

            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            // disable the Colliders
            foreach (var boxCollider in gameObject.GetComponents<BoxCollider>())
            {
                boxCollider.enabled = false;
            }

            // Item add as ChildObject to Player
            gameObject.transform.parent = other.transform;


            // Destroy(gameObject);
        }
    }
}

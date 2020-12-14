using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RaycastBuildingSystem : MonoBehaviour
{
    private GameObject BuildedLevel;
    private GameObject ObjToBuild;
    public BuildingObject[] buildingdObjects;

    public LayerMask mask;
    int LastPosX, LastPosY, LastPosZ;
    Vector3 mousePos;

    // Layer Mask
    // private int Earth = 8;
    private static readonly int[] LayerMask = new[]
    {
        9, //OnEarth
        10, //MovableItem
        12, //Player
        13 //Enemy
    };


    void Start()
    {
        BuildedLevel = GameObject.FindGameObjectWithTag("GameLevel");


        ObjToBuild = Instantiate(buildingdObjects[0].ObjToPlace, new Vector3(0, 0, 0), Quaternion.identity);

        foreach (var buildingObject in buildingdObjects)
        {
            buildingObject.ButtonToPlace.onClick.AddListener(() =>
            {
                Destroy(ObjToBuild);
                ObjToBuild = (GameObject) PrefabUtility.InstantiatePrefab(buildingObject.ObjToPlace);
                if (ObjToBuild.GetComponent<Rigidbody>() != null)
                {
                    ObjToBuild.GetComponent<Rigidbody>().isKinematic = true;
                    // ObjToBuild.GetComponent<Rigidbody>().detectCollisions = false;
                }
            });
        }
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;


        if (EventSystem.current.IsPointerOverGameObject()) // wenn Maus über einem UI element ist
        {
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            int posX = (int) Mathf.Round(hit.point.x);

            int posY = 0; //Mathf.Clamp((int) Mathf.Round(hit.point.y), 0, 2);

            if (LayerMask.Contains(ObjToBuild.layer))
            {
                posY = 1;
            }

            int posZ = (int) Mathf.Round(hit.point.z);

            // Debug.Log("X: " + posX + " & Y: " + posY + " & Z: " + posZ);
            if (posX != LastPosX || posY != LastPosY || posZ != LastPosZ)
            {
                LastPosX = posX;
                LastPosY = posY;
                LastPosZ = posZ;
                ObjToBuild.transform.position = new Vector3(posX, posY + .5f, posZ);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ObjToBuild.transform.Rotate(0, 90, 0);
            }

            if (Input.GetMouseButtonDown(0)) // left mouse click
            {
                RemoveSamePositionElement();
                
                Instantiate(ObjToBuild,
                    ObjToBuild.transform.position,
                    ObjToBuild.transform.rotation,
                    BuildedLevel.transform);
            }
            else if (Input.GetMouseButtonDown(1)) // right mouse click
            {
                RemoveSamePositionElement();
            }
        }
    }

    private void RemoveSamePositionElement()
    {
        var buildedElements = BuildedLevel.transform.GetComponentsInChildren<Transform>();
        foreach (var element in buildedElements)
        {
            if (element.position == ObjToBuild.transform.position)
            {
                Destroy(element.gameObject);
            }
        }
    }
}
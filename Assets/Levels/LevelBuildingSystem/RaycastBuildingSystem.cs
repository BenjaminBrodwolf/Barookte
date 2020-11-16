using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class RaycastBuildingSystem : MonoBehaviour
{
    private GameObject ObjToBuild;
    public BuildingObject[] buildingdObjects;
    // public int ObjToPlaceIndex;

    public LayerMask mask;
    int LastPosX, LastPosY, LastPosZ;
    Vector3 mousePos;

    private GameObject eartBtn;
    private GameObject waterBtn;


    void Start()
    {
        ObjToBuild = Instantiate(buildingdObjects[0].ObjToPlace, new Vector3(0, 0.5f, 0), Quaternion.identity);

        // eartBtn = GameObject.Find("EarthBtn");
        // waterBtn = GameObject.Find("WaterBtn");

        foreach (var buildingdObject in buildingdObjects)
        {
            buildingdObject.ButtonToPlace.onClick.AddListener(() =>
            {
                Destroy(ObjToBuild);
                ObjToBuild = (GameObject) PrefabUtility.InstantiatePrefab(buildingdObject.ObjToPlace); 
            });
        }
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            int PosX = (int) Mathf.Round(hit.point.x);
            int PosY = (int) Mathf.Round(hit.point.y);
            int PosZ = (int) Mathf.Round(hit.point.z);

//			Debug.Log("X: " + PosX + " & Z: " + PosZ);
            if (PosX != LastPosX || PosY != LastPosY || PosZ != LastPosZ)
            {
                LastPosX = PosX;
                LastPosY = PosY;
                LastPosZ = PosZ;
                ObjToBuild.transform.position = new Vector3(PosX, PosY + .5f, PosZ);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Object created");
                Instantiate(ObjToBuild, 
                    ObjToBuild.transform.position,
                    Quaternion.identity);
            }
        }
    }
    
}
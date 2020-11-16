using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class RaycastBuildingSystem : MonoBehaviour
{
    public GameObject BuildedLevel;
    private GameObject ObjToBuild;
    public BuildingObject[] buildingdObjects;

    public LayerMask mask;
    int LastPosX, LastPosY, LastPosZ;
    Vector3 mousePos;
    
    
    void Start()
    {
        ObjToBuild = Instantiate(buildingdObjects[0].ObjToPlace, new Vector3(0, 0.5f, 0), Quaternion.identity);

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
        
        // Debug.DrawLine(transform.position, mousePos , Color.red);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            int posX = (int) Mathf.Round(hit.point.x);
            int posY = (int) Mathf.Round(hit.point.y);
            int posZ = (int) Mathf.Round(hit.point.z);

//			Debug.Log("X: " + PosX + " & Z: " + PosZ);
            if (posX != LastPosX || posY != LastPosY || posZ != LastPosZ)
            {
                LastPosX = posX;
                LastPosY = posY;
                LastPosZ = posZ;
                ObjToBuild.transform.position = new Vector3(posX, posY + .5f, posZ);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Object created");
                Instantiate(ObjToBuild, 
                    ObjToBuild.transform.position,
                    ObjToBuild.transform.rotation, 
                    BuildedLevel.transform);
            }
        }
    }
    
}
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
    
    // Layer Mask
    // private int Earth = 8;
    private int OnEarth = 9;
    private int MoveAbleItem = 10;

    private GameObject buildedLevel;
    
    void Start()
    {
        buildedLevel = GameObject.Find("BuildedLevel");
    
        
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

        if (EventSystem.current.IsPointerOverGameObject()) // wenn Maus über einem UI element ist
        {
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            int posX = (int) Mathf.Round(hit.point.x);

            int posY = 0;  //Mathf.Clamp((int) Mathf.Round(hit.point.y), 0, 2);

            if (ObjToBuild.layer == MoveAbleItem || ObjToBuild.layer == OnEarth)
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
                ObjToBuild.transform.Rotate(0,90,0);
            } 

            if ( Input.GetMouseButtonDown(0))
            {
                // Debug.Log("Object created");
                // private LayerMask earth = LayerMask.GetMask("Earth");

                // if (Physics.Raycast(ray, out hit, Mathf.Infinity,
                //     LayerMask.GetMask("Earth", "OnEarth", "MoveableItem")))
                // {
                //     Debug.Log( hit.collider.gameObject.name);
                //
                // }
                //     
                // Debug.Log( "Log earth: " );
                // Debug.Log( "Log MoveableItem: " );
                    
                // RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity,LayerMask.GetMask("Earth", "OnEarth", "MoveableItem"));
                //
                // foreach (var hitElement in hits)
                // {
                //     Debug.Log(hits.Length.ToString());
                //     Debug.Log(hitElement.collider.transform.position.ToString());
                //     Debug.Log(hitElement.collider.name + " id: "  + hitElement.collider.GetInstanceID().ToString());
                // }

                var buildedElements = buildedLevel.transform.GetComponentsInChildren<Transform>();
                foreach (var element in buildedElements)
                {
                    if (element.position == ObjToBuild.transform.position)
                    {
                        Destroy(element.gameObject);
                    }
                }
                
                Instantiate(ObjToBuild, 
                    ObjToBuild.transform.position,
                    ObjToBuild.transform.rotation, 
                    BuildedLevel.transform);
            }
        }
    }
    
}
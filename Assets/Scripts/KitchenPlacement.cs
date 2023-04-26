using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KitchenPlacement : MonoBehaviour
{
    public UserMovement userInput;
    public PlaceObject placeObjScript;
    float wallThickness = 0.5f;
    float wallHeightScale = 2.4f;
    public GameObject objectToPlace;
    public List<GameObject> kitchenObjsPlaced = new List<GameObject>();
    public bool rotKitchenUnit = false;
    public Transform raycastPoint;
    



    public List<GameObject> tempList;

    private bool initialisedRoom = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(userInput.kitchenPlacementScene)
        {
            if (!initialisedRoom)
            {
                initialisedRoom = initialiseRoom();
                Cursor.lockState = CursorLockMode.Confined;
            }
            objectToPlace = userInput.currentKitchenUnit;
            if (!userInput.kitchenUnitPlaced)
            {
                ghostUnitPlacement();
            }
        }
        if (Input.GetMouseButtonDown(0) && userInput.ghostKitchenPlacement)
        {
            userInput.kitchenUnitPlaced = true;
        }
    }

    public bool initialiseRoom()
    {
        string axis = "x";
        tempList = sortWallsList(placeObjScript.walls, axis);
        tempList[0].name = "Left";
        tempList[3].name = "Right";
        axis = "z";
        tempList = sortWallsList(placeObjScript.walls, axis);
        tempList[0].name = "Bottom";
        tempList[3].name = "Top";
        placeObjScript.walls = tempList;


        Debug.Log("initialising");
        float topX = 0f;
        float bottomX = 0f;
        float LeftZ = 0f;
        float rightZ = 0f;

        foreach(GameObject Wall in placeObjScript.walls)
        {
            if(Wall.name == "Top")
            {
                Wall.transform.position = new Vector3(Wall.transform.position.x, Wall.transform.position.y, Wall.transform.position.z + wallThickness / 2);
                topX = Wall.transform.position.x;
            }
            else if (Wall.name == "Bottom")
            {
                Wall.transform.position = new Vector3(Wall.transform.position.x, Wall.transform.position.y, Wall.transform.position.z - wallThickness / 2);
                bottomX = Wall.transform.position.x;
            }
            else if (Wall.name == "Left")
            {
                Wall.transform.position = new Vector3(Wall.transform.position.x + wallThickness/2, Wall.transform.position.y, Wall.transform.position.z);
                LeftZ = Wall.transform.position.z;
            }
            else if (Wall.name == "Right")
            {
                Wall.transform.position = new Vector3(Wall.transform.position.x - wallThickness / 2, Wall.transform.position.y, Wall.transform.position.z);
                rightZ = Wall.transform.position.z;
            }
            Wall.transform.localScale = new Vector3(Wall.transform.localScale.x, Wall.transform.localScale.y * wallHeightScale, wallThickness);
        }

        //this.transform.position = new Vector3((topX + bottomX) / 2, this.transform.position.y - 7, (LeftZ + rightZ) / 2);
        
        return true;
    }

    public List<GameObject> sortWallsList(List<GameObject> walls, string axis)
    {
        if (axis == "x")
        {
            return walls.OrderBy(x => x.transform.position.x).ToList();
        }
        else if(axis == "z")
        {
            return walls.OrderBy(x => x.transform.position.z).ToList();
        }
        else
        {
            return null;
        }
    }

    public void startGhostKitchenPlacement()
    {
        if (userInput.currentKitchenUnit != null)
        {
            userInput.kitchenUnitPlaced = false;
            userInput.ghostKitchenPlacement = true;
            
            kitchenObjsPlaced.Add(Instantiate(objectToPlace));
        }
    }


    public void ghostUnitPlacement()
    {
        if (userInput.ghostKitchenPlacement)
        {
            if (kitchenObjsPlaced != null)
            {
                RaycastHit hit;
                Cursor.lockState = CursorLockMode.Locked;

                if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out hit))
                {
                    Debug.Log(hit.point);
                    Quaternion rotation = Quaternion.identity;
                    Vector3 ghostPos = new Vector3(hit.point.x, objectToPlace.transform.position.y + (objectToPlace.transform.localScale.y/2), hit.point.z);
                    if (rotKitchenUnit)
                    {
                        Debug.Log("rotated object");
                        rotation = new Quaternion(Quaternion.identity.x, Quaternion.identity.y + 1f, Quaternion.identity.z, Quaternion.identity.w);
                    }
                    kitchenObjsPlaced[kitchenObjsPlaced.Count - 1].transform.position = ghostPos;
                    kitchenObjsPlaced[kitchenObjsPlaced.Count - 1].transform.rotation = rotation;
                }
            }
            
        }
    }
}

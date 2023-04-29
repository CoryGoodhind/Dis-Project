using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KitchenPlacement : MonoBehaviour
{
    public UserMovement userInput;
    public PlaceObject placeObjScript;
    float wallThickness = 0.05f;
    float wallHeightScale = 2.4f;
    public GameObject objectToPlace;
    
    public bool rotKitchenUnit = false;
    public Transform raycastPoint;
    public Transform baseFloor;
    private GameObject lastItem;
    float cabDepth = 0.3f;
    float cabWidth = 0.3f;
    



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
            if (!userInput.collisionWithUnit)
            {

                userInput.kitchenUnitPlaced = true;
                Cursor.lockState = CursorLockMode.Confined;
                userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].GetComponent<KitchenCabinetHandle>().wallPlacement = userInput.lastWallKitchenUnit.transform.name;
                userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].GetComponent<KitchenCabinetHandle>().activeUnit = false;
            }
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

        foreach (GameObject Wall in placeObjScript.walls)
        {

            if (Wall.name == "Top")
            {

                topX = Wall.transform.position.x;
                Debug.Log(topX);
            }
            else if (Wall.name == "Bottom")
            {
                bottomX = Wall.transform.position.x;
            }
            else if (Wall.name == "Left")
            {
                LeftZ = Wall.transform.position.z;
            }
            else if (Wall.name == "Right")
            {
                rightZ = Wall.transform.position.z;
            }
            Wall.transform.localScale = new Vector3(Wall.transform.localScale.x, Wall.transform.localScale.y * wallHeightScale, wallThickness);
        }

        this.transform.position = new Vector3((topX + bottomX) / 2, this.transform.position.y - 5, (LeftZ + rightZ) / 2);
        this.transform.rotation = Quaternion.Euler(90f, 0f, 0f);


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
            
            userInput.kitchenObjsPlaced.Add(Instantiate(objectToPlace));
        }
    }


    public void ghostUnitPlacement()
    {
        if (userInput.ghostKitchenPlacement)
        {
            userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].GetComponent<KitchenCabinetHandle>().activeUnit = true;
            if (userInput.kitchenObjsPlaced != null)
            {
                
                RaycastHit hit;
                Cursor.lockState = CursorLockMode.Locked;

                if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out hit))
                {
                    selectItem(hit);
                    //Debug.Log(hit.point);

                    Quaternion rotation = Quaternion.identity;
                    Vector3 ghostPos = new Vector3(hit.point.x, 0.5f, hit.point.z); ;
                    if (userInput.lastWallKitchenUnit != null)
                    {
                        if (!userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].GetComponent<KitchenCabinetHandle>().collisionDet)
                        {
                            if (userInput.lastWallKitchenUnit.transform.name == "Top")
                            {
                                ghostPos = new Vector3(hit.point.x, 0.5f, userInput.lastWallKitchenUnit.transform.position.z - cabDepth);
                                userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].transform.rotation = Quaternion.Euler(Quaternion.identity.x, 180f, Quaternion.identity.z);
                            }
                            else if (userInput.lastWallKitchenUnit.transform.name == "Bottom")
                            {
                                //rotation = new Quaternion(Quaternion.identity.x, 0, Quaternion.identity.z, Quaternion.identity.w);
                                ghostPos = new Vector3(hit.point.x, 0.5f, userInput.lastWallKitchenUnit.transform.position.z + cabDepth);
                                userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].transform.rotation = Quaternion.Euler(Quaternion.identity.x, 0f, Quaternion.identity.z);
                            }
                            else if (userInput.lastWallKitchenUnit.transform.name == "Left")
                            {
                                //rotation = new Quaternion(Quaternion.identity.x, 1, Quaternion.identity.z, Quaternion.identity.w);
                                ghostPos = new Vector3(userInput.lastWallKitchenUnit.transform.position.x + cabWidth, 0.5f, hit.point.z);
                                userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].transform.rotation = Quaternion.Euler(Quaternion.identity.x, 90f, Quaternion.identity.z);
                            }
                            else if (userInput.lastWallKitchenUnit.transform.name == "Right")
                            {
                                //rotation = new Quaternion(Quaternion.identity.x, -1, Quaternion.identity.z, Quaternion.identity.w);
                                ghostPos = new Vector3(userInput.lastWallKitchenUnit.transform.position.x - cabWidth, 0.5f, hit.point.z);
                                userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].transform.rotation = Quaternion.Euler(Quaternion.identity.x, -90f, Quaternion.identity.z);
                            }
                            userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].transform.position = ghostPos;
                        }
                        //userInput.kitchenObjsPlaced[userInput.kitchenObjsPlaced.Count - 1].transform.rotation = rotation;
                    }
                }
            }
            
        }
    }

    void selectItem(RaycastHit hit)
    {
        if (hit.transform.tag == "Wall")
        {
            if (userInput.lastWallKitchenUnit != null)
            {
                //lastItem.transform.gameObject.GetComponentInChildren<Renderer>().material.color = baseItemColor;
            }
            userInput.lastWallKitchenUnit = hit.transform.gameObject;
            //hit.transform.gameObject.GetComponentInChildren<Renderer>().material.color = highlightItemColor;
            //userInput.ghostMode = false;
            //Cursor.lockState = CursorLockMode.Confined;
        }
    }
}

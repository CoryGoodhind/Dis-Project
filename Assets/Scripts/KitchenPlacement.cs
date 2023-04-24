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
    public bool rotKitchenUnit = false;

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

        this.transform.position = new Vector3((topX + bottomX) / 2, this.transform.position.y - 7, (LeftZ + rightZ) / 2);
        
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

    public void placeKitchenUnit(GameObject kitchenUnit)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ghostUnitPlacement()
    {
        objectToPlace.SetActive(true);
        RaycastHit hit;
        Cursor.lockState = CursorLockMode.Confined;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Quaternion rotation = Quaternion.identity;
            Vector3 ghostPos = new Vector3(Mathf.Round(hit.point.x * 10f) / 10f, Mathf.RoundToInt(objectToPlace.transform.position.y), Mathf.RoundToInt(hit.point.z * 10f) / 10f);
            if (rotKitchenUnit)
            {
                Debug.Log("rotated object");
                rotation = new Quaternion(Quaternion.identity.x, Quaternion.identity.y + 1f, Quaternion.identity.z, Quaternion.identity.w);
            }
            objectToPlace.transform.position = ghostPos;
            objectToPlace.transform.rotation = rotation;
        }
        
    }


}

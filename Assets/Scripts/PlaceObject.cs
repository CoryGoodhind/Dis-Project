using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public Transform raycasePoint;
    public GameObject itemToPlace;
    public List<GameObject> walls = new List<GameObject>();
    public int maxWalls = 4;
    public GameObject lastItem;
    public Color baseItemColor;
    public Color highlightItemColor;
    public UserMovement userInput;
    public Wall wallSpawner;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && userInput.ghostMode)
        {
            placeItem();
        }
        if (Input.GetMouseButtonDown(1) && userInput.ghostMode)
        {
            destroyItem();
        }
        //selectItem();
    }
    void placeItem()
    {
        if(Physics.Raycast(raycasePoint.position, raycasePoint.forward, out RaycastHit hit))
        {
            if (userInput.wallScene)
            {
                if (hit.transform.tag == "Wall")
                {
                    selectItem(hit);
                }
                else
                {
                    if (walls.Count < maxWalls)
                    {
                        Vector3 spawnLoc = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z));
                        Quaternion rotation = Quaternion.identity;
                        if (userInput.rotateObject)
                        {
                            rotation = new Quaternion(Quaternion.identity.x, Quaternion.identity.y + 1f, Quaternion.identity.z, Quaternion.identity.w);
                        }
                        if (spawnLoc.y <= 1)
                        {
                            walls.Add(wallSpawner.placeObj(spawnLoc, rotation));
                            //walls.Add(wallSpawner.placeObj(spawnLoc, rotation));
                            if (lastItem != null)
                            {
                                lastItem.transform.gameObject.GetComponentInChildren<Renderer>().material.color = baseItemColor;
                            }
                        }
                    }
                }
            }
        }
    }
    void destroyItem()
    {
        
        if (Physics.Raycast(raycasePoint.position, raycasePoint.forward, out RaycastHit hit))
        {
            if (hit.transform.tag == "Wall")
            {

                for (int i = 0; i < walls.Count; i++)
                {
                    if (walls[i].gameObject == hit.transform.gameObject)
                    {
                        walls.RemoveAt(i);
                    }
                }
                Destroy(hit.transform.gameObject);
            }
        }
    }
    
    void selectItem(RaycastHit hit)
    {
        if (hit.transform.tag == "Wall")
        {
            if (lastItem != null)
            {
                lastItem.transform.gameObject.GetComponentInChildren<Renderer>().material.color = baseItemColor;
            }
            lastItem = hit.transform.gameObject;
            Debug.Log("HIT");
            hit.transform.gameObject.GetComponentInChildren<Renderer>().material.color = highlightItemColor;
            userInput.ghostMode = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCabinetHandle : MonoBehaviour
{
    public UserMovement user;
    public bool collisionDet = false;
    public bool activeUnit = false;
    public string wallPlacement = null;
    public string secondaryWallPlacement = "notSet";
    private float cabWidth = 0.3f;
    public Transform raycastPoint;
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;

    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.FindGameObjectWithTag("Player").GetComponent<UserMovement>();
        raycastPoint = GameObject.FindGameObjectWithTag("raycastPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeUnit)
        {
            collisionDet = checkCollisions();
        }
    }


    public bool checkCollisions()
    {
        foreach (GameObject Wall in user.walls)
        {
            if(Wall.transform.name == "Top")
            {
                topWall = Wall;
            }
            else if (Wall.transform.name == "Bottom")
            {
                bottomWall = Wall;
            }
            else if (Wall.transform.name == "Left")
            {
                leftWall = Wall;
            }
            else if (Wall.transform.name == "Right")
            {
                rightWall = Wall;
            }
        }
        foreach(GameObject cabinet in user.kitchenObjsPlaced)
        {
            
            if(user.lastWallKitchenUnit.name == cabinet.GetComponent<KitchenCabinetHandle>().wallPlacement || user.lastWallKitchenUnit.name == cabinet.GetComponent<KitchenCabinetHandle>().secondaryWallPlacement)
            {
                Debug.Log(cabinet.GetComponentInChildren<Transform>().name);
                if(cabinet.GetComponentInChildren<Transform>().name == "CornerCabinet(Clone)")
                {
                    cabWidth = 0.65f;
                }
                else
                {
                    cabWidth = 0.3f;
                }
                RaycastHit hit;
                if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out hit))
                {
                    if (user.lastWallKitchenUnit.name == "Top")
                    {
                        if (hit.point.x + cabWidth >= cabinet.transform.position.x - cabWidth && hit.point.x - cabWidth <= cabinet.transform.position.x + cabWidth && cabinet.transform != this.transform)
                        {
                            return true;
                        }
                        if(hit.point.x + cabWidth >= rightWall.transform.position.x)
                        {
                            return true;
                        }
                    }
                    if (user.lastWallKitchenUnit.name == "Bottom")
                    {
                        if (hit.point.x + cabWidth >= cabinet.transform.position.x - cabWidth && hit.point.x - cabWidth <= cabinet.transform.position.x + cabWidth && cabinet.transform != this.transform)
                        {
                            return true;
                        }
                    }
                    if (user.lastWallKitchenUnit.name == "Left")
                    {
                        if (hit.point.z + cabWidth >= cabinet.transform.position.z - cabWidth && hit.point.z - cabWidth <= cabinet.transform.position.z + cabWidth && cabinet.transform != this.transform)
                        {
                            return true;
                        }
                    }
                    if (user.lastWallKitchenUnit.name == "Right")
                    {
                        if (hit.point.z + cabWidth >= cabinet.transform.position.z - cabWidth && hit.point.z - cabWidth <= cabinet.transform.position.z + cabWidth && cabinet.transform != this.transform)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        
        return false;
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCabinetHandle : MonoBehaviour
{
    public UserMovement user;
    public bool collisionDet = false;
    public bool activeUnit = false;
    public string wallPlacement = null;
    private float cabWidth = 0.3f;
    public Transform raycastPoint;
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
        foreach(GameObject cabinet in user.kitchenObjsPlaced)
        {
            if (cabinet.transform != this.transform)
            {
                if(user.lastWallKitchenUnit.name == cabinet.GetComponent<KitchenCabinetHandle>().wallPlacement)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out hit))
                    {
                        if (user.lastWallKitchenUnit.name == "Top")
                        {
                            Debug.Log(this.transform.position.x + this.transform.localScale.x / 2);
                            Debug.Log(cabinet.transform.position.x - cabinet.transform.localScale.x / 2);
                            if (hit.point.x + cabWidth >= cabinet.transform.position.x - cabWidth && hit.point.x - cabWidth <= cabinet.transform.position.x + cabWidth)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        
        return false;
    }


}

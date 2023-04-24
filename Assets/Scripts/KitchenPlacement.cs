using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenPlacement : MonoBehaviour
{
    public UserMovement userInput;
    public PlaceObject placeObjScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialiseRoom()
    {
        GameObject lowestX;
        GameObject lowestY;
        GameObject highestX;
        GameObject highestY;

        foreach(GameObject Wall in placeObjScript.walls)
        {
            for(int i = 0; i < placeObjScript.walls.Count; i++)
            {
                if(Wall.transform.position.x <= placeObjScript.walls[i].transform.position.x)
                {
                    Wall.GetComponent<WallInfo>().direction = WallInfo.WallDirection.Left;
                }
            }
        }
        
    }
}

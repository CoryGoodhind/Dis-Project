using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSelectionScene : MonoBehaviour
{
    public UserMovement userInput;
    public List<GameObject> inUseWalls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (userInput.wallSelectorScene)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        
    }

    bool selectItem(RaycastHit hit)
    {
        if (hit.transform.tag == "Wall")
        {
            foreach(GameObject wall in inUseWalls)
            {
                if(wall.transform == hit.transform)
                {

                }
                else
                {
                    inUseWalls.Add(hit.transform.gameObject);

                }
            }
            userInput.lastWallKitchenUnit = hit.transform.gameObject;
            return true;
        }
        return true;
    }
}

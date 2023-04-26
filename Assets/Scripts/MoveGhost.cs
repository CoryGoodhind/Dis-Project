using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGhost : MonoBehaviour
{
    public GameObject ghostObject;
    public UserMovement userInput;
    public Transform raycastPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(userInput.ghostMode)
        {
            ghostObject.SetActive(true);
            RaycastHit hit;

            if (Physics.Raycast(raycastPoint.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Quaternion rotation = Quaternion.identity;
                Vector3 ghostPos = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(ghostObject.transform.position.y), Mathf.RoundToInt(hit.point.z));
                if (userInput.rotateObject)
                {
                    rotation = new Quaternion(Quaternion.identity.x, Quaternion.identity.y + 1f, Quaternion.identity.z, Quaternion.identity.w);
                }
                ghostObject.transform.position = ghostPos;
                ghostObject.transform.rotation = rotation;
            }
            
        }
        else
        {
            ghostObject.SetActive(false);
        }

    }
}

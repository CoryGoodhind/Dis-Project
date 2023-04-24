using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIController : MonoBehaviour
{
    public UserMovement userInput;
    public GameObject wallCustoms;
    public GameObject kitchenPlacement;
    public PlaceObject wallInfo;
    public List<Transform> inputBoxList = new List<Transform>();
    public string widthText;
    public float width;
    public float length;
    public float posLeft;
    public float posDown;
    public bool setBoxYet = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        wallCustoms.SetActive(false);
        kitchenPlacement.SetActive(false);
        foreach (Transform eachChild in wallCustoms.transform)
        {
            if (eachChild.tag == "InputBox")
            {
                inputBoxList.Add(eachChild);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wallInfo.lastItem != null)
        {
            width = wallInfo.lastItem.transform.localScale.x;
            length = wallInfo.lastItem.transform.localScale.z;
            posLeft = wallInfo.lastItem.transform.localPosition.x;
            posDown = wallInfo.lastItem.transform.localPosition.z;
        }


        if (userInput.ghostMode == false)
        {
            wallCustoms.SetActive(true);
            if (!setBoxYet)
            {
                setBox();
            }

        }
        if (userInput.ghostMode == true)
        {
            setBoxYet = false;
            wallCustoms.SetActive(false);
        }
        
    }

    public void wallInputController()
    {
        float.TryParse(inputBoxList[0].GetComponentInChildren<InputField>().text, out width);
        float.TryParse(inputBoxList[1].GetComponentInChildren<InputField>().text, out length);
        float.TryParse(inputBoxList[2].GetComponentInChildren<InputField>().text, out posLeft);
        float.TryParse(inputBoxList[3].GetComponentInChildren<InputField>().text, out posDown);
        foreach (GameObject wall in wallInfo.walls)
        {
            if(wall == wallInfo.lastItem)
            {
                wall.transform.position = new Vector3(posLeft, wallInfo.lastItem.transform.localPosition.y, posDown);
                wall.transform.localScale = new Vector3(width, wallInfo.lastItem.transform.localScale.y, length);
            }
        }
    }

    public void kitchenPlacementController()
    {
        if(userInput.kitchenPlacementScene)
        {
            kitchenPlacement.SetActive(true);
        }
        else
        {
            kitchenPlacement.SetActive(false);
        }
    }

    public void setBox()
    {
        inputBoxList[0].GetComponentInChildren<InputField>().text = width.ToString();
        inputBoxList[1].GetComponentInChildren<InputField>().text = length.ToString();
        inputBoxList[2].GetComponentInChildren<InputField>().text = posLeft.ToString();
        inputBoxList[3].GetComponentInChildren<InputField>().text = posDown.ToString();
        setBoxYet = true;
    }

    public void finishWallsScene()
    {
        userInput.ghostMode = false;
        userInput.wallScene = false;
        userInput.kitchenPlacementScene = true;
    }


}

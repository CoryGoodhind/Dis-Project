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
    public List<Transform> cabPickerDropList = new List<Transform>();
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
        foreach(Transform eachChild in kitchenPlacement.transform)
        {
            if(eachChild.tag == "DropBox")
            {
                cabPickerDropList.Add(eachChild);
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
        kitchenPlacementController();
        
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

    public void kitchenPlacementDropboxController()
    {
        
        if(cabPickerDropList[0].name == "CabinetPicker")
        {
            if (cabPickerDropList[0].GetComponent<Dropdown>().value == 0)
            {
                cabPickerDropList[1].GetComponent<Dropdown>().ClearOptions();
                userInput.currentKitchenUnit = userInput.cabinets[0];
            }
            if (cabPickerDropList[0].GetComponent<Dropdown>().value == 1 || cabPickerDropList[0].GetComponent<Dropdown>().value == 2)
            {
                List<string> dropOptions = new List<string> { "600mm", "800mm" };
                cabPickerDropList[1].GetComponent<Dropdown>().ClearOptions();
                cabPickerDropList[1].GetComponent<Dropdown>().AddOptions(dropOptions);
                if(cabPickerDropList[0].GetComponent<Dropdown>().value == 1)
                {
                    userInput.currentKitchenUnit = userInput.cabinets[8];
                }
                else
                {
                    userInput.currentKitchenUnit = userInput.cabinets[6];
                }
            }
            if (cabPickerDropList[0].GetComponent<Dropdown>().value == 3 || cabPickerDropList[0].GetComponent<Dropdown>().value == 4)
            {
                List<string> dropOptions = new List<string> { "600mm"};
                cabPickerDropList[1].GetComponent<Dropdown>().ClearOptions();
                cabPickerDropList[1].GetComponent<Dropdown>().AddOptions(dropOptions);
                if (cabPickerDropList[0].GetComponent<Dropdown>().value == 3)
                {
                    userInput.currentKitchenUnit = userInput.cabinets[7];
                }
                else
                {
                    userInput.currentKitchenUnit = userInput.cabinets[5];
                }
            }
        }
        
    }


}

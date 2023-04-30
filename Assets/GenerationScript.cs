using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerationScript : MonoBehaviour
{
    public List<GameObject> TopWallUnits;
    public List<GameObject> LeftWallUnits;
    public List<GameObject> RightWallUnits;
    public List<GameObject> BottomWallUnits;

    public List<float> TopWallGaps;
    public List<float> LeftWallGaps;
    public List<float> RightWallGaps;
    public List<float> BottomWallGaps;

    public GameObject topWall = null;
    public GameObject bottomWall = null;
    public GameObject leftWall = null;
    public GameObject rightWall = null;
    public GameObject objectToPlace = null;

    public List<float> cabSizes = new List<float> { 150f, 300f, 400f, 500f, 600f, 800f, 1000f };

    public UserMovement user;

    public bool initilised = false;
    public bool generatedCabs = false;

    public float cabWidthOne = 0f;
    public float cabWidthTwo = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (user.generateKitchen && !initilised)
        {
            foreach (GameObject Wall in user.walls)
            {
                if (Wall.transform.name == "Top")
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
            initCabsLists();
        }
        if(initilised && !generatedCabs)
        {
            generateCabGaps();
            generateCabs();
        }
    }

    public void initCabsLists()
    {
        initilised = true;
        foreach (GameObject unit in user.kitchenObjsPlaced)
        {
            if (unit.GetComponent<KitchenCabinetHandle>().wallPlacement == "Top")
            {
                TopWallUnits.Add(unit);
            }
            if (unit.GetComponent<KitchenCabinetHandle>().wallPlacement == "Bottom")
            {
                BottomWallUnits.Add(unit);
            }
            if (unit.GetComponent<KitchenCabinetHandle>().wallPlacement == "Left")
            {
                LeftWallUnits.Add(unit);
            }
            if (unit.GetComponent<KitchenCabinetHandle>().wallPlacement == "Right")
            {
                RightWallUnits.Add(unit);
            }
            if (unit.GetComponent<KitchenCabinetHandle>().secondaryWallPlacement != "notSet")
            {
                if (unit.GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Top")
                {
                    TopWallUnits.Add(unit);
                }
                if (unit.GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Bottom")
                {
                    BottomWallUnits.Add(unit);
                }
                if (unit.GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Left")
                {
                    LeftWallUnits.Add(unit);
                }
                if (unit.GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Right")
                {
                    RightWallUnits.Add(unit);
                }

            }
        }
        TopWallUnits = sortWallsList(TopWallUnits, "x");
        BottomWallUnits = sortWallsList(BottomWallUnits, "x");
        LeftWallUnits = sortWallsList(LeftWallUnits, "z");
        RightWallUnits = sortWallsList(RightWallUnits, "-z");
        if (topWall.GetComponent<WallInfo>().isbeingUsed)
        {
            if (!(TopWallUnits[0].GetComponent<KitchenCabinetHandle>().wallPlacement == "Top" && TopWallUnits[0].GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Left"))
            {
                
                TopWallUnits.Insert(0, leftWall);
            }
            if (!(TopWallUnits[TopWallUnits.Count-1].GetComponent<KitchenCabinetHandle>().wallPlacement == "Top" && TopWallUnits[TopWallUnits.Count - 1].GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Right"))
            {
                TopWallUnits.Add(rightWall);
            }
        }
        if (bottomWall.GetComponent<WallInfo>().isbeingUsed)
        {
            if (!(BottomWallUnits[0].GetComponent<KitchenCabinetHandle>().wallPlacement == "Bottom" && BottomWallUnits[0].GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Left"))
            {
                BottomWallUnits.Add(rightWall);
                
            }
            if (!(BottomWallUnits[BottomWallUnits.Count-1].GetComponent<KitchenCabinetHandle>().wallPlacement == "Bottom" && BottomWallUnits[BottomWallUnits.Count - 1].GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Right"))
            {
                BottomWallUnits.Insert(0, leftWall);
            }
        }
        if (leftWall.GetComponent<WallInfo>().isbeingUsed)
        {
            Debug.Log("left wall being used");
            if (!(LeftWallUnits[0].GetComponent<KitchenCabinetHandle>().wallPlacement == "Bottom" && LeftWallUnits[0].GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Left"))
            {
                LeftWallUnits.Insert(0, bottomWall);
            }
            if (!(LeftWallUnits[LeftWallUnits.Count - 1].GetComponent<KitchenCabinetHandle>().wallPlacement == "Top" && LeftWallUnits[LeftWallUnits.Count - 1].GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Left"))
            {
                LeftWallUnits.Add(topWall);
            }
        }
        if (rightWall.GetComponent<WallInfo>().isbeingUsed)
        {
            Debug.Log("left wall being used");
            if (!(RightWallUnits[0].GetComponent<KitchenCabinetHandle>().wallPlacement == "Top" && RightWallUnits[0].GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Right"))
            {
                RightWallUnits.Insert(0, topWall);
            }
            if (!(RightWallUnits[RightWallUnits.Count - 1].GetComponent<KitchenCabinetHandle>().wallPlacement == "Bottom" && RightWallUnits[RightWallUnits.Count - 1].GetComponent<KitchenCabinetHandle>().secondaryWallPlacement == "Right"))
            {
                RightWallUnits.Add(bottomWall);
            }
        }


    }

    public List<GameObject> sortWallsList(List<GameObject> walls, string axis)
    {
        if (axis == "x")
        {
            return walls.OrderBy(x => x.transform.position.x).ToList();
        }
        else if (axis == "z")
        {
            return walls.OrderBy(x => x.transform.position.z).ToList();
        }
        else if (axis == "-z")
        {
            return walls.OrderBy(x => -x.transform.position.z).ToList();
        }
        else
        {
            return null;
        }
    }

    public void generateCabGaps()
    {
        foreach(GameObject wall in user.walls)
        {
            if(wall.name == "Top" && wall.GetComponent<WallInfo>().isbeingUsed)
            {
                for(int i = 0; i<TopWallUnits.Count-1; i++)
                {
                    Debug.Log(TopWallUnits[i].GetComponentInParent<Transform>().localScale.z);
                    if (TopWallUnits[i].name == "CornerCabinet(Clone)")
                    {
                        cabWidthOne = 1f;
                    }
                    else if(TopWallUnits[i].GetComponentInParent<Transform>().localScale.z == 0.05f)
                    {
                        cabWidthOne = 0.05f;
                    }
                    else
                    {
                        cabWidthOne = 0.3f;
                    }
                    if (TopWallUnits[i+1].name == "CornerCabinet(Clone)")
                    {
                        cabWidthTwo = 1f;
                    }
                    else if (TopWallUnits[i + 1].GetComponentInParent<Transform>().localScale.z == 0.05f)
                    {
                        cabWidthTwo = 0.05f;
                    }
                    else
                    {
                        cabWidthTwo = 0.3f;
                    }
                    TopWallGaps.Add((TopWallUnits[i + 1].transform.position.x - cabWidthTwo) - (TopWallUnits[i].transform.position.x + cabWidthOne));
                }
            }

            if (wall.name == "Bottom" && wall.GetComponent<WallInfo>().isbeingUsed)
            {
                for (int i = 0; i < BottomWallUnits.Count - 1; i++)
                {
                    Debug.Log(BottomWallUnits[i].GetComponentInParent<Transform>().localScale.z);
                    if (BottomWallUnits[i].name == "CornerCabinet(Clone)")
                    {
                        cabWidthOne = 1f;
                    }
                    else if (BottomWallUnits[i].GetComponentInParent<Transform>().localScale.z == 0.05f)
                    {
                        cabWidthOne = 0.05f;
                    }
                    else
                    {
                        cabWidthOne = 0.3f;
                    }
                    if (BottomWallUnits[i + 1].name == "CornerCabinet(Clone)")
                    {
                        cabWidthTwo = 1f;
                    }
                    else if (BottomWallUnits[i + 1].GetComponentInParent<Transform>().localScale.z == 0.05f)
                    {
                        cabWidthTwo = 0.05f;
                    }
                    else
                    {
                        cabWidthTwo = 0.3f;
                    }
                    BottomWallGaps.Add((BottomWallUnits[i + 1].transform.position.x + cabWidthTwo) - (BottomWallUnits[i].transform.position.x - cabWidthOne));
                }
            }

            if (wall.name == "Left" && wall.GetComponent<WallInfo>().isbeingUsed)
            {
                for (int i = 0; i < LeftWallUnits.Count - 1; i++)
                {
                    Debug.Log(LeftWallUnits[i].GetComponentInParent<Transform>().localScale.z);
                    if (LeftWallUnits[i].name == "CornerCabinet(Clone)")
                    {
                        cabWidthOne = 1f;
                    }
                    else if (LeftWallUnits[i].GetComponentInParent<Transform>().localScale.z == 0.05f)
                    {
                        cabWidthOne = 0.05f;
                    }
                    else
                    {
                        cabWidthOne = 0.3f;
                    }
                    if (LeftWallUnits[i + 1].name == "CornerCabinet(Clone)")
                    {
                        cabWidthTwo = 1f;
                    }
                    else if (LeftWallUnits[i + 1].GetComponentInParent<Transform>().localScale.z == 0.05f)
                    {
                        cabWidthTwo = 0.05f;
                    }
                    else
                    {
                        cabWidthTwo = 0.3f;
                    }
                    LeftWallGaps.Add(-(((LeftWallUnits[i].transform.position.z) + cabWidthOne) - ((LeftWallUnits[i + 1].transform.position.z) - cabWidthTwo)));
                }
            }

            if (wall.name == "Right" && wall.GetComponent<WallInfo>().isbeingUsed)
            {
                for (int i = 0; i < RightWallUnits.Count - 1; i++)
                {
                    Debug.Log(RightWallUnits[i].GetComponentInParent<Transform>().localScale.z);
                    if (RightWallUnits[i].name == "CornerCabinet(Clone)")
                    {
                        cabWidthOne = 1f;
                    }
                    else if (RightWallUnits[i].GetComponentInParent<Transform>().localScale.z == 0.05f)
                    {
                        cabWidthOne = 0.05f;
                    }
                    else
                    {
                        cabWidthOne = 0.3f;
                    }
                    if (RightWallUnits[i + 1].name == "CornerCabinet(Clone)")
                    {
                        cabWidthTwo = 1f;
                    }
                    else if (RightWallUnits[i + 1].GetComponentInParent<Transform>().localScale.z == 0.05f)
                    {
                        cabWidthTwo = 0.05f;
                    }
                    else
                    {
                        cabWidthTwo = 0.3f;
                    }
                    RightWallGaps.Add((((RightWallUnits[i].transform.position.z) - cabWidthOne) - ((RightWallUnits[i + 1].transform.position.z) + cabWidthTwo)));
                }
            }
        }
        generatedCabs = true;
    }

    public void generateCabs()
    {
        int drawerCount = user.amountOfDrawers;
        int storageNum = user.amountOfStorage;
        int activeWalls = 0;
        int drawersPlacedOnWall = 0;

        foreach(GameObject wall in user.walls)
        {
            if (wall.GetComponent<WallInfo>().isbeingUsed)
            {
                activeWalls++;
            }
        }
        int drawersPerWall = Mathf.CeilToInt( drawerCount / activeWalls);

        if(topWall.GetComponent<WallInfo>().isbeingUsed)
        {
           
            if(drawerCount > 0 && drawersPlacedOnWall < drawersPerWall)
            {
                objectToPlace = user.cabinets[2];
            }
            else
            {
                objectToPlace = user.cabinets[3];
            }
            
            for (int i = 0; i<TopWallGaps.Count; i++)
            {
                float tempGap = TopWallGaps[i] * 1000;
                float xPush = 0;
                int nextObj = 0;
                for (int t = cabSizes.Count-1; t>-1; t--)
                {
                    
                    float prevXPos = TopWallUnits[i].transform.position.x;
                    Debug.Log(cabSizes[t]);
                    if (tempGap > cabSizes[t])
                    {
                        if(i == 0)
                        {
                            xPush = 0.5f;
                        }
                        else
                        {
                            xPush = 0;
                        }
                        float cabScaleX = ((float)(1 / (0.6 / (cabSizes[t]/1000))));
                        //Debug.Log(cabScaleX);
                        Vector3 cabScale = new Vector3(cabScaleX, 1f, 1f);
                        Quaternion rotation = Quaternion.Euler(0, 180, 0);
                        Debug.Log(xPush);
                        Vector3 tempPos = new Vector3(prevXPos + (cabSizes[t] / 1000) + xPush*1.2f, TopWallUnits[i].transform.position.y, TopWallUnits[i].transform.position.z - 0.3f);
                        TopWallUnits.Insert(i+1, Instantiate(objectToPlace, tempPos, rotation));
                        Debug.Log(TopWallUnits[i + 1].transform.position.x);
                        TopWallUnits[i+1].transform.localScale = cabScale;
                        Debug.Log(TopWallUnits[i + 1].transform.position.x);
                        tempGap -= cabSizes[t];
                        prevXPos = tempPos.x;
                    }
                    
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWall", menuName = "wall")]
public class Wall : ScriptableObject
{
    public GameObject instanObject;
    public enum WallDirection
    {
        Top,
        Bottom,
        Left,
        Right,
    }
    public WallDirection direction;
    public int length;
    public int width;
    public int height;

    public GameObject placeObj(Vector3 spawnLoc, Quaternion rotation)
    {
        return Instantiate(instanObject, spawnLoc, rotation);
    }

}

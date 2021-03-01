using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class BrickInfo
{
    public TypeBrick typeBrick = TypeBrick.None;
    public Vector3 position = Vector3.zero;
}

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    public float width = 4f;
    public float height = 4f;
    public BrickInfo[] brickInfo;
}

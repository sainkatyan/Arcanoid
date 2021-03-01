using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BrickPresetScriptableObject", order = 1)]
public class BrickScriptableObject : ScriptableObject
{
    public Brick prefab_Brick;
    public Vector3 brickScale = new Vector3(1f, 0.3f, 1f);
    public int brickHealth = 1;
    public Color brickColor = Color.white;
    public TypeBrick typeBrick = TypeBrick.None;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBonus { None, PlusBallSpeed, MinusBallSpeed, AddBall, PlusPlayerScale}

public class Brick : MonoBehaviour, IElement
{
    private int health = 1;
    private Vector3 scale;
    private Vector3 position;
    private TypeBonus typeBonus = TypeBonus.None;

    public Vector3 Scale { get => scale;}
    public Vector3 Position { get => position;}
    public TypeBonus TypeBonus { get => typeBonus;}
    public int Health { get => health;}

    public void Init(Vector3 _scale, Vector3 _pos, int _health, TypeBonus _typeBonus = TypeBonus.None)
    {
        scale = _scale;
        transform.localScale = scale;
        position = _pos;
        transform.position = position;
        health = _health;
        typeBonus = _typeBonus;
    }
    public void Collision()
    {
        health = health - 1;
        Debug.Log("health:" + health);
        if (health < 1)
        {
            if (TypeBonus != TypeBonus.None)
            {
                GameController.InitBonus(this);
            }
            GameController.RemoveElement(this);
        }
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}

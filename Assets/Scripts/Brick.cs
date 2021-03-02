using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBrick { None, PlusBallSpeed, MinusBallSpeed, AddBall, PlusPlayerScale}

public class Brick : MonoBehaviour, IElement
{
    private int health = 1;
    private Vector3 scale;
    private Vector3 position;
    private Color color;
    private TypeBrick typeBonus = TypeBrick.None;

    public Vector3 Scale { get => scale;}
    public Vector3 Position { get => position;}
    public TypeBrick TypeBonus { get => typeBonus;}
    public int Health { get => health;}
    public Color Color { get => color;}

    public void Init(Vector3 _scale, Vector3 _pos, int _health,  Color _color, TypeBrick _typeBonus = TypeBrick.None)
    {
        scale = _scale;
        position = _pos;
        health = _health;
        typeBonus = _typeBonus;
        color = _color;

        transform.position = position;
        transform.localScale = scale;

        GetComponent<SpriteRenderer>().color = color;
    }
    public void Collision()
    {
        health = health - 1;
        if (health < 1)
        {
            if (TypeBonus != TypeBrick.None)
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

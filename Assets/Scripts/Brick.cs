using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IElement
{
    private int health = 1;
    public void Init(Vector3 scale, Vector3 pos, int brickHealth)
    {
        transform.localScale = scale;
        transform.position = pos;
        health = brickHealth;
    }
    public void Collision()
    {
        GameController.RemoveElement(this);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}

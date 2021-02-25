using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IElement
{
    public void Collision()
    {
        Destroy(this.gameObject);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void Init(Vector3 scale, Vector3 pos)
    {
        transform.localScale = scale;
        transform.position = pos;
    }
}

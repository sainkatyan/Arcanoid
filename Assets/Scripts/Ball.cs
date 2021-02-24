using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void Start()
    {

    }
    public void SetRadius(float radiusBall)
    {
        transform.localScale = Vector3.one * radiusBall * 2f;
    }
}

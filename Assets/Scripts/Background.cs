using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public void SetLocalScale(Vector3 backGroundLocalScale)
    {
        transform.localScale = backGroundLocalScale;
    }
}

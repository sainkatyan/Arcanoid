using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElement
{
    Transform GetTransform();
    void Collision();
}

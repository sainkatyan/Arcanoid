using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IElement
{
    private float speed = 1f;
    private Vector3 scale = Vector3.one;

    public void Collision()
    {
        
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void Init(Vector3 playerScale, Vector3 startPos, float playerSpeed)
    {
        speed = playerSpeed;
        scale = playerScale;
        transform.localScale = scale;
        transform.position = startPos;
    }


    public void UpdatePosition(Vector2 wall, float deltaTime, float inputHorizontal)
    {
        var deltaPosition = inputHorizontal * deltaTime * speed;
        transform.position = transform.position + new Vector3(deltaPosition, 0f, 0f);

        if (transform.position.x + scale.x * 0.5f >= wall.x)
        {
            transform.position = new Vector3(wall.x - scale.x * 0.5f, transform.position.y, transform.position.z);
        }

        else if (transform.position.x - scale.x * 0.5f <= -wall.x)
        {
            transform.position = new Vector3(-wall.x + scale.x * 0.5f, transform.position.y, transform.position.z);
        }
    }
}

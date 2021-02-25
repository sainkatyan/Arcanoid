using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    private float speed = 1f;
    private float radius = 1f;
    public void Init(float radiusBall, float ballSpeed, Vector2 ballDirection)
    {
        radius = radiusBall;
        transform.localScale = Vector3.one * radius * 2f;
        speed = ballSpeed;
        direction = ballDirection;
    }



    public void UpdatePosition(Vector2 wall, float deltaTime, List<IElement> elements)
    {
        var deltaPosition = direction * deltaTime * speed;
        transform.position = transform.position + new Vector3(deltaPosition.x, deltaPosition.y, 0f);

        if (transform.position.x >= wall.x - radius || transform.position.x <= -wall.x + radius)
        {
            direction.x *= -1f;
            if (transform.position.x >= 0f)
            {
                transform.position = new Vector3(wall.x - radius, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(-wall.x + radius, transform.position.y, transform.position.z);
            }

        }
        if (transform.position.y >= wall.y - radius)
        {
            direction.y *= -1f;
            transform.position = new Vector3(transform.position.x, wall.y - radius, transform.position.z);
        }
        if (transform.position.y <= -wall.y + radius)
        {
            Debug.Log("GameOver");
            Destroy(this.gameObject);
        }

        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i] != null)
            {
                var position = elements[i].GetTransform().position;
                var scale = elements[i].GetTransform().localScale;
                if (CheckDown(position, scale) && CheckUp(position, scale))
                {
                    if (CheckRight(position, scale))
                    {
                        if (CheckLeft(position, scale))
                        {
                            direction.y *= -1f;

                            if (transform.position.y >= position.y)
                            {
                                transform.position = new Vector3(transform.position.x, position.y + radius + scale.y * 0.5f,  transform.position.z);
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x, position.y - radius - scale.y * 0.5f, transform.position.z);
                            }
                            elements[i].Collision();
                            break;
                        }
                    }
                }
            }
        }
    }
    private bool CheckUp(Vector3 pos, Vector3 scale)
    {
        return transform.position.y - radius <= pos.y + scale.y * 0.5f;
    }
    private bool CheckDown(Vector3 pos, Vector3 scale)
    {
        return transform.position.y + radius >= pos.y - scale.y * 0.5f;
    }
    private bool CheckRight(Vector3 pos, Vector3 scale)
    {
        return transform.position.x - radius <= pos.x + scale.x * 0.5f;
    }
    private bool CheckLeft(Vector3 pos, Vector3 scale)
    {
        return transform.position.x + radius >= pos.x - scale.x * 0.5f;
    }
}

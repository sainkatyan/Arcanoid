using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IElement
{
    private float speed = 1f;
    private Vector3 scale = Vector3.one;

    public Vector3 Scale { get => scale;}

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

    public void UpdatePosition(Vector2 wall, float deltaTime, float inputHorizontal, List<Bonus> bonuses)
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

        for (int i = 0; i < bonuses.Count; i++)
        {
            if (bonuses != null)
            {
                var position = bonuses[i].transform.position;
                var scale = bonuses[i].transform.localScale;

                if (CheckUp(position, scale) && CheckRight(position, scale) && CheckLeft(position, scale))
                {
                    bonuses[i].Collision();
                }
            }
        }
    }

    private bool CheckUp(Vector3 pos, Vector3 scale)
    {
        return transform.position.y + transform.localScale.y * 0.5f >= pos.y - scale.y * 0.5f;
    }
    private bool CheckRight(Vector3 pos, Vector3 scale)
    {
        return transform.position.x - transform.localScale.x * 0.5f <= pos.x + scale.x * 0.5f;
    }
    private bool CheckLeft(Vector3 pos, Vector3 scale)
    {
        return transform.position.x + transform.localScale.x * 0.5f >= pos.x - scale.x * 0.5f;
    }

    public void ChangeScale(float _deltaScaleX)
    {
        scale += new Vector3(_deltaScaleX, 0f, 0f);
        transform.localScale = scale;
    }
}

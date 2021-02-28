using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Vector2 direction = new Vector2(0, -1f);
    private float speed = 0.5f;
    private Vector3 scale = Vector3.one;
    private Vector3 position = Vector3.one;

    private TypeBonus typeBonus = TypeBonus.None;

    public void Collision()
    {
        Debug.Log("collision bonus: " + typeBonus);
        GameController.ActivateBonus(typeBonus);
        GameController.RemoveBonus(this);
    }

    public void Init(Vector3 _scale, Vector3 _pos, float _speed, TypeBonus _typeBonus)
    {
        speed = _speed;
        scale = _scale;
        position = _pos;

        transform.localScale = scale;
        transform.position = position;
        typeBonus = _typeBonus;
    }
    public void UpdatePosition(Vector2 wall, float deltaTime)
    {
        var deltaPosition = direction * deltaTime * speed;
        transform.position = transform.position + new Vector3(0f, deltaPosition.y, 0f);

        if (transform.position.y <= -wall.y)
        {
            GameController.RemoveBonus(this);
        }
    }
}

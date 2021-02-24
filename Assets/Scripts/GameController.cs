using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Ball")]
    public Ball prefab_Ball;
    private Ball ball;
    public float ballSpeed = 1f;
    public float ballRadius = 0.3f;
    public Vector2 ballDirection = Vector2.zero;

    [Header("Background")]
    public Background prefab_Background;
    private Background background;
    public float width = 4f;
    public float height = 4f;
    private float wallX = 0f;
    private float wallY = 0f;

    void Start()
    {
        background = Instantiate(prefab_Background, new Vector3(0f, 0f, 100f), Quaternion.identity);
        background.SetLocalScale(new Vector3(width, height, 1f));
        wallX = width / 2f - ballRadius;
        wallY = height / 2f - ballRadius;

        ball = Instantiate(prefab_Ball, Vector3.zero, Quaternion.identity);
        ball.SetRadius(ballRadius);
        ballDirection = Random.insideUnitCircle;
    }

    void Update()
    {
        if (ball.transform.position.x >= wallX || ball.transform.position.x <= -wallX)
        {
            ballDirection.x *= -1f;
        }
        if (ball.transform.position.y >= wallY || ball.transform.position.y <= -wallY)
        {
            ballDirection.y *= -1f;
        }

        var deltaPosition = ballDirection * (Time.deltaTime * ballSpeed);
        ball.transform.position = ball.transform.position + new Vector3(deltaPosition.x, deltaPosition.y, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Player")]
    public Player prefubPlayer;
    private Player player;
    private Vector3 playerStartPos;
    public Vector3 playerScale = new Vector3(2f, 0.3f, 1f);
    private float playerOffsetY = 0.05f;
    public float playerSpeed = 1f;

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
    private Vector2 wall = Vector2.zero;

    [Header("Bricks")]
    public int countBricks = 3;
    public Brick prefab_Brick;
    private Brick[] bricks;
    public Vector3 brickScale = new Vector3(0.95f, 0.2f, 1f);

    private List<IElement> elements;

    void Start()
    {
        elements = new List<IElement>();

        background = Instantiate(prefab_Background, new Vector3(0f, 0f, 100f), Quaternion.identity);
        background.Init(new Vector3(width, height, 1f));
        wall.x = width / 2f;
        wall.y = height / 2f;

        playerStartPos = new Vector3(0f, -height / 2f + playerScale.y + playerOffsetY);
        player = Instantiate(prefubPlayer, Vector3.zero, Quaternion.identity);
        player.Init(playerScale, playerStartPos, playerSpeed);
        elements.Add(player);

        bricks = new Brick[countBricks];
        for (int i = 0; i < countBricks; i++)
        {
            bricks[i] = Instantiate(prefab_Brick, Vector3.zero, Quaternion.identity);
            bricks[i].Init(brickScale, new Vector3(i, 2f, 1f));
            elements.Add(bricks[i]);
        }

        float ballStartPosY = playerStartPos.y + playerScale.y * 0.5f + ballRadius + 1f;
        ball = Instantiate(prefab_Ball, new Vector3(playerStartPos.x, ballStartPosY, playerStartPos.z), Quaternion.identity);
        float randomAngle = Random.Range(20f, 160f) * Mathf.Deg2Rad;
        ballDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        ball.Init(ballRadius, ballSpeed, ballDirection);

    }


    void Update()
    {
        player.UpdatePosition(wall, Time.deltaTime, Input.GetAxis("Horizontal"));
        ball.UpdatePosition(wall, Time.deltaTime, elements);
    }
}

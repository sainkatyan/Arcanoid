using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LevelScriptableObject level;


    [Header("Player")]
    public Player prefubPlayer;
    private Player player;
    private Vector3 playerStartPos;
    public Vector3 playerScale = new Vector3(2f, 0.3f, 1f);
    private float playerOffsetY = 0.05f;
    public float playerSpeed = 1f;

    [Header("Ball")]
    public Ball prefab_Ball;
    private List<Ball> balls;
    public float ballSpeed = 1f;
    public float ballRadius = 0.3f;
    public Vector2 ballDirection = Vector2.zero;
    public static readonly float MINANGLE = 20f;
    public static readonly float MAXANGLE = 160f;

    [Header("Background")]
    public Background prefab_Background;
    private Background background;
    private Vector2 wall = Vector2.zero;
    //public float width = 4f;
    //public float height = 4f;

    [Header("Bricks")]
    public BrickScriptableObject[] brickPreset;
    private Brick[] bricks;
    //public int countBricks = 3;

    [Header("Bonus")]
    public Bonus prefub_Bonus;
    public float bonusSpeed;
    private List<Bonus> bonuses;
    public float bonusTime = 3f;

    private List<IElement> elements;

    private static GameController instance;

    public static GameController Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        elements = new List<IElement>();
        balls = new List<Ball>();
        bonuses = new List<Bonus>();

        background = Instantiate(prefab_Background, new Vector3(0f, 0f, 100f), Quaternion.identity);
        background.Init(new Vector3(level.width, level.height, 1f));
        wall.x = level.width / 2f;
        wall.y = level.height / 2f;

        playerStartPos = new Vector3(0f, -level.height / 2f + playerScale.y + playerOffsetY);
        player = Instantiate(prefubPlayer, Vector3.zero, Quaternion.identity);
        player.Init(playerScale, playerStartPos, playerSpeed);
        elements.Add(player);

        bricks = new Brick[level.brickInfo.Length];
        for (int i = 0; i < level.brickInfo.Length; i++)
        {
            var dad = GetBrick(level.brickInfo[i].typeBrick);
            bricks[i] = Instantiate(dad.prefab_Brick);

            bricks[i].Init(dad.brickScale, level.brickInfo[i].position, dad.brickHealth, dad.typeBrick);
            elements.Add(bricks[i]);
        }

        InitBall();

        //init bonuses
        //for (int i = 0; i < countBricks; i++)
        //{
        //    InitBonus(brickScale, bricks[i].GetTransform().position, bonusSpeed);
        //}
    }

    private void Update()
    {
        player.UpdatePosition(wall, Time.deltaTime, Input.GetAxis("Horizontal"), bonuses);
        if (balls.Count > 0)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].UpdatePosition(wall, Time.deltaTime, elements);
            }
        }
        else
        {
            Debug.Log("GameOver");
        }
        if (bonuses.Count > 0)
        {
            for (int i = 0; i < bonuses.Count; i++)
            {
                bonuses[i].UpdatePosition(wall, Time.deltaTime);
            }
        }
    }

    private BrickScriptableObject GetBrick(TypeBrick typeWant)
    {
        for (int i = 0; i < brickPreset.Length; i++)
        {
            var typeDad = brickPreset[i].typeBrick;
            if (brickPreset[i].typeBrick == typeWant)
            {
                return brickPreset[i];
            }
        }
        return null;

    }
    private void InitBall()
    {
        float ballStartPosY = playerStartPos.y + playerScale.y * 0.5f + ballRadius + 0.5f;

        var tempBall = Instantiate(prefab_Ball, new Vector3(playerStartPos.x, ballStartPosY, playerStartPos.z), Quaternion.identity);

        float randomAngle = Random.Range(MINANGLE, MAXANGLE) * Mathf.Deg2Rad;
        ballDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        tempBall.Init(ballRadius, ballSpeed, ballDirection);
        balls.Add(tempBall);
    }

    public static void RemoveElement(IElement element)
    {
        bool result =  instance.elements.Remove(element);
        if (result == true)
        {
            Debug.Log("Element is removed from element list");
            Destroy(element.GetTransform().gameObject);
        }
        else
        {
            Debug.LogError("Element doesn't exist");
        }
    }
    public static void RemoveBall(Ball ball)
    {
        bool result = instance.balls.Remove(ball);
        if (result == true)
        {
            Debug.Log("Ball is removed from element list");
            Destroy(ball.gameObject);
        }
        else
        {
            Debug.LogError("Ball doesn't exist");
        }
    }

    public static void InitBonus(Brick _brick)
    {
        var bonus = Instantiate(instance.prefub_Bonus, Vector3.zero, Quaternion.identity);
        bonus.Init(_brick.Scale, _brick.Position, instance.bonusSpeed, _brick.TypeBonus);
        instance.bonuses.Add(bonus);
    }

    public static void RemoveBonus(Bonus bonus)
    {
        bool result = instance.bonuses.Remove(bonus);
        if (result == true)
        {
            Debug.Log("Bonus is removed from element list");
            Destroy(bonus.gameObject);
        }
        else
        {
            Debug.LogError("Bonus doesn't exist");
        }
    }
    public static void ActivateBonus(TypeBrick _typeBonus)
    {
        Debug.Log(_typeBonus);

        switch (_typeBonus)
        {
            case TypeBrick.None:
                break;
            case TypeBrick.PlusBallSpeed:
                instance.StartCoroutine(instance.ChangeBallSpeed(instance.ballSpeed * 0.5f));
                break;
            case TypeBrick.MinusBallSpeed:
                instance.StartCoroutine(instance.ChangeBallSpeed(instance.ballSpeed * 0.5f));
                break;
            case TypeBrick.AddBall:
                instance.AddBall();
                break;
            case TypeBrick.PlusPlayerScale:
                instance.StartCoroutine(instance.ChangePlayerScale(instance.player.Scale.x * 0.3f));
                break;
            default:
                break;
        }
    }

    private IEnumerator ChangeBallSpeed(float _deltaSpeed)
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].ChangeSpeed(_deltaSpeed);
        }
        Debug.Log("change speed on " + _deltaSpeed);
        yield return new WaitForSeconds(bonusTime);

        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].ChangeSpeed(-_deltaSpeed);
        }
    }
    private void AddBall()
    {
        List<Ball> oldBalls = new List<Ball>();
        oldBalls.AddRange(balls);
        balls.Clear();
        for (int i = 0; i < oldBalls.Count; i++)
        {
            float deltaAngle = 45f;
            for (int j = 0; j < 2; j ++)
            {
                var tempBall = Instantiate(prefab_Ball, oldBalls[i].transform.position, Quaternion.identity);
                float angle = Mathf.Atan2(oldBalls[i].Direction.y, oldBalls[i].Direction.x) + (deltaAngle * Mathf.Deg2Rad);
                var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                tempBall.Init(oldBalls[i].Radius, oldBalls[i].Speed, direction);
                balls.Add(tempBall);

                deltaAngle = 90f;
            }
        }
        for (int i = 0; i < oldBalls.Count; i++)
        {
            Destroy(oldBalls[i].gameObject);
        }
    }
    private IEnumerator ChangePlayerScale(float _deltaScale)
    {
        player.ChangeScale(_deltaScale);
        yield return new WaitForSeconds(bonusTime);
        player.ChangeScale(-_deltaScale);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    public static GameCoordinator Instance;

    [SerializeField]
    private bool player1Turn = true;
    public bool Player1Turn { get { return player1Turn; } }
    
    [SerializeField]
    private Table table;

    [SerializeField]
    private Cue cue;

    [SerializeField]
    private Ball cueBall;

    private BallType p1 = BallType.NONE;
    private BallType p2 = BallType.NONE;

    public BallType P1 { get { return p1; } }
    public BallType P2 { get { return p2; } }

    private bool isAssign;

    public bool IsTurnReady { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        AssignBallType();
        IsTurnReady = CheckTurn();
        if (cue.CueBallHit)
        {
            player1Turn = !player1Turn;
            cue.CueBallHit = false;
            // turn off trigger for all balls when cue ball is hit
            foreach (Ball ball in table.GetBallObjects())
            {
                ball.GetComponent<Collider2D>().isTrigger = false;
            }

        }

        if (IsTurnReady)
        {
            // turn on trigger for normal ball except cue ball
            Ball[] balls = table.GetBallObjects();
            for (int i = 0; i < balls.Length - 1; i++)
            {
                balls[i].GetComponent<Collider2D>().isTrigger = true;
            }
        } else
        {
            cue.GetCollider().isTrigger = true;
        }
    }

    private bool CheckTurn()
    {
        Ball[] balls = table.GetBallObjects();
        foreach(Ball ball in balls)
        {
            if(ball.GetComponent<Rigidbody2D>().velocity.magnitude > 0.0f)
            {
                return false;
            }
        }
        return true;
    }

    void AssignBallType()
    {
        if (isAssign) return;
        bool[] balls = table.GetBalls();
        for (int i = 1; i <= balls.Length; i++)
        {
            if(balls[i - 1] && i != (int)BallType.EIGHT && i != (int)BallType.CUE)
            {
                if(Ball.GetBallType(i - 1) == BallType.SOLID)
                {
                    if(player1Turn)
                    {
                        p1 = BallType.SOLID;
                        p2 = BallType.STRIPE;
                    } else
                    {
                        p1 = BallType.STRIPE;
                        p2 = BallType.SOLID;
                    }
                } else
                {
                    if (player1Turn)
                    {
                        p1 = BallType.STRIPE;
                        p2 = BallType.SOLID;
                    }
                    else
                    {
                        p1 = BallType.SOLID;
                        p2 = BallType.STRIPE;
                    }
                }
                isAssign = true;
            }
        }
    }

    public Ball GetCueBall()
    {
        return cueBall;
    }

    public Table GetTable()
    {
        return table;
    }
}

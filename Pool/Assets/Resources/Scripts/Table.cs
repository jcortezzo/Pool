using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private bool[] balls = new bool[16];
    [SerializeField]
    private Ball[] ballObjects = new Ball[16];

    public void SetBallIn(int i)
    {
        balls[i] = true;
    }

    public bool[] GetBalls()
    {
        return balls;
    }

    public Ball[] GetBallObjects()
    {
        return ballObjects;
    }
}

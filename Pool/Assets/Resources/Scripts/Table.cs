using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private bool[] balls = new bool[16];
    [SerializeField]
    private Ball[] ballss = new Ball[16];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBallIn(int i)
    {
        balls[i] = true;
    }

    public bool[] GetBalls()
    {
        return balls;
    }

    public Ball[] GetBallss()
    {
        return ballss;
    }
}

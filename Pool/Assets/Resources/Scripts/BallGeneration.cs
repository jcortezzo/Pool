using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;

    private float ballSize;

    // Start is called before the first frame update
    void Start()
    {
        ballSize = ballPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        Generate8Balls();
        Destroy(this.gameObject);
    }

    private void Generate8Balls()
    {
        int[] generationOrder = new int[] { 1, 9, 2, 10, 8, 3, 11, 7, 14, 4, 5, 13, 15, 6, 12 };
        int order = 0;
        int col = 5;
        Ball[] balls = GameCoordinator.Instance.GetTable().GetBallObjects();
        for (int i = 0; i < col; i++)
        {
            Vector2 initPos = this.transform.position + new Vector3(-ballSize * i, ballSize * i - (ballSize / 2.0f) * i);
            for (int j = 0; j <= i; j++)
            {
                Vector2 position = initPos - new Vector2(0, ballSize * j);

                GameObject ball = Instantiate(ballPrefab, new Vector3(position.x, position.y, -5), Quaternion.identity);
                Ball b = ball.GetComponent<Ball>();
                int ballNum = generationOrder[order++];
                b.SetNumber(ballNum);
                balls[ballNum - 1] = b;

            }
        }
    }
}

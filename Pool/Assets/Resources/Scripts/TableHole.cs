using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableHole : MonoBehaviour
{
    
    [SerializeField]
    private List<GameObject> balls;
    private Table table;
    // Start is called before the first frame update
    void Start()
    {
        table = transform.parent.GetComponent<Table>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if(ball.Number != 8 && ball.Number != 16)
            {
                balls.Add(ball.gameObject);
                ball.gameObject.SetActive(false);
                table.SetBallIn(ball.Number - 1);
            }
        }
    }
}

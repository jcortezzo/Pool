using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] int number;
    private BallType type;

    private const int CUE_NUMBER = 16;

    [Header("Debug Control")]
    [SerializeField] private bool control;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        type = number < 7  ? BallType.SOLID :
               number == 7 ? BallType.EIGHT :
                             BallType.STRIPE;  // number > 7

        anim.Play("" + number);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (control) Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical   = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontal, vertical).normalized * Time.deltaTime * speed;
    }
}

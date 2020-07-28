using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Properties")]
    [SerializeField] private int number;
    [SerializeField] private float stopThreshhold = 0.1f;

    private BallType type;

    private const int CUE_NUMBER = 16;

    [Header("Debug Control")]
    [SerializeField] private bool control;
    [SerializeField] private float speed;


    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        type = number < 7  ? BallType.SOLID :
               number == 7 ? BallType.EIGHT :
               number < CUE_NUMBER ? BallType.STRIPE :
               BallType.CUE;

        if (type == BallType.CUE)
        {
            if (GlobalValues.instance.GetCueBall() != null)
            {
                Destroy(GlobalValues.instance.GetCueBall().gameObject);
            }
            GlobalValues.instance.SetCueBall(this);
        }

        anim.Play("" + number);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (control) Move();

        if (rb.velocity.magnitude <= stopThreshhold)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical   = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontal, vertical).normalized * Time.deltaTime * speed;
    }

    public void SetNumber(int num)
    {
        number = num;
        anim.Play("" + num);
    }

    public int GetNumber()
    {
        return number;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            if (type != BallType.CUE)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

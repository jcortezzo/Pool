using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer sr;
    private Transform tip;
    private LineRenderer lr;
    [SerializeField] private LayerMask ballMask;
    [SerializeField] private LayerMask bounceMask;

    private bool isActive;
    private bool showLine;
    private Vector2 direction;

    private const int MAX_DEPTH = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        tip = GetComponentInChildren<Transform>();  // ?
        lr = GetComponent<LineRenderer>();

        //lr.positionCount = MAX_DEPTH + 2;
        lr.SetPosition(0, tip.position);
        lr.SetPosition(1, tip.position);
        lr.startColor = Color.blue;
        lr.endColor = Color.blue;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        
        showLine = false;

        col.isTrigger = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        isActive = Input.GetMouseButton(0);
        col.isTrigger = !isActive;
        sr.color = isActive ? Color.white : Color.gray;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            showLine = !showLine;
            lr.enabled = false;
        }

        if (showLine)
        {
            lr.positionCount = 2;
            lr.SetPosition(0, tip.position);
            RaycastHit2D hit = Physics2D.Raycast(tip.position, -transform.right,
                                                 100000f, ballMask);
            Debug.DrawRay(tip.position, -transform.right * 10000f, Color.white, 0.1f, false);
            if (hit.collider != null)
            {
                lr.enabled = true;
                lr.SetPosition(1, hit.point);
                DrawPath(hit, -transform.right, 1);
            }
            else
            {
                lr.enabled = false;
            }
        }
    }

    void DrawPath(RaycastHit2D originHit, Vector2 incoming, int depth)
    {
        if (depth >= MAX_DEPTH || originHit.collider == null) return;

        //RaycastHit2D hit = Physics2D.Raycast(originHit.point/* - originHit.normal * 6*/, -originHit.normal,
        //                                     100000f, ballMask);

        Vector2 reflection = Vector2.Reflect(incoming, originHit.normal);
        reflection *= originHit.collider.CompareTag("Ball") ? -1 : 1;

        RaycastHit2D hit = Physics2D.Raycast(originHit.point, reflection,
                                             100000f, bounceMask);
        if (hit.collider == null)
        {
            //while (depth != MAX_DEPTH)
            //{
            //    lr.SetPosition(++depth, originHit.point);
            //}
            return;
        }
        lr.positionCount++;
        lr.SetPosition(depth + 1, hit.point);

        DrawPath(originHit, reflection, depth + 1);
    }
    
    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isActive && Input.GetMouseButton(1))
        {
            //mousePos = new Vector3(mousePos.x, transform.position.y, transform.position.z); wrong?
            //rb.constraints = RigidbodyConstraints2D.FreezePositionY;

            Vector3 localVelocity = transform.InverseTransformDirection(mousePos - transform.position);
            localVelocity.y = 0;
            localVelocity.z = 0;
            rb.velocity = transform.TransformDirection(localVelocity * 50);  // <-- lmao @ 50
        } else
        {
            //rb.constraints = RigidbodyConstraints2D.None;
            rb.MovePosition(mousePos);
            //rb.velocity = mousePos - transform.position;
        }
        
        //rb.MovePosition(mousePos);

        if (!isActive)
        {
            Ball cueBall = GlobalValues.instance.GetCueBall();
            Vector3 cueBallPos = cueBall.transform.position;
            float angle = Mathf.Atan2(mousePos.y - cueBallPos.y, mousePos.x - cueBallPos.x);
            rb.MoveRotation(Mathf.Rad2Deg * angle);
        }
        rb.angularVelocity = 0f;

        direction = transform.right;//mousePos - tip.position;//transform.eulerAngles;
    }
}

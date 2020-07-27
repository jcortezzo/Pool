using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer sr;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
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
    }
}

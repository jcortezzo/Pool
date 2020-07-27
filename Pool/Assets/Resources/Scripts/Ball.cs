using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;

    [SerializeField] int number;
    private BallType type;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        type = number < 7  ? BallType.SOLID :
               number == 7 ? BallType.EIGHT :
                             BallType.STRIPE;  // number > 7

        anim.SetInteger("number", number);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

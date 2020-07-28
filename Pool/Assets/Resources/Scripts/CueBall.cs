using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : Ball
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.GetComponent<Collider2D>().isTrigger = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuideCursor : SwipeManager
{
    private Tweener tweener;


    override protected void Up(float directionX, float directionY)
    {
        tweener = transform.DORotate(transform.localEulerAngles + new Vector3(0, 0, -directionY/10), 0);
    }

    override protected void Down(float directionX, float directionY)
    {
        tweener = transform.DORotate(transform.localEulerAngles + new Vector3(0, 0, -directionY/10), 0);

    }


    override protected void TouchUp(float directionx, float directionY)
    {
        tweener.Kill();
        tweener = transform.DORotate(transform.localEulerAngles,0) ;

        print("sd");
    }
}

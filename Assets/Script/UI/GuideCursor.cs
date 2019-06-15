using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuideCursor : SwipeManager
{
    private Tweener tweener;

    override protected void Update_()
    {
        if(Save.maingameFlag == Save.MainGameFlag.THROWMOVE)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }


    override protected void Up(float directionX, float directionY)
    {
        if(Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
            tweener = transform.DORotate(transform.localEulerAngles + new Vector3(0, 0, -directionY/10), 0);
    }

    override protected void Down(float directionX, float directionY)
    {
        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
            tweener = transform.DORotate(transform.localEulerAngles + new Vector3(0, 0, -directionY/10), 0);

    }

    override protected void TouchUp(float directionx, float directionY)
    {
        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
        {
            tweener.Kill();
            tweener = transform.DORotate(transform.localEulerAngles, 0);
        }
    }
}

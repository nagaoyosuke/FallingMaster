﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuideCursor : SwipeManager
{

    [SerializeField]
    private SpriteRenderer angle;
    [SerializeField]
    private SpriteRenderer arrow;
    [SerializeField]
    private GameObject[] objs;


    private Tweener tweener;

    [SerializeField]
    private float MaxAngle;
    [SerializeField]
    private float MinAngle;

    void Start()
    {
        angle.enabled = false;
        arrow.enabled = false;
        foreach (GameObject o in objs)
        {
            if (o == null)
                continue;

            o.SetActive(false);
        }
    }

    override protected void Update_()
    {
        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
        {
            angle.enabled = true;
            arrow.enabled = true;
            foreach (GameObject o in objs)
            {
                if (o == null)
                    continue;

                o.SetActive(true);
            }
        }

        if (Save.maingameFlag == Save.MainGameFlag.THROW)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }


    override protected void Up(float directionX, float directionY)
    {
        directionY = AngleOverCheckY(directionY);

        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
            tweener = transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0, 0, directionY / 10), 0);
        
    }

    override protected void Down(float directionX, float directionY)
    {
        directionY = AngleOverCheckY(directionY);

        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
            tweener = transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0, 0, directionY / 10), 0);
        

    }

    override protected void Left(float directionX, float directionY)
    {
        directionX = AngleOverCheckX(directionX);

        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
                tweener = transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0, 0, -directionX / 10), 0);
        
    }

    override protected void Right(float directionX, float directionY)
    {
        directionX = AngleOverCheckX(directionX);

        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
                tweener = transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0, 0, -directionX / 10), 0);
        
    }

    override protected void TouchUp(float directionX, float directionY)
    {
        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
        {
            tweener.Kill();
            if (transform.localEulerAngles.z > MaxAngle)
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, MaxAngle);
            if (transform.localEulerAngles.z <= MinAngle)
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, MinAngle);

            tweener = transform.DOLocalRotate(transform.localEulerAngles, 0);
        }
    }

    float AngleOverCheckX(float directionX)
    {
        if (transform.localEulerAngles.z - directionX / 10 > MaxAngle)
        {
            return 0;
        }
        if (transform.localEulerAngles.z - directionX / 10 < MinAngle)
        {
            return 0;
        }
        return directionX;
    }

    float AngleOverCheckY(float directionY)
    {
        if (transform.localEulerAngles.z + directionY / 10 > MaxAngle)
        {
            return 0;
        }
        if (transform.localEulerAngles.z + directionY / 10 < MinAngle)
        {
            return 0;
        }

        return directionY;
    }
}

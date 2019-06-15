using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public Vector3 touchStartPos;
    public Vector3 touchEndPos;
    bool oneplay;

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        TOUCH,
        NONE
    }

    public Direction direction = Direction.NONE;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Flick();
        Update_();
    }

    void Flick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                Input.mousePosition.z);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x,
            Input.mousePosition.y,
            Input.mousePosition.z);
            GetDirection();
            touchStartPos = new Vector3(Input.mousePosition.x,
            Input.mousePosition.y,
            Input.mousePosition.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            TouchUp(touchStartPos.x, touchStartPos.y);
        }
    }

    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
        //xがｙより絶対値が大きい時。
        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (0 < directionX)
            {
                //右向きにフリック
                direction = Direction.RIGHT;

            }
            if (0 > directionX)
            {
                //左向きにフリック
                direction = Direction.LEFT;
            }
            //yがxより絶対値が大きい時。
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (0 < directionY)
            {
                //上向きにフリック
                direction = Direction.UP;
            }
            if (0 > directionY)
            {
                //下向きのフリック
                direction = Direction.DOWN;
            }
        }
        else
        {
            //タッチを検出
            direction = Direction.TOUCH;
        }
        switch (direction)
        {
            case Direction.UP:
                //上フリックされた時の処理
                Up(directionX,directionY);
                break;

            case Direction.DOWN:
                //下フリックされた時の処理
                Down(directionX, directionY);
                break;

            case Direction.RIGHT:
                //右フリックされた時の処理
                Right(directionX, directionY);
                break;

            case Direction.LEFT:
                //左フリックされた時の処理
                Left(directionX, directionY);
                break;

            case Direction.TOUCH:
                //タッチされた時の処理
                break;
        }

    }

    virtual protected void Update_()
    {

    }

    virtual protected void Up(float directionx, float directionY)
    {

    }

    virtual protected void Down(float directionx, float directionY)
    {
    }

    virtual protected void Left (float directionx, float directionY)
    {
    }

    virtual protected void Right(float directionx, float directionY)
    {
    }

    virtual protected void Touch(float directionx, float directionY)
    {

    }

    virtual protected void TouchUp(float directionx, float directionY)
    {

    }

    float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }
}
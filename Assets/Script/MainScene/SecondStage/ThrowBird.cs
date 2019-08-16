using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowBird : MonoBehaviour
{
    [SerializeField]
    private Vector3 ThrowStartPosition;
    [SerializeField]
    private Vector3 ThrowGoolPosition;
    [SerializeField]
    private float time;
    [SerializeField]
    private float StartCameraTime;
    private bool isMove;

    // Start is called before the first frame update
    void OnEnable()
    {
        isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMove)
        {
            float time_ = time;
            if (Save.maingameFlag == Save.MainGameFlag.STARTCAMERA)
            {
                time_ = StartCameraTime;
            }
            //print(time_);
            transform.localPosition = ThrowStartPosition;
            transform.DOLocalMove(ThrowGoolPosition, time_);
            isMove = true;
        }
    }
}

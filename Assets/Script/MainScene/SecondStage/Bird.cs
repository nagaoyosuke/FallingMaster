using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private Vector3 StartMovePosition;

    [SerializeField]
    private Animator ani;

    [SerializeField]
    private float time;

    [SerializeField]
    private bool isSelf;

    private Sequence sequence;

    private Vector3 pos = Vector3.zero;


    // Start is called before the first frame update
    void OnEnable()
    {
        if(pos.x == 0 && pos.y == 0)
            pos = transform.localPosition;

        transform.localPosition = pos;

        if (time <= 0)
        {
            time = 2.0f;
        }
        if(ani != null)
        {
            ani.CrossFade("BirdSnake", 0, 0,Random.Range(0, 10.0f));
        }


        if (isSelf)
        {
            if(sequence != null)
            {
                sequence.Kill();
            }

            sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalMove(StartMovePosition, time));
        }
    }

    public void StartCameraMove1(Sequence seq)
    {
        seq.Join(transform.DOLocalMove(StartMovePosition, time));
    }


}

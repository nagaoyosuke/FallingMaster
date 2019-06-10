using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WalkManager : MonoBehaviour
{

    [SerializeField]
    private Animator ani;

    /// <summary>
    /// 最初に曲がるポイントまで歩くための座標
    /// </summary>
    [SerializeField]
    private Vector3 CornerMovePoint1;

    /// <summary>
    /// 最初に曲がる向き
    /// </summary>
    [SerializeField]
    private Vector3 CornerMoveRotate1;

    /// <summary>
    /// 二番目に曲がるポイントまで歩くための座標
    /// </summary>
    [SerializeField]
    private Vector3 CornerMovePoint2;

    /// <summary>
    /// 二番目に曲がる向き
    /// </summary>
    [SerializeField]
    private Vector3 CornerMoveRotate2;

    /// <summary>
    /// 投げられるポイントまで歩くための座標
    /// </summary>
    [SerializeField]
    private Vector3 ThrowMovePoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartWalkAnimetion1(Sequence seq)
    {

        seq.Append(
            transform.DOMove(CornerMovePoint1, 1).SetEase(Ease.Linear)
        );

 
        ani.SetBool("Walk", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("Walk", false)));

    }

    public void StartWalkAnimetion2(Sequence seq)
    {

        seq.Join(
            transform.DORotate(CornerMoveRotate1, 0.2f).SetEase(Ease.Linear)
        );

        seq.Join(
            transform.DOMove(CornerMovePoint2, 2).SetEase(Ease.Linear)
        );
            

    }

    public void StartWalkAnimetion3(Sequence seq)
    {

        seq.Join(
            transform.DORotate(CornerMoveRotate2, 0.2f).SetEase(Ease.Linear)
        );
        //seq.SetEase(Ease.InSine);
        seq.Join(
            transform.DOMove(ThrowMovePoint, 2).SetEase(Ease.Linear)
        ).AppendCallback(() =>
        {
            ani.SetBool("Idle", true);
            StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("Idle", false)));
            Save.maingameFlag = Save.MainGameFlag.STARTWAIT;

        });

        ani.SetBool("Walk", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("Walk", false)));

    }
}

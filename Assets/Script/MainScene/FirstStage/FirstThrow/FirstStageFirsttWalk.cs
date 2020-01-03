using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstStageFirsttWalk : WalkManager
{

    // Start is called before the first frame update
    void Start()
    {
        Sound.PlaySe("dora01");

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
        );

        ani.SetBool("Walk", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("Walk", false)));

    }

    public void StartBowAnimetion(Sequence seq)
    {
        seq.Append(
            transform.DOMove(ThrowMovePoint, 0).SetEase(Ease.Linear)
        ).AppendCallback(() =>
        {
            ani.SetBool("BowBT", true);
            StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("BowBT", false)));
            StartCoroutine(BowEnd());
        });
    }

    private IEnumerator BowEnd()
    {
        if (Save.stageState == Save.StageState.SIMPLESTAGE1)
        {
            float SImplevar = SimpleAngle.z * Mathf.Deg2Rad;
        }
        else{
            yield return new WaitUntil(() => aniFlag.BowEndPoint);
            ani.SetBool("Idle", true);
            StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("Idle", false)));
            Save.maingameFlag = Save.MainGameFlag.STARTWAIT;
        }
    }
}

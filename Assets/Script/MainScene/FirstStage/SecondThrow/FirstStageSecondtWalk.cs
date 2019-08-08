using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstStageSecondtWalk : WalkManager
{
    // Start is called before the first frame update
    void Start()
    {
        Sound.PlaySe("dora01");
    }

    public void StartWalkAnimetion1(Sequence seq)
    {
        //seq.SetEase(Ease.InSine);
        seq.Append(
            transform.DOMove(ThrowMovePoint, 1).SetEase(Ease.Linear)
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
        yield return new WaitUntil(() => aniFlag.BowEndPoint);
        ani.SetBool("Idle", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("Idle", false)));
    }
}

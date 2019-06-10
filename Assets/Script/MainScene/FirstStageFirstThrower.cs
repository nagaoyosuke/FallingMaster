using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstStageFirstThrower : WalkManager
{
    public void StartBowAnimetion(Sequence seq)
    {
        seq.AppendCallback(() =>
        {
            ani.SetBool("BowT", true);
            StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("BowT", false)));
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

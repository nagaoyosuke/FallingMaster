using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstStageSecondCamera : CameraManager {
	// Use this for initialization
	void Awake () {
        trans = transform;
        //StartCameraMove();
    }

    void FixedUpdate(){
        if (Save.maingameFlag == Save.MainGameFlag.FALLING)
            ThrowCameraMove();
        //if (Save.ukemiRank == Save.UkemiRank.NOUKEMI)
            //NoUkemiCamereMove();
    }

    public void StartCameraMove1(Sequence seq)
    {
        seq.Join(
            trans.DOMove(ThrowMovePoint, 1).SetEase(Ease.Linear)
        );
    }


    void ThrowCameraMove(){
        //投げられてる時のカメラワークをここに実装

        trans.position = Player.position + new Vector3(2f, 2f, 2f);

        if (!isThrow)
        {
            trans.Rotate(new Vector3(60, -20, 0));
            isThrow = true;
        }

        //trans.position = new Vector3(Player.position.x + 0.5f, Player.position.y + 4.09f, Player.position.z + 2.73f);
    }

    override public IEnumerator PerfectEffect()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(
            trans.DOMove(Player.position + new Vector3(0, 5, 0), 1).SetEase(Ease.Linear)
        );
        seq.Join(
            trans.DORotate(new Vector3(90, 0, 0), 1).SetEase(Ease.Linear)
        );

        seq.Play();
        yield return new WaitUntil(() => PlayerAniFlag.UkemiStandPoint);
        seq.Kill();

        seq = DOTween.Sequence();
        seq.Append(
            trans.DOMove(Player.position + new Vector3(5, 1, -1), 2).SetEase(Ease.Linear)
        );
        seq.Join(
            trans.DORotate(new Vector3(0, 270, 0), 2).SetEase(Ease.Linear)
        );

        seq.Play();
        yield return new WaitUntil(() => PlayerAniFlag.GutEndPoint);
        seq.Kill();

        Player.GetComponent<Rigidbody>().useGravity = false;
        Save.ThrowReSet();

    }

    override public IEnumerator GoodEffect()
    {
        StartCoroutine(PerfectEffect());
        yield return null;
    }

    override public IEnumerator BadEffect()
    {
        StartCoroutine(PerfectEffect());
        yield return null;

    }

    override public IEnumerator FailureNoUkemiEffect()
    {
        StartCoroutine(PerfectEffect());
        yield return null;

    }

    void NoUkemiCamereMove()
    {
        trans.position = Player.position + new Vector3(2f, 2f, 0f);
    }

}

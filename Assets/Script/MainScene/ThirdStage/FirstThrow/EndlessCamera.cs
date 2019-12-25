using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndlessCamera : CameraManager, IMainCameraMove
{

    // Use this for initialization
    void Awake()
    {
        trans = transform;
        //StartCameraMove();
    }

    void FixedUpdate()
    {
        if (Save.maingameFlag == Save.MainGameFlag.FALLING)
            ThrowCameraMove();
        if (Save.maingameFlag == Save.MainGameFlag.SLOWSTART)
            SlowCameraMove();
        //if (Save.ukemiRank == Save.UkemiRank.NOUKEMI)
        //NoUkemiCamereMove();
    }

    public void StartCameraMove1(Sequence seq)
    {

        seq.Join(
            trans.DOLocalMove(CornerMovePoint1, 1).SetEase(Ease.Linear)
        );
        seq.Join(
            transform.DOLocalRotate(CornerMoveRotate1, 0.2f).SetEase(Ease.Linear)
        );
    }

    public void StartCameraMove2(Sequence seq)
    {


        seq.Append(
            trans.DOLocalMove(CornerMovePoint2, 2).SetEase(Ease.Linear)
        );

    }

    public void StartCameraMove3(Sequence seq)
    {
        seq.Join(
            transform.DOLocalRotate(CornerMoveRotate2, 1f).SetEase(Ease.Linear)
        );
        seq.Join(
            trans.DOLocalMove(ThrowMovePoint, 2).SetEase(Ease.Linear)
        );



        Sound.PlaySe("suzume");

    }

    /// <summary>
    /// 未使用
    /// </summary>
    /// <param name="seq"></param>
    public void AnglePointCameraMove(Sequence seq)
    {
        seq.AppendCallback(() => StartCoroutine(AngleMove()));
    }

    /// <summary>
    /// 未使用
    /// </summary>
    /// <param name="seq"></param>
    IEnumerator AngleMove()
    {
        yield return new WaitForSeconds(2.0f);
        fader.isFadeOut = true;
        yield return new WaitUntil(() => fader.isFadeOut == false);
        trans.localPosition = AngleMovePoint;
        trans.DOLocalRotate(AngleMoveRotate, 0);
        camera.orthographic = true;
        camera.orthographicSize = size;
        fader.isFadeIn = true;
        Save.maingameFlag = Save.MainGameFlag.STARTWAIT;
        yield return new WaitUntil(() => fader.isFadeIn == false);
    }

    /// <summary>
    /// 未使用
    /// </summary>
    public void ThrowAngleCameraMove()
    {
        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
        {
            Save.maingameFlag = Save.MainGameFlag.THROWMOVE;

            Sound.PlaySe("osu01");
            //            print("as");
            StartCoroutine(throwAnglCameraMove());
            //StartCoroutine(DelayClass.DelayCoroutin(60 * 6 - 20, () => Sound.PlayBgm("Result3")));
        }
    }

    /// <summary>
    /// 未使用
    /// </summary>
    /// <param name="seq"></param>
    IEnumerator throwAnglCameraMove()
    {
        fader.isFadeOut = true;
        yield return new WaitUntil(() => fader.isFadeOut == false);
        trans.localPosition = ThrowMovePoint;
        trans.DOLocalRotate(CornerMoveRotate2, 0);
        camera.orthographic = false;
        camera.fieldOfView = field_of_view;
        fader.isFadeIn = true;
        yield return new WaitForSeconds(0.1f);
        Save.maingameFlag = Save.MainGameFlag.THROW;

        yield return new WaitUntil(() => fader.isFadeIn == false);
    }


    void ThrowCameraMove()
    {
        //投げられてる時のカメラワークをここに実装

        trans.localPosition = Player.localPosition + new Vector3(2.6f, 8.6f, 0.21f);

        if (!isThrow)
        {
            trans.DOLocalRotate(new Vector3(60, -90, 0), 0);
            isThrow = true;
        }

        //trans.position = new Vector3(Player.position.x + 0.5f, Player.position.y + 4.09f, Player.position.z + 2.73f);
    }

    void SlowCameraMove()
    {

        if (!isThrow2)
        {
            trans.DOLocalRotate(new Vector3(90, -90, 0), 0.4f);
            isThrow2 = true;
        }
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
            trans.DOMove(Player.position + new Vector3(0, 1, 5), 2).SetEase(Ease.Linear)
        );
        seq.Join(
            trans.DORotate(new Vector3(0, 180, 0), 2).SetEase(Ease.Linear)
        );

        seq.Play();
        yield return new WaitUntil(() => PlayerAniFlag.GutEndPoint);
        seq.Kill();
        Sound.PlaySe("wasshoi");

        Player.GetComponent<Rigidbody>().useGravity = false;
        Save.maingameFlag = Save.MainGameFlag.ENDANIMETION;
        Save.maingameFlag = Save.MainGameFlag.ENDCAMERA;
        Save.maingameFlag = Save.MainGameFlag.RESULT;


        //Save.ThrowReSet();

    }

    override public IEnumerator GoodEffect()
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
            trans.DOMove(Player.position + new Vector3(-5, 1, 0), 2).SetEase(Ease.Linear)
        );
        seq.Join(
            trans.DORotate(new Vector3(0, 90, 0), 2).SetEase(Ease.Linear)
        );


        yield return new WaitUntil(() => PlayerAniFlag.GutEndPoint);
        seq.Kill();
        Player.GetComponent<Rigidbody>().useGravity = false;
        Save.maingameFlag = Save.MainGameFlag.ENDANIMETION;
        Save.maingameFlag = Save.MainGameFlag.ENDCAMERA;
        Save.maingameFlag = Save.MainGameFlag.RESULT;
        yield return null; yield return null;
    }

    override public IEnumerator BadEffect()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(
            trans.DOMove(Player.position + new Vector3(0, 5, 0), 1).SetEase(Ease.Linear)
        );
        seq.Join(
            trans.DORotate(new Vector3(90, 0, 0), 1).SetEase(Ease.Linear)
        );

        seq.Play();

        yield return new WaitUntil(() => PlayerAniFlag.GutEndPoint);
        seq.Kill();
        Player.GetComponent<Rigidbody>().useGravity = false;
        Save.maingameFlag = Save.MainGameFlag.ENDANIMETION;
        Save.maingameFlag = Save.MainGameFlag.ENDCAMERA;
        Save.maingameFlag = Save.MainGameFlag.RESULT;
        yield return null;

    }

    override public IEnumerator FailureNoUkemiEffect()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(
            trans.DOMove(Player.position + new Vector3(0, 5, 0), 1).SetEase(Ease.Linear)
        );
        seq.Join(
            trans.DORotate(new Vector3(90, 0, 0), 1).SetEase(Ease.Linear)
        );

        seq.Play();

        yield return new WaitUntil(() => PlayerAniFlag.GutEndPoint);
        seq.Kill();
        Player.GetComponent<Rigidbody>().useGravity = false;
        Save.maingameFlag = Save.MainGameFlag.ENDANIMETION;
        Save.maingameFlag = Save.MainGameFlag.ENDCAMERA;
        Save.maingameFlag = Save.MainGameFlag.RESULT;
        yield return null;

    }

    void NoUkemiCamereMove()
    {
        trans.position = Player.position + new Vector3(2f, 2f, 0f);
    }

}

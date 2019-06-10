using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstStageCameraManager : CameraManager {

    private bool isThrow;

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
            trans.DOMove(CornerMovePoint1, 1).SetEase(Ease.Linear)
        );
        seq.Join(
            transform.DORotate(CornerMoveRotate1, 0.2f).SetEase(Ease.Linear)
        );
    }

    public void StartCameraMove2(Sequence seq)
    {
        seq.Append(
            trans.DOMove(CornerMovePoint2, 2).SetEase(Ease.Linear)
        );

    }

    public void StartCameraMove3(Sequence seq)
    {
        seq.Append(
            transform.DORotate(CornerMoveRotate2, 1f).SetEase(Ease.Linear)
        );  
        seq.Join(
            trans.DOMove(ThrowMovePoint, 2).SetEase(Ease.Linear)
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

    void NoUkemiCamereMove()
    {
        trans.position = Player.position + new Vector3(2f, 2f, 0f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour {

    private Transform trans;
    [SerializeField]
    private Transform Player;

    /// <summary>
    /// 最初の異動先
    /// </summary>
    [SerializeField]
    private Vector3 FirstMovePoint;

    private bool isThrow;


	// Use this for initialization
	void Awake () {
        trans = transform;
        //StartCameraMove();
	}
    
    void FixedUpdate(){
        if (Save.maingameFlag == Save.MainGameFlag.FALLING)
            ThrowCameraMove();
    }

    public void StartCameraMove(Sequence seq){
        //Sequence seq = DOTween.Sequence();
        //Vector3 MovePoint = new Vector3(5,trans.position.y,trans.position.z);
        //seq.Append(
        //    trans.DOMove(MovePoint,1.0f)
        //);

        //MovePoint = new Vector3(1.28f,5.32f,-3.08f);
        //seq.Append(
        //    trans.DOMove(MovePoint,1.0f)
        //);

        //seq.Join(
        //       trans.DORotate(new Vector3(60,-20,0),1.0f)
        //);

        seq.Append(
            trans.DOMove(FirstMovePoint, 2).SetEase(Ease.Linear)
        );
        
        //seq.OnComplete(() => { 
        //    Save.maingameFlag = Save.MainGameFlag.STARTWAIT;
        //    print("wsdfg");
        //});

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

}

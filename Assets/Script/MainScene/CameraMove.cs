using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour {

    private Transform trans;

	// Use this for initialization
	void Start () {
        trans = transform;
        StartCameraMove();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void StartCameraMove(){
        Sequence seq = DOTween.Sequence();
        var MovePoint = new Vector3(5,trans.position.y,trans.position.z);
        seq.Append(
            transform.DOMove(MovePoint,1.0f)
        );

        MovePoint = new Vector3(1.28f,5.32f,-3.08f);
        seq.Append(
            trans.DOMove(MovePoint,1.0f)
        );

        seq.Join(
               trans.DORotate(new Vector3(60,-20,0),1.0f)
        );
    }

}

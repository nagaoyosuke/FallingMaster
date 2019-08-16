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


    // Start is called before the first frame update
    void OnEnable()
    {
        if(ani != null)
        {
            ani.CrossFade("BirdSnake", 0, 0,Random.Range(0, 10.0f));
        }
    }

    void Update()
    {
        
    }

    public void StartCameraMove1(Sequence seq)
    {
        seq.Join(transform.DOLocalMove(StartMovePosition, 2));
    }


}

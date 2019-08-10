using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarMove : MonoBehaviour
{
    private Vector3 StartPosition;
    [SerializeField]
    private Vector3 GoolPosition;
    [SerializeField]
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.localPosition;
        Move();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        print("a");
        transform.DOLocalMove(GoolPosition, time)
         .SetEase(Ease.Linear)
         .OnComplete(() =>
         {
             transform.localPosition = StartPosition;
             Move();
         });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private Vector3 StartMovePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartCameraMove1(Sequence seq)
    {
        seq.Join(transform.DOLocalMove(StartMovePosition, 2));
    }
}

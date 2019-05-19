using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartWalk : MonoBehaviour
{

    [SerializeField]
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartWalkAnimetion(Sequence seq)
    {
        seq.Join(
            transform.DOMove(new Vector3(0,0,2),10)
        );
            
        ani.SetBool("Walk",true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("Walk", false)));

    }
}

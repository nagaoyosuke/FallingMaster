using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 待機モーションを見せる時の管理用クラス(05/18)
/// </summary>
public class IdlingAnimetion : MonoBehaviour
{
    [SerializeField]
    private Animator Ani;

    /// <summary>
    /// 二人のMasterが全く同じ動きしてたら不自然やから開始フレームをずらす
    /// </summary>
    private float ram;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartAnimetion(Sequence seq)
    {
        ram = Random.Range(0, 20);
        Ani.CrossFade("Idle", ram);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アニメーションクリップでフラグを発火させたいときに使うクラス
/// </summary>
public class AnimetionFlag : MonoBehaviour
{
    /// <summary>
    /// 投げられるアニメーションが終わったとき
    /// </summary>
    public bool ThrowEnd;
    /// <summary>
    /// 投げられるアニメーションが終わる直前
    /// </summary>
    public bool ThrowRBPoint;
    /// <summary>
    /// 投げられるor投げるアニメーションが加速するとき
    /// </summary>
    public bool ThrowSpeedPoint;
    /// <summary>
    /// お辞儀アニメーションが終わるとき
    /// </summary>
    public bool BowEndPoint;
    /// <summary>
    /// その場受け身アニメーションが起き上がるとき
    /// </summary>
    public bool UkemiStandPoint;

    /// <summary>
    /// ガッツポーズアニメーションが終わるとき
    /// </summary>
    public bool GutEndPoint;
    /// <summary>
    /// 痛いポーズアニメーションが終わるとき
    /// </summary>
    public bool OuchEndPoint;
    /// <summary>
    /// 失敗アニメーションが終わるとき
    /// </summary>
    public bool ErrorEndPoint;
}

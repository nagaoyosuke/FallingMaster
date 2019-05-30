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
}

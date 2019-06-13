using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーンによって処理を変えたいから親クラスにした(06/10長尾)
/// </summary>
public class CameraManager : MonoBehaviour
{
    protected Transform trans;

    [SerializeField]
    protected Transform Player;

    [SerializeField]
    protected AnimetionFlag PlayerAniFlag;

    /// <summary>
    /// 最初に曲がるポイントまでの座標
    /// </summary>
    [SerializeField]
    protected Vector3 CornerMovePoint1;

    /// <summary>
    /// 最初に曲がる向き
    /// </summary>
    [SerializeField]
    protected Vector3 CornerMoveRotate1;

    /// <summary>
    /// 二番目に曲がるポイントまでの座標
    /// </summary>
    [SerializeField]
    protected Vector3 CornerMovePoint2;

    /// <summary>
    /// 二番目に曲がる向き
    /// </summary>
    [SerializeField]
    protected Vector3 CornerMoveRotate2;

    /// <summary>
    /// 投げられるポイントまでの座標
    /// </summary>
    [SerializeField]
    protected Vector3 ThrowMovePoint;

    protected bool isThrow;

    virtual public IEnumerator PerfectEffect() { yield return null; }

    virtual public IEnumerator GoodEffect() { yield return null; }

    virtual public IEnumerator BadEffect() { yield return null; }

    virtual public IEnumerator FailureNoUkemiEffect() { yield return null; }

    void Reset()
    {
        GameObject Player_ = GameObject.Find("UkemiMaster");
        Player = Player_.transform;
        PlayerAniFlag = Player.GetComponent<AnimetionFlag>();
    }

    void OnEnable()
    {
        isThrow = false;
    }
}

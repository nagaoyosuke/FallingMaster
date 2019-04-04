using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 着地ギリギリになったかどうかの判定(04/04 長尾)
/// </summary>
public class UkemiStartCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UkemiStart();
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    /// <summary>
    /// 受け身開始時のメソッド
    /// UpdateでSave.maingameFlagを監視してUKEMIになったら処理をするクラスに通知を飛ばす
    /// </summary>
    void UkemiStart()
    {
        Save.maingameFlag = Save.MainGameFlag.UKEMI;
    }
}

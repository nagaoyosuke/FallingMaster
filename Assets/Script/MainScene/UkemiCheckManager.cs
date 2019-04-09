using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受け身入力の判定を管理する(04/04 長尾)
/// </summary>
public class UkemiCheckManager : MonoBehaviour
{

    /// <summary>
    /// 受け身入力開始からPERFECT判定になるフレーム
    /// </summary>
    [SerializeField]
    private int parfectFlame;

    /// <summary>
    /// UkemiEffecManagerから成功失敗の演出参照
    /// </summary>
    [SerializeField]
    private UkemiEffecManager Effect;


    // Start is called before the first frame update
    void Start()
    {
        if (parfectFlame == 0)
            parfectFlame = 20;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UkemiStart();

            /// わかりやすくするために動きを止めてるだけの仮実装
            //other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //other.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    /// <summary>
    /// 受け身開始時のメソッド
    /// UpdateでSave.maingameFlagを監視してUKEMIになったら処理をするクラスに通知を飛ばす
    /// </summary>
    void UkemiStart()
    {
        Save.maingameFlag = Save.MainGameFlag.UKEMI;
        StartCoroutine(UkemiWait());
    }

    /// <summary>
    /// 受け身入力を待ってる間の処理
    /// </summary>
    /// <returns>The wait.</returns>
    IEnumerator UkemiWait()
    {
        int flame = 0;
        while (true)
        {
            /// 受け身入力成功
            if (Save.isUkemi)
            {
                if (flame < parfectFlame)
                    Save.ukemiRank = Save.UkemiRank.PERFECT;
                else
                    Save.ukemiRank = Save.UkemiRank.GOOD;

                break;
            }

            /// 床に当たったら(PlaneHit.cs参照)
            if (Save.ukemiRank == Save.UkemiRank.NOUKEMI)
                break;

            yield return new WaitForFixedUpdate();
            flame++;
        }

        ChoiceEffect();

        print(Save.ukemiRank);
    }

    /// <summary>
    /// UkemiRankに応じて演出切り替え
    /// </summary>
    void ChoiceEffect()
    {
        switch (Save.ukemiRank)
        {
            case Save.UkemiRank.NOUKEMI:
                Effect.FailureNoUkemiEffect();
                break;

            case Save.UkemiRank.BAD:
                Effect.BadEffect();
                break;

            case Save.UkemiRank.GOOD:
                Effect.GoodEffect();
                break;

            case Save.UkemiRank.PERFECT:
                Effect.PerfectEffect();
                break;
        }
    }

}

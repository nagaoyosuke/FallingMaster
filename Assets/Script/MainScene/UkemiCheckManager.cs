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

    [SerializeField]
    private GameObject EffectObject;

    /// <summary>
    /// UkemiEffecManagerから成功失敗の演出参照
    /// </summary>
    private IUkemiEffect Effect;

    private bool isAction;

    /// <summary>
    /// 受け身入力が開始された時に表示されるテキスト
    /// </summary>
    [HideInInspector]
    public GameObject UkemiStartText;

    void OnEnable()
    {
        isAction = false;
    }

    void Awake()
    {
        if (UkemiStartText == null)
        {
            UkemiStartText = GameObject.FindWithTag("UkemiText");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (parfectFlame == 0)
            parfectFlame = 20;

        Effect = EffectObject.GetComponent<IUkemiEffect>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isAction && other.gameObject.CompareTag("Player"))
        {
            if(Save.maingameFlag == Save.MainGameFlag.SLOWSTART)
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
        isAction = true;
        Sound.PlaySe("keikoku01");
        UkemiStartText.SetActive(true);


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

        Save.maingameFlag = Save.MainGameFlag.UKEMIANIMETION;

        yield return null;
        UkemiStartText.SetActive(false);

        Effect.StartEffect();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    /// PERFECT判定の前後のフレーム
    /// </summary>
    [SerializeField]
    private int parfectingFlame;

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

    private StampChange stampChange;

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
            parfectFlame = 50;

        if (parfectingFlame == 0)
            parfectingFlame = 5;

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

        var circle = GameObject.FindWithTag("UkemiCircle").GetComponent<RectTransform>();
        stampChange = GameObject.FindWithTag("UkemiStamp").GetComponent<StampChange>();
        stampChange.isChanging = false;

        int flame = 0;
        //float num = 1.0f / 60.0f;
        float num = 1.0f / parfectFlame;

        while (true)
        {
            circle.localScale -= new Vector3(num, num, 0);
            print(flame);
            /// 受け身入力成功
            if (Save.isUkemi)
            {
                var underflame = parfectFlame - parfectingFlame;
                var topflame = parfectFlame + parfectingFlame;

                if (underflame <= flame && flame < topflame)
                    Save.ukemiRank = Save.UkemiRank.PERFECT;
                else if ((underflame - 10 <= flame && flame < underflame) || (topflame <= flame && flame < topflame + 10))
                    Save.ukemiRank = Save.UkemiRank.GOOD;
                else
                    Save.ukemiRank = Save.UkemiRank.NOUKEMI;
                break;
            }

            /// 床に当たったら(PlaneHit.cs参照)
            if (Save.ukemiRank == Save.UkemiRank.NOUKEMI)
                break;

            yield return new WaitForSeconds((1.0f/(60.0f/Time.timeScale)));
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
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.BAD));
                break;

            case Save.UkemiRank.BAD:
                Effect.BadEffect();
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.BAD));
                break;

            case Save.UkemiRank.GOOD:
                Effect.GoodEffect();
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.GOOD));
                break;

            case Save.UkemiRank.PERFECT:
                Effect.PerfectEffect();
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.PARFECT));
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUkemiCheck : MonoBehaviour
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
    private float timeScale;

    [SerializeField]
    private GameObject EffectObject;

    /// <summary>
    /// UkemiEffecManagerから成功失敗の演出参照
    /// </summary>
    private IAddUkemiEffect Effect;

    private bool isAction;

    /// <summary>
    /// Trueにすると受け身入力ができないから危険地底にできる
    /// </summary>
    [SerializeField]
    private bool isDanger;

    private AddUkemiPlane Plane;

    private Rigidbody rb;
    private Vector3 vel;

    /// <summary>
    /// 受け身入力が開始された時に表示されるテキスト
    /// </summary>
    [HideInInspector]
    public GameObject UkemiStartText;

    private StampChange stampChange;

    [SerializeField]
    private bool isSlow = true;

    void OnEnable()
    {
        isAction = false;
        if (EffectObject == null){
            EffectObject = this.gameObject;
        }
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
        if(Plane == null)
        {
            Plane = transform.parent.GetComponentInChildren<AddUkemiPlane>();
        }
        if (parfectFlame == 0)
            parfectFlame = 20;
        if (parfectingFlame == 0)
            parfectingFlame = 5;
        if (timeScale == 0)
            timeScale = 0.4f;

        Effect = EffectObject.GetComponent<IAddUkemiEffect>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isAction && other.gameObject.CompareTag("Player"))
        {
            if (Save.maingameFlag == Save.MainGameFlag.FALLING)
            {
                rb = other.gameObject.GetComponentInParent<Rigidbody>();
                vel = rb.velocity;
                print(vel);
                UkemiStart();
            }
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
        Save.AddUkemiReSet();
        Save.maingameFlag = Save.MainGameFlag.ADDUKEMI;
        isAction = true;
        if (!isDanger)
        {
            Sound.PlaySe("keikoku01");
            UkemiStartText.SetActive(true);
        }
        StartCoroutine(UkemiWait());
    }

    /// <summary>
    /// 受け身入力を待ってる間の処理
    /// </summary>
    /// <returns>The wait.</returns>
    IEnumerator UkemiWait()
    {
        var circle = GameObject.FindWithTag("UkemiCircle").GetComponent<RectTransform>();
        circle.localScale = new Vector3(2, 2, 0);

        stampChange = GameObject.FindWithTag("UkemiStamp").GetComponent<StampChange>();
        stampChange.isChanging = false;

        int flame = 0;
        //float num = 1.0f / 60.0f;
        float num = 1.0f / parfectFlame;


        if (isSlow)
        {
            Time.timeScale = timeScale;

            rb.interpolation = RigidbodyInterpolation.Interpolate;

            float f = 40.0f;
            Vector3 v = rb.velocity;
            Vector3 vel_ = v / f;
            rb.velocity = vel_;
        }

        while (true)
        {
            circle.localScale -= new Vector3(num, num, 0);
            print(flame);
            /// 受け身入力成功
            if (Save.isAddUkemi)
            {
                var underflame = parfectFlame - parfectingFlame;
                var topflame = parfectFlame + parfectingFlame;

                if (underflame <= flame && flame < topflame)
                    Save.addUkemiRank = Save.AddUkemi.PERFECT;
                else if ((underflame - 2 <= flame && flame < underflame) || (topflame <= flame && flame < topflame + 2))
                    Save.addUkemiRank = Save.AddUkemi.PERFECT;
                else
                    Save.addUkemiRank = Save.AddUkemi.NOUKEMI;
                break;
            }

            /// 床に当たったら(PlaneHit.cs参照)
            if (Save.addUkemiRank == Save.AddUkemi.NOUKEMI)
                break;

            if (flame > parfectFlame + parfectingFlame + 30)
            {
                print("timeout");
                Save.addUkemiRank = Save.AddUkemi.NOUKEMI;
                break;
            }

            yield return new WaitForSeconds((1.0f / (60.0f /timeScale)));
            flame++;
        }
        if (isSlow)
        {
            rb.interpolation = RigidbodyInterpolation.None;

            Time.timeScale = 1.0f;
            rb.velocity = vel;
        }


        if (isDanger)
            Save.addUkemiRank = Save.AddUkemi.NOUKEMI;

        print(Save.addUkemiRank);
        Save.maingameFlag = Save.MainGameFlag.ADDUKEMIANIMETION;
        Effect.AddStartEffect();

        int time = 0;
        while (true)
        {
            if (Save.addUkemiRank == Save.AddUkemi.NOUKEMI)
                break;

            if (Plane.isHit)
                break;

            if (time > 45)
            {
                print("timeout");
                break;
            }

            yield return new WaitForFixedUpdate();
            time++;

        }


        yield return null;
        UkemiStartText.SetActive(false);

        ChoiceEffect();

    }

    /// <summary>
    /// UkemiRankに応じて演出切り替え
    /// </summary>
    void ChoiceEffect()
    {
        switch (Save.addUkemiRank)
        {
            case Save.AddUkemi.NOUKEMI:
                Effect.AddFailureNoUkemiEffect();
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.BAD));
                break;

            case Save.AddUkemi.PERFECT:
                Save.AddUkemiPoint++;
                Effect.AddPerfectEffect();
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.PARFECT));
                break;
        }
    }
}

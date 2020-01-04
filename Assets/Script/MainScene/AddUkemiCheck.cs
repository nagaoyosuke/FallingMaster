using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUkemiCheck : MonoBehaviour
{
    /// <summary>
    /// 受け身入力開始からPERFECT判定になるフレーム
    /// </summary>
    [SerializeField]
    public int parfectFlame;

    /// <summary>
    /// PERFECT判定の前後のフレーム
    /// </summary>
    [SerializeField]
    public int parfectingFlame;

    /// <summary>
    /// GOOD判定の前後のフレーム
    /// </summary>
    [SerializeField]
    public int goodingFlame = 2;


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

    /// <summary>
    /// ベクトルと受身するモデルを反転するかどうか
    /// AddUkemiGeneralとAddUkemiPlaneのisInversionにも渡す
    /// </summary>
    [SerializeField]
    public bool isInversion;


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
        if (goodingFlame == 0)
            parfectingFlame = 2;
        if (goodingFlame < 0)
            parfectingFlame = 0;
        if (timeScale <= 0)
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
                else if ((underflame - goodingFlame <= flame && flame < underflame) || (topflame <= flame && flame < topflame + goodingFlame))
                    Save.addUkemiRank = Save.AddUkemi.GOOD;
                else
                    Save.addUkemiRank = Save.AddUkemi.NOUKEMI;
                break;
            }

            /// 床に当たったら(PlaneHit.cs参照)
            /// 追加受身できる場所の床は判定ないから関係ない
            if (Save.addUkemiRank == Save.AddUkemi.NOUKEMI)
                break;

            //長い気がするから30 -> 10　に変更した(12/24)
            if (flame > parfectFlame + parfectingFlame + 10)
            {
                print("ukemi timeout");
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


        Plane.isInversion = isInversion;

        //地面に着くまで受身を待ってる
        int time = 0;
        while (true)
        {
            if (Save.addUkemiRank == Save.AddUkemi.NOUKEMI)
                break;

            if (Plane.isHit)
                break;

            if (time > 25)
            {
                print("plane timeout");
                break;
            }

            yield return new WaitForFixedUpdate();
            time++;

        }


        yield return null;
        UkemiStartText.SetActive(false);

        //反転処理
        InversionChange();

        ChoiceEffect();

    }

    /// <summary>
    /// 反転処理
    /// </summary>
    void InversionChange()
    {
        if (isInversion)
        {

            //モデル反転
            var ang = rb.transform.localEulerAngles;
            rb.transform.localEulerAngles = new Vector3(ang.x, ang.y * -1, ang.z);

            //それぞれでベクトル反転
            //Plane.isInversion = isInversion;
            Effect.isInversion = isInversion;
        }
    }

    /// <summary>
    /// UkemiRankに応じて演出切り替え
    /// </summary>
    void ChoiceEffect()
    {
        switch (Save.addUkemiRank)
        {
            case Save.AddUkemi.NOUKEMI:
                Save.addUkemiCombo = 0;
                Effect.AddFailureNoUkemiEffect();
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.BAD));
                break;

            case Save.AddUkemi.GOOD:
                Save.AddUkemiPoint++;
                Save.addUkemiCombo = 0;

                Effect.AddPerfectEffect();
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.GOOD));
                break;

            case Save.AddUkemi.PERFECT:
                Save.AddUkemiPoint++;
                Save.addUkemiCombo++;

                if (Save.stageState == Save.StageState.ENDLESS)
                    Save.AddUkemiPoint++;

                Effect.AddPerfectEffect();
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.PARFECT));
                break;
        }


        if (Save.stageState == Save.StageState.ENDLESS)
        {
            ComboCheck();
            ScoreCheck();
        }
    }

    void ComboCheck()
    {
        GameObject.FindGameObjectWithTag("Combo").GetComponent<ComboCounter>().TextChange();
    }

    void ScoreCheck()
    {
        GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreCount>().TextChange();
    }

    /// <summary>
    /// エンドレスで受身判定を調整する用
    /// </summary>
    /// <param name="parfectFlame"></param>
    /// <param name="parfectingFlame"></param>
    /// <param name="goodingFlame"></param>
    public void FrameChange(int parfectFlame, int parfectingFlame, int goodingFlame)
    {
        if (parfectFlame == 0)
            parfectFlame = 16;
        if (parfectingFlame == 0)
            parfectingFlame = 1;
        if (goodingFlame == 0)
            parfectingFlame = 1;

        this.parfectFlame = parfectFlame;
        this.parfectingFlame = parfectingFlame;
        this.goodingFlame = goodingFlame;
    }
}

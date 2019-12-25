using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHit : MonoBehaviour
{
    [SerializeField]
    private GameObject Splash;

    [SerializeField]
    private GameObject Bubble;

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
    private IUkemiEffect Effect;

    private bool isAction;

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
        if (EffectObject == null)
        {
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
        if (Plane == null)
        {
            Plane = transform.parent.GetComponentInChildren<AddUkemiPlane>();
        }
        if (parfectFlame == 0)
            parfectFlame = 20;
        if (parfectingFlame == 0)
            parfectingFlame = 5;
        if (timeScale <= 0)
            timeScale = 0.4f;

        Effect = EffectObject.GetComponent<IUkemiEffect>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isAction && other.gameObject.CompareTag("Player"))
        {
            if (Save.ukemiRank != Save.UkemiRank.NONE)
                return;

            rb = other.gameObject.GetComponentInParent<Rigidbody>();
            vel = rb.velocity;
            print(vel);
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
        Save.AddUkemiReSet();
        Save.maingameFlag = Save.MainGameFlag.ADDUKEMI;
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

            //float f = 40.0f;
            //Vector3 v = rb.velocity;
            //Vector3 vel_ = v / f;
            rb.velocity = Vector3.zero;
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
                    Save.ukemiRank = Save.UkemiRank.GOOD;
                else
                    Save.ukemiRank = Save.UkemiRank.NOUKEMI;
                break;
            }

            //長い気がするから30 -> 10　に変更した(12/24)
            if (flame > parfectFlame + parfectingFlame + 3)
            {
                print("ukemi timeout");
                Save.ukemiRank = Save.UkemiRank.NOUKEMI;
                break;
            }

            yield return new WaitForSeconds((1.0f / (60.0f / timeScale)));
            flame++;
        }
        if (isSlow)
        {
            rb.interpolation = RigidbodyInterpolation.None;

            Time.timeScale = 1.0f;
            //rb.velocity = vel;
        }

        print(Save.addUkemiRank);
        Save.maingameFlag = Save.MainGameFlag.ADDUKEMIANIMETION;
        Effect.StartEffect();


        //地面に着くまで受身を待ってる
        //int time = 0;
        //while (true)
        //{
        //    if (Save.addUkemiRank == Save.AddUkemi.NOUKEMI)
        //        break;

        //    if (Plane.isHit)
        //        break;

        //    if (time > 25)
        //    {
        //        print("plane timeout");
        //        break;
        //    }

        //    yield return new WaitForFixedUpdate();
        //    time++;

        //}


        yield return null;
        UkemiStartText.SetActive(false);

        ChoiceEffect();

    }

    /// <summary>
    /// UkemiRankに応じて演出切り替え
    /// </summary>
    void ChoiceEffect()
    {
        switch (Save.ukemiRank)
        {
            case Save.UkemiRank.NOUKEMI:
                rb.velocity = vel;
                StartCoroutine(WaterEffect());
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.BAD));
                break;

            case Save.UkemiRank.GOOD:
                rb.useGravity = false;
                rb.transform.position += new Vector3(0, 1, 0);
                Effect.GoodEffect();
                StartCoroutine(stampChange.StampChangeView(StampChange.Stamp.GOOD));
                break;
        }
    }

    IEnumerator WaterEffect()
    {
        var pos = rb.transform.position;
        var spl = Instantiate(Splash) as GameObject;
        spl.transform.position = pos;
        spl.transform.position += new Vector3(0, 2, 0);
        Time.timeScale = 1.0f;
        Sound.PlaySe("waterdive");
        Save.ukemiRank = Save.UkemiRank.BAD;
        Effect.BadEffect();
        var bub = Instantiate(Bubble) as GameObject;
        bub.transform.position = pos;
        bub.transform.position += new Vector3(0, 2, 0);

        yield return new WaitForSeconds(0.8f);

        rb.useGravity = false;
        rb.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        //Save.ukemiRank = Save.UkemiRank.BAD;
        //effect.BadEffect();

        yield return new WaitForSeconds(1.0f);

        Save.maingameFlag = Save.MainGameFlag.RESULT;


    }
}

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
        int flame = 0;
        while (true)
        {
            /// 受け身入力成功
            if (Save.isAddUkemi)
            {
                if (flame < parfectFlame)
                    Save.addUkemiRank = Save.AddUkemi.PERFECT;
                else
                    Save.addUkemiRank = Save.AddUkemi.NOUKEMI;

                break;
            }

            /// 床に当たったら(AddUkemiPlane.cs参照)
            if (Save.addUkemiRank == Save.AddUkemi.NOUKEMI)
                break;

            yield return new WaitForFixedUpdate();
            flame++;
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
                //Save.AddUkemiPoint = 0;
                break;

            case Save.AddUkemi.PERFECT:
                Save.AddUkemiPoint++;
                Effect.AddPerfectEffect();
                break;
        }
    }
}

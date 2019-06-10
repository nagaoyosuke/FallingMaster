using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 受け身関連の演出(04/04 長尾)
/// </summary>
public class UkemiEffecManager : MonoBehaviour
{

    /// <summary>
    /// 受け身するオブシェクト
    /// </summary>
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Rigidbody PlayerRb;

    [SerializeField]
    private BoxCollider PlayerBox;

    [SerializeField]
    private Animator PlayerAni;

    /// <summary>
    /// 受け身入力が開始された時に表示されるテキスト
    /// </summary>
    [SerializeField]
    private Text UkemiStartText;

    private bool isEnd;

    void Start()
    {
        UkemiStartText.enabled = false;
    }

    void Update()
    {
        if (!isEnd)
        {
            if (Save.maingameFlag == Save.MainGameFlag.UKEMI)
            {
                Effect();
                isEnd = true;
            }
        }
    }

    /// <summary>
    /// ここに共通の受け身開始時の演出かく
    /// </summary>
    void Effect()
    {
        UkemiStartText.enabled = true;
    }

    /// <summary>
    /// 完璧成功時の演出書く
    /// </summary>
    public void PerfectEffect()
    {
        PlayerRb.velocity = Vector3.zero;
        Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        //Player.transform.position += new Vector3(0, -3, 0);
        PlayerAni.SetBool("UkemiSonoba",true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiSonoba", false)));
        PlayerAni.SetBool("UkemiPerfect", true);
        StartCoroutine(DelayClass.DelayCoroutin(60 * 12, () => PlayerAni.SetBool("UkemiPerfect", false)));
        //StartCoroutine(DelayClass.DelayCoroutin(661, () => PlayerAni.SetBool("UkemiPerfect", false)));


    }

    /// <summary>
    /// 成功時の演出書く
    /// </summary>
    public void GoodEffect()
    {
        PlayerRb.velocity = Vector3.zero;
        Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        //Player.transform.position += new Vector3(0, -3, 0);
        PlayerAni.SetBool("UkemiSonoba", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiSonoba", false)));
        PlayerAni.SetBool("UkemiGood", true);
        StartCoroutine(DelayClass.DelayCoroutin(60 * 12, () => PlayerAni.SetBool("UkemiGood", false)));
    }


    /// <summary>
    /// 失敗時の演出書く
    /// </summary>
    public void BadEffect()
    {
        //PlayerRb.velocity = Vector3.zero;
        //Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        PlayerAni.SetBool("UkemiBad", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiBad", false)));
    }

    /// <summary>
    /// 受け身入力せず着地した時の演出書く
    /// </summary>
    public void FailureNoUkemiEffect()
    {
        //PlayerRb.velocity = Vector3.zero;
        //Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        //PlayerRb.AddForce(new Vector3(0, 500, -60));
        PlayerAni.SetBool("UkemiBad", true);
        //Player.transform.position += new Vector3(0, 0, 1);

        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiBad", false)));
    }
}

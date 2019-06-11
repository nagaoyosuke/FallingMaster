using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 投げる投げられる両方のアニメーションなどの管理
/// </summary>
public class Throw : MonoBehaviour
{

    private bool isStart;

    [SerializeField]
    private Animator ThrowAni;
    [SerializeField]
    private Animator UkemiAni;
    [SerializeField]
    private GameObject UnderBody;
    [SerializeField]
    private AnimetionFlag UkemiFlag;

    // Update is called once per frame
    void Update(){
        if (!isStart && Save.maingameFlag == Save.MainGameFlag.THROW)
            StartCoroutine(ThrowAnime());
    }

    IEnumerator ThrowAnime(){
        //ここに投げるアニメーションができたら実装する
        isStart = true;
        //GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -200.0f));
        ThrowAni.SetBool("Throw1", true);
        UkemiAni.SetBool("Throwing1", true);
        yield return null;
        ThrowAni.SetBool("Throw1", false);
        UkemiAni.SetBool("Throwing1", false);

        var rb = UkemiAni.gameObject.GetComponent<Rigidbody>();

        //投げるアニメーションの再生速度を早くする
        yield return new WaitUntil(() => UkemiFlag.ThrowSpeedPoint);
        ThrowAni.speed = 2;
        UkemiAni.speed = 2;


        //アニメーションクリップのほうでフラグをオンにしてる
        yield return new WaitUntil(() => UkemiFlag.ThrowRBPoint);
        rb.constraints = RigidbodyConstraints.None; //物理演算で回転が影響するように
        rb.useGravity = true;   //重力オン
        rb.AddForce(new Vector3(0, 0, -100));   //手前に落ちるから力を与えて自然に

        //アニメーションクリップのほうでフラグをオンにしてる
        yield return new WaitUntil(() => UkemiFlag.ThrowEnd);
        UkemiAni.transform.position = UnderBody.transform.position;
        UkemiAni.transform.position += new Vector3(0, 0, -1);   //長られるアニメーションから落ちるアニメーションに変更したときに座標がずれるから
        UkemiAni.SetBool("Fall", true);
        Save.maingameFlag = Save.MainGameFlag.FALLING;
        ThrowAni.speed = 1;
        UkemiAni.speed = 1;
        yield return null;
        UkemiAni.SetBool("Fall", false);

        ThrowAni.SetBool("Idle", true);
        yield return null;
        ThrowAni.SetBool("Idle", false);
    }
}

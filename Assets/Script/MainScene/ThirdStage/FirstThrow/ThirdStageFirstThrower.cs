using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThirdStageFirstThrower : ThrowManager
{

    // Update is called once per frame
    void Update()
    {
        if (!isStart && Save.maingameFlag == Save.MainGameFlag.THROW)
        {
            StartCoroutine(anime());
        }
    }

    public void StartBowAnimetion(Sequence seq)
    {

        seq.AppendCallback(() =>
        {
            ani.SetBool("BowT", true);
            StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("BowT", false)));
            StartCoroutine(BowEnd());
        });
    }

    IEnumerator BowEnd()
    {
        yield return new WaitUntil(() => aniFlag.BowEndPoint);
        if (Save.stageState == Save.StageState.SIMPLESTAGE3)
        {
            Save.maingameFlag = Save.MainGameFlag.THROW;
            StartCoroutine(anime());
        }
        else
        {
            ani.SetBool("Idle", true);
            StartCoroutine(DelayClass.DelayCoroutin(1, () => ani.SetBool("Idle", false)));
        }
    }

    IEnumerator anime()
    {
        //ここに投げるアニメーションができたら実装する
        isStart = true;
        //GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -200.0f));
        ani.SetBool("Throw1", true);
        UkemiAni.SetBool("Throwing1", true);
        yield return new WaitForEndOfFrame();

        ani.SetBool("Throw1", false);
        UkemiAni.SetBool("Throwing1", false);

        var rb = UkemiAni.gameObject.GetComponent<Rigidbody>();

        //投げるアニメーションの再生速度を早くする
        //yield return new WaitUntil(() => UkemiFlag.ThrowSpeedPoint);
        yield return new WaitForSeconds(174.0f/(30.0f * ani.speed));
        ani.speed = 2;
        UkemiAni.speed = 2;

        Sound.PlaySe("hoi");
        print("SpeedPoint");


        //アニメーションクリップのほうでフラグをオンにしてる
        //yield return new WaitUntil(() => UkemiFlag.ThrowRBPoint);
        yield return new WaitForSeconds(34.0f/ (30.0f * ani.speed));

        //rb.constraints = RigidbodyConstraints.None; //物理演算で回転が影響するように
        rb.useGravity = true;   //重力オン
        if (Save.stageState == Save.StageState.SIMPLESTAGE3)
        {
            float rad = SimpleAngle.z * Mathf.Deg2Rad;
            Vector3 vec = new Vector3(0, Mathf.Cos(rad), -Mathf.Sin(rad));
            rb.AddForce(vec * 2000);   //手前に落ちるから力を与えて自然に
        }
        else
        {
            float rad = AngleArrow.localEulerAngles.z * Mathf.Deg2Rad;
            Vector3 vec = new Vector3(0, Mathf.Cos(rad), -Mathf.Sin(rad));
            rb.AddForce(vec * 2000);   //手前に落ちるから力を与えて自然に
        }


        //アニメーションクリップのほうでフラグをオンにしてる
        Sound.PlaySe("bakuhatu");
		Sound.PlayBgm("Result3");
		rb.AddForce(new Vector3(0, Save.windZ * 10, 0));
        print("ThrowRBPoint");

        //アニメーションクリップのほうでフラグをオンにしてる
        //yield return new WaitUntil(() => UkemiFlag.ThrowEnd);
        yield return new WaitForSeconds(19.0f/ (30.0f * ani.speed));

        print("ThrowEndPoint");

        UkemiAni.transform.position = UnderBody.transform.position;
        //UkemiAni.transform.position += new Vector3(0, 0, -1);   //長られるアニメーションから落ちるアニメーションに変更したときに座標がずれるから
        UkemiAni.SetBool("Fall", true);
        Save.maingameFlag = Save.MainGameFlag.FALLING;
        ani.speed = 1;
        UkemiAni.speed = 1;
        yield return null;
        UkemiAni.SetBool("Fall", false);

        ani.SetBool("Idle", true);
        yield return null;
        ani.SetBool("Idle", false);
    }
}

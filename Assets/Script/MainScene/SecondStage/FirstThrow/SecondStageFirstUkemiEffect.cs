using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondStageFirstUkemiEffect : UkemiEffect, IUkemiEffect
{
    public GameObject smokeParticle;

    /// <summary>
    /// 共通の受け身開始時の演出(追加受け身は除く)
    /// </summary>
    public void StartEffect()
    {
        int random = Random.Range(1, 3);

        Sound.PlaySe("syakin");

        if (random == 1)
        {
            StartCoroutine(DelayClass.DelayCoroutin(20, () => Sound.PlaySe("seiya")));

        }
        else
        {
            StartCoroutine(DelayClass.DelayCoroutin(20, () => Sound.PlaySe("soiya")));
        }
    }

    /// <summary>
    /// 共通の受け身終了時の演出(追加受け身は除く)
    /// </summary>
    public void EndEffect()
    {
        Save.maingameFlag = Save.MainGameFlag.UKEMIEFFECT;
        //UkemiStartText.SetActive(false);

        //0615 仮のパーティクル(時々正しく表示されなくなるバグ有り)豊田
        GameObject particle = Instantiate(smokeParticle, Player.transform.position, Quaternion.identity) as GameObject;
        var ps = particle.GetComponent<ParticleSystem>();
        ps.Stop();

        var main = ps.main;
        main.customSimulationSpace = Player.transform;
        //particle.transform.parent = Player.gameObject.transform;
        particle.transform.position = Player.gameObject.transform.position;
        StartCoroutine(PlayParticle(ps, particle));
        //particle.transform.rotation = new Quaternion(0,90,0,0);
        //ここまで

        //Save.ThrowReSet();
    }

    private IEnumerator PlayParticle(ParticleSystem ps, GameObject particle)
    {
        yield return new WaitForSeconds(1f);
        Sound.PlaySe("ukemi01");

        var shape = ps.shape;
        shape.rotation = new Vector3(90, 0, 0);
        shape.position = new Vector3(0, 0, -1f);
        ps.Play();
        Destroy(particle, 5f);


    }

    /// <summary>
    /// 完璧成功時の演出書く(追加受け身は除く)
    /// </summary>
    public void PerfectEffect()
    {
        PlayerRb.velocity = Vector3.zero;
        //Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        //Player.transform.position += new Vector3(0, -3, 0);
        PlayerBox.material = null;
        PlayerAni.SetBool("UkemiSonoba", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiSonoba", false)));
        PlayerAni.SetBool("UkemiPerfect", true);
        StartCoroutine(DelayClass.DelayCoroutin(60 * 12, () => PlayerAni.SetBool("UkemiPerfect", false)));
        //StartCoroutine(DelayClass.DelayCoroutin(661, () => PlayerAni.SetBool("UkemiPerfect", false)));
        StartCoroutine(CameraMove.PerfectEffect());
        EndEffect();
    }

    /// <summary>
    /// 成功時の演出書く(追加受け身は除く)
    /// </summary>
    public void GoodEffect()
    {
        PlayerRb.velocity = Vector3.zero;
        //Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        //Player.transform.position += new Vector3(0, -3, 0);
        PlayerBox.material = null;

        PlayerAni.SetBool("UkemiSonoba", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiSonoba", false)));
        PlayerAni.SetBool("UkemiGood", true);
        StartCoroutine(DelayClass.DelayCoroutin(60 * 12, () => PlayerAni.SetBool("UkemiGood", false)));
        StartCoroutine(CameraMove.GoodEffect());

        EndEffect();

    }


    /// <summary>
    /// 失敗時の演出書く(追加受け身は除く)
    /// </summary>
    public void BadEffect()
    {
        //PlayerRb.velocity = Vector3.zero;
        //Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        PlayerAni.SetBool("UkemiBad", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiBad", false)));
        StartCoroutine(CameraMove.BadEffect());

        EndEffect();

    }

    /// <summary>
    /// 受け身入力せず着地した時の演出書く(追加受け身は除く)
    /// </summary>
    public void FailureNoUkemiEffect()
    {
        //PlayerRb.velocity = Vector3.zero;
        //Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        //PlayerRb.AddForce(new Vector3(0, 500, -60));
        PlayerAni.SetBool("UkemiBad", true);
        //Player.transform.position += new Vector3(0, 0, 1);

        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiBad", false)));
        StartCoroutine(CameraMove.FailureNoUkemiEffect());

        EndEffect();

    }
}

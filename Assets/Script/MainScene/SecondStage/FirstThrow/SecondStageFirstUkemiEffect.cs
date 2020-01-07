using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondStageFirstUkemiEffect : UkemiEffect, IUkemiEffect
{
    public GameObject smokeParticle;
    [SerializeField] private GameObject smartPhoneParticle;
    [SerializeField] private GameObject passerby;
    [SerializeField] private GameObject passerby_2;

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

        GameObject particle = Instantiate(smokeParticle) as GameObject;
        particle.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);
        var ps = particle.GetComponent<ParticleSystem>();
        ps.Stop();

        StartCoroutine(PlayParticle(ps, particle));

        StartCoroutine(aa());
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
    /// 通行人パシャパシャ
    /// </summary>
    /// <returns>The aa.</returns>
    private IEnumerator aa()
    {
        //別スクリプトに分ける
        var minoru = GameObject.Find("UkemiMaster").transform.Find("Person");
        var front = Instantiate(passerby, minoru.transform.parent.transform) as GameObject;
        var back = Instantiate(passerby_2, minoru.transform.parent.transform) as GameObject;


        front.transform.localPosition = new Vector3(0, 0, 0);
        back.transform.localPosition = new Vector3(0, 0, 0);
        back.transform.Rotate(0, 180, 0);

        var front_circle = front.GetComponent<CircleDeployer>();
        var back_circle = back.GetComponent<CircleDeployer>();

        front_circle._radius = 19.50f;
        back_circle._radius = 21.00f;

        //4.5
        for (int i = 0; i <= 150; i++)
        {
            front_circle._radius -= 0.10f;
            back_circle._radius -= 0.10f;

            front_circle.OnValidate();
            back_circle.OnValidate();

            yield return null;
        }

        back.transform.Rotate(0, 180, 0);

        GameObject particleObject = Instantiate(smartPhoneParticle) as GameObject;
        particleObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2, Player.transform.position.z);

        Sound.PlaySe("walk02");
        Sound.PlaySe("dash02");
        yield return new WaitForSeconds(2.0f);
        particleObject.GetComponent<ParticleSystem>().Play();
        Sound.PlaySe("shutter");

        yield return null;
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

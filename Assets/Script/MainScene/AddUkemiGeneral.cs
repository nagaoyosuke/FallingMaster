using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AddUkemiGeneral : AddUkemiEffect, IAddUkemiEffect
{
    public GameObject smokeParticle;

    [SerializeField]
    private Vector3 MoveVector;

    /// <summary>
    /// 共通の追加受け身開始時の演出
    /// </summary>
    public void AddStartEffect()
    {
        int random = Random.Range(0, 1);

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
    /// 共通の追加受け身終了時の演出
    /// </summary>
    public void AddEndEffect()
    {
        //Save.maingameFlag = Save.MainGameFlag.ADDUKEMIEFFECT;
        UkemiStartText.SetActive(false);

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
        yield return new WaitForSeconds(0f);

        var shape = ps.shape;
        shape.rotation = new Vector3(90, 0, 0);
        shape.position = new Vector3(0, 0, 0.6f);
        ps.Play();
        Destroy(particle, 5f);


    }

    /// <summary>
    /// 追加受け身成功時の演出
    /// </summary>
    public void AddPerfectEffect()
    {
        PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, 0, PlayerRb.velocity.z);
        var pos = Player.transform.position + MoveVector;
        //Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        //Player.transform.position += new Vector3(0, -3, 0);
        StartCoroutine(Move());
        PlayerBox.material = null;
        PlayerAni.SetBool("UkemiMae", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiMae", false)));
        //PlayerAni.SetBool("UkemiPerfect", true);
        //StartCoroutine(DelayClass.DelayCoroutin(60 * 12, () => PlayerAni.SetBool("UkemiPerfect", false)));
        //StartCoroutine(DelayClass.DelayCoroutin(661, () => PlayerAni.SetBool("UkemiPerfect", false)));
        //StartCoroutine(CameraMove.PerfectEffect());
        Save.maingameFlag = Save.MainGameFlag.FALLING;
        Sound.PlaySe("ukemi01");

        AddEndEffect();
    }

    public IEnumerator Move()
    {
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForFixedUpdate();
            Player.transform.localPosition += new Vector3(MoveVector.x/ 60.0f, MoveVector.z / 60.0f, MoveVector.z / 60.0f);
        }
    }

    /// <summary>
    /// 共通の追加受け身開失敗時の演出
    /// </summary>
    public void AddFailureNoUkemiEffect()
    {
        //PlayerRb.velocity = Vector3.zero;
        //Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        //PlayerRb.AddForce(new Vector3(0, 500, -60));
        PlayerAni.SetBool("UkemiBad", true);
        //Player.transform.position += new Vector3(0, 0, 1);

        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiBad", false)));
        //StartCoroutine(CameraMove.FailureNoUkemiEffect());
        Save.maingameFlag = Save.MainGameFlag.RESULT;

        AddEndEffect();
    }
}

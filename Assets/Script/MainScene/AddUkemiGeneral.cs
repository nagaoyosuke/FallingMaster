using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AddUkemiGeneral : AddUkemiEffect, IAddUkemiEffect
{
    /// <summary>
    ///動物とかに受け身したときになかせるため
    /// </summary>
    [SerializeField]
    private string UkemiSoundName;


    public GameObject smokeParticle;
    public GameObject FlashParticle;

    //public GameObject OtherParticle;


    [SerializeField]
    private Vector3 MovePower;

    /// <summary>
    /// ベクトルを反転するかどうか
    /// AddYkemiCheckで入れられる
    /// </summary>
    [HideInInspector]
    public bool isInversion
    {
        get;
        set;
    }

    /// <summary>
    /// 共通の追加受け身開始時の演出
    /// </summary>
    public void AddStartEffect()
    {
        int random = Random.Range(0, 2);

        //Sound.PlaySe("syakin");

        if (random == 1)
        {
            StartCoroutine(DelayClass.DelayCoroutin(20, () => Sound.PlaySe("yo")));

        }
        else
        {
            StartCoroutine(DelayClass.DelayCoroutin(20, () => Sound.PlaySe("hun")));
        }
    }

    /// <summary>
    /// 共通の追加受け身終了時の演出
    /// </summary>
    public void AddEndEffect()
    {
        GameObject particle = Instantiate(smokeParticle) as GameObject;
        particle.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);
        var ps = particle.GetComponent<ParticleSystem>();
        ps.Stop();

        StartCoroutine(PlayParticle(ps, particle));
    }

    private IEnumerator PlayParticle(ParticleSystem ps, GameObject particle)
    {
        yield return new WaitForSeconds(0f);

        //var shape = ps.shape;
        //shape.rotation = new Vector3(90, 0, 0);
        //shape.position = new Vector3(0, 0, 0.6f);
        ps.Play();
        Destroy(particle, 5f);


    }

    /// <summary>
    /// 追加受け身成功時の演出
    /// </summary>
    public void AddPerfectEffect()
    {
        Save.addUkemiCounter++;

        //跳ねないように一時的にyベクトルを0に
        PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, 0, PlayerRb.velocity.z);
        //受身成功時にかかる力、持続的にかかるやつはAddUkemiPlaneにある
        PlayerRb.AddForce(MovePower);

        if (isInversion)
        {
            //PlayerRb.velocity = new Vector3(PlayerRb.velocity.x * -1, 0, PlayerRb.velocity.z * -1);
        }

        //var pos = Player.transform.position + MoveVector;
        //Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        //Player.transform.position += new Vector3(0, -3, 0);
        //StartCoroutine(Move());
        PlayerBox.material = null;
        PlayerAni.SetBool("UkemiMae", true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => PlayerAni.SetBool("UkemiMae", false)));
        //PlayerAni.SetBool("UkemiPerfect", true);
        //StartCoroutine(DelayClass.DelayCoroutin(60 * 12, () => PlayerAni.SetBool("UkemiPerfect", false)));
        //StartCoroutine(DelayClass.DelayCoroutin(661, () => PlayerAni.SetBool("UkemiPerfect", false)));
        //StartCoroutine(CameraMove.PerfectEffect());

        GameObject flash = Instantiate(FlashParticle, Player.transform.position, Quaternion.identity) as GameObject;
        flash.transform.parent = Player.gameObject.transform;
        flash.transform.localPosition = new Vector3(0, 0, 0);

        Save.maingameFlag = Save.MainGameFlag.FALLING;

        if (UkemiSoundName == "")
            UkemiSoundName = "ukemi01";

        Sound.PlaySe(UkemiSoundName);

        AddEndEffect();
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

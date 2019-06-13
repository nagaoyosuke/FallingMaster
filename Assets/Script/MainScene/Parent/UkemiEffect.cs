using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// インスペクターからResetすると自動で参照してくれるから最後に押すと便利
/// </summary>
public class UkemiEffect : MonoBehaviour
{
    [SerializeField]
    protected CameraManager CameraMove;
    /// <summary>
    /// 受け身するオブシェクト
    /// </summary>
    [SerializeField]
    protected GameObject Player;

    [SerializeField]
    protected Rigidbody PlayerRb;

    [SerializeField]
    protected BoxCollider PlayerBox;

    [SerializeField]
    protected Animator PlayerAni;

    /// <summary>
    /// 受け身入力が開始された時に表示されるテキスト
    /// </summary>
    [SerializeField]
    protected Text UkemiStartText;

    protected bool isEnd;


    void Reset()
    {
        Player = GameObject.Find("UkemiMaster");
        PlayerRb = Player.GetComponent<Rigidbody>();
        PlayerAni = Player.GetComponent<Animator>();
        PlayerBox = Player.GetComponentInChildren<BoxCollider>();
    }

    void OnEnable()
    {
        isEnd = false;
    }
}

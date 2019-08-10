﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUkemiEffect : MonoBehaviour
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
	[HideInInspector]
	public GameObject UkemiStartText;

    protected bool isEnd;

    //void Reset()
    //{
    //    //CameraMove = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();

    //    Player = GameObject.FindWithTag("Player");
    //    PlayerRb = Player.GetComponent<Rigidbody>();
    //    PlayerAni = Player.GetComponent<Animator>();
    //    PlayerBox = Player.GetComponentInChildren<BoxCollider>();
    //}

    void OnEnable()
    {
        isEnd = false;
    }

    void Awake()
    {
        if (UkemiStartText == null)
        {
            UkemiStartText = GameObject.FindWithTag("UkemiText");
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (!isEnd)
        {
            if (Save.maingameFlag == Save.MainGameFlag.ADDUKEMI)
            {
                //UkemiStartText.SetActive(true);
                isEnd = true;
            }
        }
    }
}
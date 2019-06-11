using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(GameObject), typeof(AnimetionFlag))]
public class ThrowManager : WalkManager
{
    protected bool isStart;

    [SerializeField]
    protected Animator UkemiAni;
    [SerializeField]
    protected GameObject UnderBody;
    [SerializeField]
    protected AnimetionFlag UkemiFlag;

    override protected void Reset()
    {
        ani = GetComponent<Animator>();
        aniFlag = GetComponent<AnimetionFlag>();

        GameObject ukemi = GameObject.Find("UkemiMaster");
        UkemiAni = ukemi.GetComponent<Animator>();
        UkemiFlag = ukemi.GetComponent<AnimetionFlag>();
        UnderBody = GameObject.Find("UkemiMaster/Bone/Bone_ALL/Under_Body");
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スローモーションをRigidBodyで再現するクラス(03/30長尾)
/// </summary>
public class SlowManager : MonoBehaviour
{
    Transform Tartget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();
            StartCoroutine(Slow(rb));
        }
    }

    IEnumerator Slow(Rigidbody rb){
        Tartget = rb.transform;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        float f = 40.0f;
        Vector3 v = rb.velocity;
        Vector3 vel = v / f;
        Vector3 ang = rb.angularVelocity;
        Vector3 anve = ang / f;
        rb.velocity = vel;
        rb.angularVelocity = anve;
        Save.maingameFlag = Save.MainGameFlag.SLOWSTART;
        yield return new WaitUntil(() => Save.maingameFlag == Save.MainGameFlag.UKEMI);

    }
}

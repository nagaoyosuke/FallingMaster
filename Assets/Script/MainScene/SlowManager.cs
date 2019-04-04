using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スローモーションをRigidBodyで再現するクラス(03/30長尾)
/// </summary>
public class SlowManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Rigidbody rb = other.GetComponent<Rigidbody>();
            StartCoroutine(Slow(rb));
        }
    }

    IEnumerator Slow(Rigidbody rb){
        float f = 10.0f;
        Vector3 v = rb.velocity;
        Vector3 vel = v / f;
        Vector3 ang = rb.angularVelocity;
        Vector3 anve = ang / f;
        while (true){
            if (Save.maingameFlag == Save.MainGameFlag.UKEMI)
                break;
            rb.velocity = vel;
            rb.angularVelocity = anve;
            yield return new WaitForFixedUpdate();
        }
    }
}

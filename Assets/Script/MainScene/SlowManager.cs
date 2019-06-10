using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スローモーションをRigidBodyで再現するクラス(03/30長尾)
/// </summary>
public class SlowManager : MonoBehaviour
{
    private bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player") && !isHit){
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();
            StartCoroutine(Slow(rb));
        }
    }

    IEnumerator Slow(Rigidbody rb){
        isHit = true;
        Time.timeScale = 0.4f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        float f = 40.0f;
        Vector3 v = rb.velocity;
        Vector3 vel = v / f;
        rb.velocity = vel;
        Save.maingameFlag = Save.MainGameFlag.SLOWSTART;
        yield return new WaitUntil(() => Save.maingameFlag == Save.MainGameFlag.UKEMIANIMETION);
        rb.interpolation = RigidbodyInterpolation.None;
        Time.timeScale = 1.0f;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundForce : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(Save.maingameFlag == Save.MainGameFlag.SLOWSTART)
        {
            if(other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponentInParent<Rigidbody>().AddForce(new Vector3(0, 0, 100));
            }
        }
    }
}

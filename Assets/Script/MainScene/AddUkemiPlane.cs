using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUkemiPlane : MonoBehaviour
{
    private float count;


    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && enabled)
        {
            if (!Save.isAddUkemi && Save.maingameFlag == Save.MainGameFlag.ADDUKEMI)
            {
                //other.gameObject.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
                //other.gameObject.GetComponentInParent<Rigidbody>().useGravity = false;
                //other.gameObject.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                //other.transform.rotation = new Quaternion(0, 0, 0, 0);
                Save.addUkemiRank = Save.AddUkemi.NOUKEMI;
                print("NoAddUkemi");
            }

            if (Save.maingameFlag == Save.MainGameFlag.FALLING)
            {

                var vec = other.gameObject.GetComponentInParent<Rigidbody>().velocity;

                DelayClass.DelayCoroutin(30,() => 
                {
                    if(Save.maingameFlag == Save.MainGameFlag.FALLING)
                    {
                        other.gameObject.GetComponentInParent<Rigidbody>().velocity = vec;
                        print("WallHitVelocity");
                    }
                });
            }

            //other.gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
            //other.gameObject.GetComponentInParent<Rigidbody>().useGravity = false;
            //other.gameObject.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;

        }
    }

    /// <summary>
    /// 積み防止用 08/19
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionStay(Collision collision)
    {
        if (Save.maingameFlag == Save.MainGameFlag.FALLING)
        {
            count += Time.deltaTime;
            if(count > 3)
            {
                Save.maingameFlag = Save.MainGameFlag.RESULT;
            }
        }

    }


    private void OnDisable()
    {
        GetComponent<BoxCollider>().enabled = false;

    }
}

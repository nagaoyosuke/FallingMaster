using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUkemiPlane : MonoBehaviour
{
    private float count;
    private Rigidbody rb;

    public bool isHit;

    [SerializeField]
    private Vector3 Power;

    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<BoxCollider>().enabled = true;
        if(Power.x == 0 && Power.y == 0 && Power.z == 0)
        {
            Power = new Vector3(0, 0, 10f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && enabled)
        {
            if (rb == null)
                rb = other.gameObject.GetComponentInParent<Rigidbody>();

            if (!Save.isAddUkemi && Save.maingameFlag == Save.MainGameFlag.ADDUKEMI)
            {
                //other.gameObject.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
                //other.gameObject.GetComponentInParent<Rigidbody>().useGravity = false;
                //other.gameObject.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                //other.transform.rotation = new Quaternion(0, 0, 0, 0);
                Save.addUkemiRank = Save.AddUkemi.NOUKEMI;
                print("NoAddUkemi");
            }

            if (Save.isAddUkemi && Save.maingameFlag == Save.MainGameFlag.ADDUKEMIANIMETION)
            {
                isHit = true;
            }


            if (Save.maingameFlag == Save.MainGameFlag.FALLING)
            {
                var vec = rb.velocity;

                DelayClass.DelayCoroutin(30,() => 
                {
                    if(Save.maingameFlag == Save.MainGameFlag.FALLING)
                    {
                        if (rb.velocity.z > 10)
                        {
                            rb.velocity = vec;
                            print("WallHitVelocity");
                        }
                        
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
            if (rb == null)
                rb = collision.gameObject.GetComponentInParent<Rigidbody>();

            //rb.velocity += new Vector3(0, 0,10f);
            //rb.transform.position += new Vector3(0, 0, 0.25f);
            rb.velocity = Power;

            print("Power");
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

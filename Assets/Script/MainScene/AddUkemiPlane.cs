using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUkemiPlane : MonoBehaviour
{
    private float count;
    private Rigidbody rb;

    /// <summary>
    /// addukemicheck.csでみてる
    /// </summary>
    public bool isHit;

    [SerializeField]
    private Vector3 Power;

    /// <summary>
    /// ベクトルを反転するかどうか(主にエンドレスを想定)
    /// AddYkemiCheckで入れられる
    /// </summary>
    [HideInInspector]
    public bool isInversion;

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

            //追加受身のタイミングはAddUkemiCheck.csでフレーム管理してるから地面判定はいらない
            //if (!Save.isAddUkemi && Save.maingameFlag == Save.MainGameFlag.ADDUKEMI)
            //{
            //    //other.gameObject.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
            //    //other.gameObject.GetComponentInParent<Rigidbody>().useGravity = false;
            //    //other.gameObject.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //    //other.transform.rotation = new Quaternion(0, 0, 0, 0);
            //    //Save.addUkemiRank = Save.AddUkemi.NOUKEMI;
            //    print("NoAddUkemi");
            //}

            if (Save.isAddUkemi && Save.maingameFlag == Save.MainGameFlag.ADDUKEMIANIMETION)
            {
                isHit = true;
            }


            if (Save.maingameFlag == Save.MainGameFlag.FALLING)
            {
                var vec = rb.velocity;

                if (isInversion)
                {

                    Power = new Vector3(Power.x * -1, Power.y, Power.z * - 1);

                }

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
    /// 実際は受身した時に前に進むのも兼ねているから消すと動かなくなる
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && enabled)
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
                if (count > 3)
                {
                    Save.maingameFlag = Save.MainGameFlag.RESULT;
                }
            }
        }

    }


    private void OnDisable()
    {
        GetComponent<BoxCollider>().enabled = false;

    }
}

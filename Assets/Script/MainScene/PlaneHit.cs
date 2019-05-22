using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受け身入力せずに落ちた場合に低評価にするクラス(04/06 長尾)
/// </summary>
public class PlaneHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Save.isUkemi)
            {
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.transform.rotation = new Quaternion(0, 0, 0, 0);
                Save.ukemiRank = Save.UkemiRank.NOUKEMI;
            }
        }
    }
}

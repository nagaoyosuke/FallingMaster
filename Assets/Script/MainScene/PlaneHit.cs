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
        if (other.gameObject.tag == "Player")
        {
            if (!Save.isUkemi)
            {
                Save.ukemiRank = Save.UkemiRank.NOUKEMI;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受け身の入力(04/06　長尾)
/// </summary>
public class UkemiButton : MonoBehaviour
{

    private bool isPush;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPush) {
            if (Save.maingameFlag == Save.MainGameFlag.UKEMI) {
                if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
                {
                    isPush = true;
                    Push();
                }
            }
        }
       
    }

    /// <summary>
    /// ここにタップした時の事を書く
    /// もしくはSave.isUkemiのフラグを監視する
    /// </summary>
    void Push()
    {
        Save.isUkemi = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タップしたらスタートするクラス(03/30長尾)
/// </summary>
public class TapStart : MonoBehaviour
{

    [SerializeField]
    private Text text;

    private bool isEne;

    void Start(){
        text.enabled = false;
    }

    void Update(){
        if (!isEne){
            if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT){
                text.enabled = true;
                isEne = true;
            }
        }
    }

    public void Tap(){
        Save.maingameFlag = Save.MainGameFlag.THROW;
    }

}

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


    void OnEnable()
    {
        isEne = false;
    }

    void Start(){
        text.enabled = false;
    }

    void Update()
    {
        if (Save.maingameFlag == Save.MainGameFlag.STARTWAIT)
        {

            if (!isEne)
            {
                {
                    text.enabled = true;
                    isEne = true;
                }
            }

            else if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                Tap();
            }
        }
    }

    public void Tap(){
        Save.maingameFlag = Save.MainGameFlag.THROW;
        text.enabled = false;
    }
}

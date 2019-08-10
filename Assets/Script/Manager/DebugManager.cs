using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // SendMessageでエラーが出ないように
class DebugManager : MonoBehaviour
{

    [SerializeField]
    private float WindZ;
    [SerializeField]
    private Save.MainGameFlag MainGameFlag;

    public void Change()
    {
        Save.windZ = WindZ;
        Save.maingameFlag = MainGameFlag;
        Debug.Log("Change！！");
    }

    public void CheckSet()
    {
        WindZ = Save.windZ;
        MainGameFlag = Save.maingameFlag;
        Debug.Log("Set!!");
    }
}

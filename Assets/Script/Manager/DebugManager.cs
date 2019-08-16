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
    [SerializeField]
    private Save.UkemiRank ukemiRank;
    [SerializeField]
    private int addUkemiPoint;

	public void Change()
    {
        Save.windZ = WindZ;
        Save.maingameFlag = MainGameFlag;
        Save.ukemiRank = ukemiRank;
        Save.AddUkemiPoint = addUkemiPoint;
        Debug.Log("Change！！");
    }

    public void CheckSet()
    {
        WindZ = Save.windZ;
        MainGameFlag = Save.maingameFlag;
        ukemiRank = Save.ukemiRank;
        addUkemiPoint = Save.AddUkemiPoint;
        Debug.Log("Set!!");
    }
}

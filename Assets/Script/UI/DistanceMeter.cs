using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceMeter : MonoBehaviour
{
    public RectTransform IconTrs;
    public Text NumberT;
    public Text CommmaNumberT;

    private Transform PlayerTrs;
    private Transform DistanceTrs;

    private float StartPosY;
    private float AddY;

    private float MaxRectPosY = 600;
    private float MinRectPosY = -550;

    private bool isGeting;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        GetTrs();
    }

    // Update is called once per frame
    void Update()
    {
 
        //Save.MainGameFlag.FALLINGが5
        if ((int)(Save.maingameFlag) > 4)
            ChengeIcon();
    }

    void GetTrs()
    {
        isGeting = true;
        PlayerTrs = GameObject.FindGameObjectWithTag("Player").transform;
        DistanceTrs = GameObject.FindGameObjectWithTag("DistancePoint").transform;

        StartPosY = PlayerTrs.position.y;
        AddY = (MaxRectPosY - MinRectPosY) / (StartPosY - DistanceTrs.position.y);

        ChangeText(PlayerTrs.position.y - DistanceTrs.position.y);

    }

    void ChengeIcon()
    {
        var distanceY = PlayerTrs.position.y - DistanceTrs.position.y;
        var posY = (distanceY * AddY) + MinRectPosY;

        if (posY < MinRectPosY)
            return;

        IconTrs.localPosition = new Vector3(0, posY, 0);


        ChangeText(distanceY);
    }

    void ChangeText(float distance)
    {
        if(Save.stageState != Save.StageState.STAGE3)
            distance = distance / 4;

        var floor = Mathf.FloorToInt(distance);
        var commma = Mathf.FloorToInt((distance - floor) * 100);

        var ct = commma.ToString();

        if (commma - 10 <= 0)
            ct = "0" + ct;

        NumberT.text = floor.ToString();
        CommmaNumberT.text = ct;
    }
}

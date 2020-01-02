using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceMeter : MonoBehaviour
{
    public RectTransform IconTrs;

    private Transform PlayerTrs;
    private Transform DistanceTrs;

    private float StartPosY;
    private float AddY;

    private float MaxRectPosY = 600;
    private float MinRectPosY = -550;

    private bool isGeting;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Save.maingameFlag == Save.MainGameFlag.FALLING)
            if (!isGeting)
                GetTrs();
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

    }

    void ChengeIcon()
    {
        var distanceY = ((PlayerTrs.position.y - DistanceTrs.position.y) * AddY);
        var posY = distanceY + MinRectPosY;

        if (posY < MinRectPosY)
            return;

        IconTrs.localPosition = new Vector3(0, posY, 0);
    }
}

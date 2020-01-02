using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessDistanceMeter : MonoBehaviour
{
    public RectTransform IconTrs;
    public Text NumberT;
    public Text CommmaNumberT;

    private float distance;

    private float MaxRectPosY = 600;
    private float MinRectPosY = -550;

    // Update is called once per frame
    void Update()
    {
        //Save.MainGameFlag.FALLINGが5
        if ((int)(Save.maingameFlag) > 4)
            ChangeText();

        //Save.MainGameFlag.FALLINGが5
        if (Save.maingameFlag == Save.MainGameFlag.RESULT)
            Save.distance = distance;
    }

    void ChangeText()
    {
        if (distance <= 0)
            StartCoroutine(_ChangeText());
    }

    IEnumerator _ChangeText()
    {

        while (true)
        {
            distance += 0.03f;

            var floor = Mathf.FloorToInt(distance);
            var commma = Mathf.FloorToInt((distance - floor) * 100);

            var ct = commma.ToString();

            if (commma - 10 <= 0)
                ct = "0" + ct;

            NumberT.text = floor.ToString();
            CommmaNumberT.text = ct;

            var posY = IconTrs.localPosition.y - 1;

            if (posY < MinRectPosY)
                posY = MaxRectPosY;

            IconTrs.localPosition = new Vector3(0, posY, 0);

            yield return new WaitForSeconds(0.01f);
        }
    }
}

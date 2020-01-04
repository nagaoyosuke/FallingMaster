using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    [SerializeField]
    private Text ScoreNumber;

    [SerializeField]
    private Text AddScore;

    public void TextChange()
    {
        ScoreChange();
        ScoreNumber.text = Save.UkemiScore.ToString();
    }

    void ScoreChange()
    {
        int basePoint = 0;
        switch (Save.addUkemiRank)
        {
            case Save.AddUkemi.PERFECT:
                basePoint = 20;
                break;
            case Save.AddUkemi.GOOD:
                basePoint = 10;
                break;
        }
        var point = Save.addUkemiCombo * basePoint + basePoint;

        Save.UkemiScore += point;
        AddScoreChange(point);
    }

    void AddScoreChange(int point)
    {
        AddScore.enabled = true;
        AddScore.text = "+" + point.ToString() + "点";

        StartCoroutine(DelayClass.DelayCoroutin(90, () => AddScore.enabled = false));
    }
}

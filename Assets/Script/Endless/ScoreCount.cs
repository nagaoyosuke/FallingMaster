using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    [SerializeField]
    private Text ScoreNumber;

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
                basePoint = 2;
                break;
            case Save.AddUkemi.GOOD:
                basePoint = 1;
                break;
        }

        Save.UkemiScore += Save.addUkemiCombo * basePoint;
        
    }
}

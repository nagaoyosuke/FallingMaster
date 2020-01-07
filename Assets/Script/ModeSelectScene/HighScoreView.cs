using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour
{
    [SerializeField]
    private Text Simple;
    [SerializeField]
    private Text Noremal;
    [SerializeField]
    private Text Endless;

    // Start is called before the first frame update
    void Start()
    {
        Simple.text = TextCheck(Save.HighSimpleRank);
        Noremal.text = TextCheck(Save.HighRank);
        Endless.text = Save.UkemiHighScore.ToString();
    }

    string TextCheck(Save.Rank target)
    {
        string t = "皆伝";
        switch (target)
        {
            case Save.Rank.FIRST:
                t = "初段";
                break;
            case Save.Rank.SECOND:
                t = "弐段";
                break;
            case Save.Rank.THIRD:
                t = "参段";
                break;
            case Save.Rank.FOURTH:
                t = "四段";
                break;
            case Save.Rank.FIFTH:
                t = "伍段";
                break;
            case Save.Rank.SIXTH:
                t = "六段";
                break;
            case Save.Rank.SEVENTH:
                t = "七段";
                break;
            case Save.Rank.EIGHTH:
                t = "八段";
                break;
            case Save.Rank.NINTH:
                t = "九段";
                break;
            case Save.Rank.TENTH:
                t = "十段";
                break;
            case Save.Rank.MASTER:
                t = "皆伝";
                break;

        }

        return t;
    }
}

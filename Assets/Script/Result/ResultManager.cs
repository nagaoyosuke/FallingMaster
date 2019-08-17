using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Text ResultText;
    [SerializeField]
    private Text AddUkemiText;
    [SerializeField]
    private Text AddUkemiTextPoint;
    [SerializeField]
    private Text MainUkemiText;
    [SerializeField]
    private Text MainUkemiTextPoint;
    [SerializeField]
    private List<Text> ResultWaitText;
    [SerializeField]
    private GameObject Right;
    [SerializeField]
    private GameObject Left;
    [SerializeField]
    private Image BeforRank;
    [SerializeField]
    private Text BeforRankText;
    [SerializeField]
    private Text AfterRankText;
    [SerializeField]
    private Text LeftRankText;
    [SerializeField]
    private Text RightRankText;

    [SerializeField]
    private ScreenFader fader;

    [SerializeField]
    private Animator ani;

    [SerializeField]
    private Image Hanko;

    [SerializeField]
    private Sprite Huka;
    [SerializeField]
    private Sprite Kiwami;

    [SerializeField]
    private ResultttFlag flag;

    private enum Rank
    {
        BAD,
        GOOD,
        PARFECT
    }

    private Rank rank;

    private int MaxPoint;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Save.AddUkemiPoint = 10;
        Save.UkemiPoint = 9;
        ani.enabled = false;
        //AlphaSet();
        MaxPoint = Save.UkemiPoint + Save.AddUkemiPoint;

        ActiveSet();
        fader.gameObject.SetActive(true);
        fader.isFadeIn = true;
        yield return new WaitForEndOfFrame();
        UkemiPointCheck();
        StartCoroutine(View());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AlphaSet()
    {
        ResultText.color = new Color(1, 1, 1, 0);
        AddUkemiText.color = new Color(1, 1, 1, 0);
        AddUkemiTextPoint.color = new Color(1, 1, 1, 0);
        MainUkemiText.color = new Color(1, 1, 1, 0);
        MainUkemiTextPoint.color = new Color(1, 1, 1, 0);
        //ResultWaitText.color = new Color(1, 1, 1, 0);
    }

    void ActiveSet()
    {
        ResultText.enabled = false;
        AddUkemiText.enabled = false;
        AddUkemiTextPoint.enabled = false;
        MainUkemiText.enabled = false;
        MainUkemiTextPoint.enabled = false;
        foreach(Text t in ResultWaitText)
        {
            t.enabled = false;
        }
        Right.SetActive(false);
        Left.SetActive(false);
        BeforRank.gameObject.SetActive(false);
        //AfterRank.SetActive(false);
    }

    IEnumerator View()
    {
        ResultText.enabled = true;
        yield return new WaitForSeconds(1);

        AddUkemiText.enabled = true;
        yield return new WaitForSeconds(1);

        AddUkemiTextPoint.enabled = true;
        yield return new WaitForSeconds(1);

        MainUkemiText.enabled = true;
        yield return new WaitForSeconds(1);

        MainUkemiTextPoint.enabled = true;
        yield return new WaitForSeconds(1);

        foreach (Text t in ResultWaitText)
        {
            t.enabled = true;
            yield return new WaitForSeconds(1);
        }

        BeforRank.gameObject.SetActive(true);
        BeforRank.color = new Color(1, 1, 1, 0);
        BeforRankText.color = new Color(1, 1, 1, 0);

        for (int i = 0; i < 60 * 3; i++)
        {
            yield return null;
            BeforRankText.color = new Color(1, 1, 1, 60 * 3 / 255.0f * i * 2 / 255.0f);
            BeforRank.color = new Color(1, 1, 1, 60 * 3 / 255.0f * i * 2 / 255.0f);
        }

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        int aniint = MaxPoint / 2;

        ani.SetInteger("Attack", aniint);
        ani.enabled = true;

        for(int i = 1; i <= aniint; i++)
        {
            yield return new WaitUntil(() => flag.isAttackStart == true);
            AfterTextChange(i);
            yield return new WaitUntil(() => flag.isAttackEnd == true);
            if (i >= 10)
            {
                ani.SetBool("Stop", true);
                BeforTextChange(i);
                yield break;
            }
            else
            {
                ani.SetInteger("Attack", aniint - i);
                BeforTextChange(i);

            }
        }
        ani.SetInteger("Attack", 0);

    }

    void UkemiPointCheck()
    {
        bool isOut = false;
        //int point = 0;
        //switch (Save.ukemiRank)
        //{
        //    case Save.UkemiRank.PERFECT:
        //        point = 2;
        //        break;
        //    case Save.UkemiRank.GOOD:
        //        point = 1;
        //        break;
        //    case Save.UkemiRank.NOUKEMI:
        //        point = 0;
        //        isOut = true;
        //        break;
        //}

        MainUkemiTextPoint.text = Save.UkemiPoint.ToString();
        AddUkemiTextPoint.text = Save.AddUkemiPoint.ToString();


        if (MaxPoint > 6 && !isOut)
        {
            rank = Rank.PARFECT;
            Hanko.sprite = Kiwami;
        }
        else if (MaxPoint > 0 && !isOut)
            rank = Rank.GOOD;
        else
        {
            rank = Rank.BAD;
            Hanko.sprite = Huka;
        }
        if (isOut)
        {
            rank = Rank.BAD;
            Hanko.sprite = Huka;
        }

        ani.SetInteger("Attack", MaxPoint / 2);
    }

    void BeforTextChange(int point)
    {
        print(point);
        switch ((Save.Rank)point)
        {
            case Save.Rank.FIRST:
                BeforRankText.text = "初段";
                //AfterRankText.text = "弐段";
                //LeftRankText.text = "初 ";
                //RightRankText.text = " 段";
                break;
            case Save.Rank.SECOND:
                BeforRankText.text = "弐段";
                //AfterRankText.text = "参段";
                //LeftRankText.text = "弐　";
                //RightRankText.text = "　段";
                break;
            case Save.Rank.THIRD:
                BeforRankText.text = "参段";
                //AfterRankText.text = "四段";
                //LeftRankText.text = "参　";
                //RightRankText.text = "　段";
                break;
            case Save.Rank.FOURTH:
                BeforRankText.text = "四段";
                //AfterRankText.text = "伍段";
                //LeftRankText.text = "四　";
                //RightRankText.text = "　段";
                break;
            case Save.Rank.FIFTH:
                BeforRankText.text = "伍段";
                //AfterRankText.text = "六段";
                //LeftRankText.text = "伍　";
                //RightRankText.text = "　段";
                break;
            case Save.Rank.SIXTH:
                BeforRankText.text = "六段";
                //AfterRankText.text = "七段";
                //LeftRankText.text = "六　";
                //RightRankText.text = "　段";
                break;
            case Save.Rank.SEVENTH:
                BeforRankText.text = "七段";
                //AfterRankText.text = "八段";
                //LeftRankText.text = "七　";
                //RightRankText.text = "　段";
                break;
            case Save.Rank.EIGHTH:
                BeforRankText.text = "八段";
                //AfterRankText.text = "九段";
                //LeftRankText.text = "八　";
                //RightRankText.text = "　段";
                break;
            case Save.Rank.NINTH:
                BeforRankText.text = "九段";
                //AfterRankText.text = "十段";
                //LeftRankText.text = "九　";
                //RightRankText.text = "　段";
                break;
            case Save.Rank.TENTH:
                BeforRankText.text = "十段";
                //AfterRankText.text = "皆伝";
                //LeftRankText.text = "十　";
                //RightRankText.text = "　段";
                break;
            case Save.Rank.MASTER:
                BeforRankText.text = "皆伝";
                //AfterRankText.text = "皆伝";
                //LeftRankText.text = "十　";
                //RightRankText.text = "　段";
                break;

        }
    }

    void AfterTextChange(int point)
    {
        print(point);
        switch ((Save.Rank)point)
        {
            case Save.Rank.FIRST:
                //BeforRankText.text = "初段";
                AfterRankText.text = "弐段";
                LeftRankText.text = "初 ";
                RightRankText.text = " 段";
                break;
            case Save.Rank.SECOND:
                //BeforRankText.text = "弐段";
                AfterRankText.text = "弐段";
                LeftRankText.text = "初 ";
                RightRankText.text = " 段";
                break;
            case Save.Rank.THIRD:
                //BeforRankText.text = "参段";
                AfterRankText.text = "参段";
                LeftRankText.text = "弐　";
                RightRankText.text = "　段";
                break;
            case Save.Rank.FOURTH:
                //BeforRankText.text = "四段";
                AfterRankText.text = "四段";
                LeftRankText.text = "参　";
                RightRankText.text = "　段";
                break;
            case Save.Rank.FIFTH:
                //BeforRankText.text = "伍段";
                AfterRankText.text = "伍段";
                LeftRankText.text = "四　";
                RightRankText.text = "　段";
                break;
            case Save.Rank.SIXTH:
                //BeforRankText.text = "六段";
                AfterRankText.text = "六段";
                LeftRankText.text = "伍　";
                RightRankText.text = "　段";
                break;
            case Save.Rank.SEVENTH:
                //BeforRankText.text = "七段";
                AfterRankText.text = "七段";
                LeftRankText.text = "六　";
                RightRankText.text = "　段";
                break;
            case Save.Rank.EIGHTH:
                //BeforRankText.text = "八段";
                AfterRankText.text = "八段";
                LeftRankText.text = "七　";
                RightRankText.text = "　段";
                break;
            case Save.Rank.NINTH:
                //BeforRankText.text = "九段";
                AfterRankText.text = "九段";
                LeftRankText.text = "八　";
                RightRankText.text = "　段";
                break;
            case Save.Rank.TENTH:
                //BeforRankText.text = "十段";
                AfterRankText.text = "十段";
                LeftRankText.text = "九　";
                RightRankText.text = "　段";
                break;
            case Save.Rank.MASTER:
                //BeforRankText.text = "皆伝";
                AfterRankText.text = "皆伝";
                LeftRankText.text = "十　";
                RightRankText.text = "　段";
                break;

        }
    }


}



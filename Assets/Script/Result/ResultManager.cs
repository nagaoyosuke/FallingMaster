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

    private float time;
    private int tapCount;
    private bool isSkip;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        Sound.PlayBgm("Result1");
        time = 0;
        //Save.AddUkemiPoint_ = 2;
        //Save.UkemiPoint = 8;
        ani.enabled = false;
        //AlphaSet();
        MaxPoint = Save.UkemiPoint + Save.AddUkemiPoint_;

        ActiveSet();
        fader.gameObject.SetActive(true);
        fader.isFadeIn = true;
        yield return new WaitForEndOfFrame();
        UkemiPointCheck();
        StartCoroutine(TextView());

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSkip)
        {
            time += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && time > 1.0f)
            {
                tapCount++;
                time = 0;
                if (tapCount == 1)
                {
                    Skip1();
                    StartCoroutine(RankView());
                }
            }
        }
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

    IEnumerator TextView()
    {
        ResultText.enabled = true;
        Sound.PlaySe("taiko02");
        yield return new WaitForSeconds(2);

        if (tapCount > 0)
            yield break;

        AddUkemiText.enabled = true;
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1);

        if (tapCount > 0)
            yield break;

        AddUkemiTextPoint.enabled = true;
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1);

        if (tapCount > 0)
            yield break;

        MainUkemiText.enabled = true;
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1);

        if (tapCount > 0)
            yield break;

        MainUkemiTextPoint.enabled = true;
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1.25f);

        Sound.PlaySe("taiko01");


        foreach (Text t in ResultWaitText)
        {
            if (tapCount > 0)
                yield break;

            t.enabled = true;
            yield return new WaitForSeconds(1);
        }

        if (tapCount > 0)
            yield break;

        StartCoroutine(RankView());
    }

    IEnumerator RankView()
    {
        BeforRank.gameObject.SetActive(true);
        BeforRank.color = new Color(1, 1, 1, 0);
        BeforRankText.color = new Color(1, 1, 1, 0);

        for (int i = 0; i < 60 * 2f; i++)
        {
            yield return null;
            //BeforRankText.color = new Color(1, 1, 1, 60.0f * 1.5f / 255.0f * i * 2 / 255.0f);
            //BeforRank.color = new Color(1, 1, 1, 60.0f * 1.5f / 255.0f * i * 2 / 255.0f);

            BeforRankText.color += new Color(0, 0, 0, 255.0f / 60.0f * 2f / 255.0f);
            BeforRank.color += new Color(0, 0, 0, 255.0f / 60.0f * 2f / 255.0f);
        }

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        int aniint = MaxPoint / 2;
		print(aniint);
        Save.rank = (Save.Rank)aniint;
        ani.SetInteger("Attack", aniint);
        ani.enabled = true;

        for (int i = 1; i <= aniint; i++)
        {
            if (tapCount >= 2 && i < 10)
            {
                i = aniint;
                if (i >= 10)
                {
                    i = 10;
                }
            }

            //flag.is~はCanvasのアニメーションで管理してる
            yield return new WaitUntil(() => flag.isAttackStart == true);
            AfterTextChange(i);
            yield return new WaitUntil(() => flag.isAttackEnd == true);
            if (i >= 10)
            {
                ani.SetBool("Stop", true);
                BeforTextChange(i);
                Sound.PlaySe("cheer01");
                Sound.PlaySe("yeah01");
                yield break;
            }
            else
            {
                ani.SetInteger("Attack", aniint - i);
                BeforTextChange(i);

            }


        }

        isSkip = true;

		//yield return new WaitUntil(() => flag.isAttackStart == true);
		//yield return new WaitUntil(() => flag.isAttackStart == true);
		yield return new WaitUntil(() => flag.isAttackEnd == true);
		yield return new WaitForSeconds(0.1f);

		AfterTextChange(aniint + 1);
		//yield return new WaitUntil(() => flag.isAttackEnd == true);
		ani.SetInteger("Attack", 0);
        yield return new WaitForSeconds(0.5f);

        Sound.PlaySe("beshi");
        yield return new WaitForSeconds(0.6f);

        HighScoreCheck();

        switch (rank)
        {
            case Rank.PARFECT:
                //perfect時のみby豊田
                Sound.PlaySe("cheer01");
                Sound.PlaySe("yeah01");
               break;
            case Rank.GOOD:
				Sound.PlaySe("cheer01");
				break;
            case Rank.BAD:

                break;
        }

    }

    void Skip1()
    {
        ResultText.enabled = true;
        AddUkemiText.enabled = true;
        AddUkemiTextPoint.enabled = true;
        MainUkemiText.enabled = true;
        MainUkemiTextPoint.enabled = true;
        foreach (Text t in ResultWaitText)
        {
            t.enabled = true;
        }
        Sound.PlaySe("taiko01");

    }

    void HighScoreCheck()
    {
        if(Save.stageState == Save.StageState.STAGE1 || Save.stageState == Save.StageState.STAGE2 || Save.stageState == Save.StageState.STAGE3)
            if (Save.rank > Save.HighRank)
                Save.HighRank = Save.rank;
        if (Save.stageState == Save.StageState.SIMPLESTAGE1 || Save.stageState == Save.StageState.SIMPLESTAGE2 || Save.stageState == Save.StageState.SIMPLESTAGE3)
            if (Save.rank > Save.HighSimpleRank)
                Save.HighSimpleRank = Save.rank;
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
        AddUkemiTextPoint.text = Save.AddUkemiPoint_.ToString();


        if (MaxPoint > 12 && !isOut)
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
		Save.Rank point_ = Save.Rank.FIRST;

		try
		{
			point_ = (Save.Rank)point;
		}
		catch
		{
			point_ = Save.Rank.MASTER;
		}

        switch (point_)
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

        Sound.PlaySe("itawari01");

		Save.Rank point_ = Save.Rank.FIRST;

		try
		{
			point_ = (Save.Rank)point;
		}
		catch
		{
			point_ = Save.Rank.MASTER;
		}

		switch (point_)
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



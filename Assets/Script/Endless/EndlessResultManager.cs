using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessResultManager : MonoBehaviour
{
    [SerializeField]
    private Text ResultText;
    [SerializeField]
    private Text AddUkemiText;
    [SerializeField]
    private Text AddUkemiTextPoint;
    [SerializeField]
    private Text UkemiComboText;
    [SerializeField]
    private Text UkemiComboTextPoint;
    [SerializeField]
    private Text FallDistanceText;
    [SerializeField]
    private Text FallDistanceTextPoint;
    [SerializeField]
    private Text ScoreText;

    [SerializeField]
    private List<Text> ResultWaitText;

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

        //落下距離もスコアにくわえる
        Save.UkemiScore += (int)(Save.distance) * 10;


        MaxPoint = Save.UkemiScore;

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
                    StartCoroutine(Attack());
                }
            }
        }
    }

    void AlphaSet()
    {
        ResultText.color = new Color(1, 1, 1, 0);
        AddUkemiText.color = new Color(1, 1, 1, 0);
        AddUkemiTextPoint.color = new Color(1, 1, 1, 0);
        UkemiComboText.color = new Color(1, 1, 1, 0);
        UkemiComboTextPoint.color = new Color(1, 1, 1, 0);
        //ResultWaitText.color = new Color(1, 1, 1, 0);
    }

    void ActiveSet()
    {
        ResultText.enabled = false;
        AddUkemiText.enabled = false;
        AddUkemiTextPoint.enabled = false;
        UkemiComboText.enabled = false;
        UkemiComboTextPoint.enabled = false;
        FallDistanceText.enabled = false;
        FallDistanceTextPoint.enabled = false;

        foreach (Text t in ResultWaitText)
        {
            t.enabled = false;
        }
       
        //AfterRank.SetActive(false);
    }

    IEnumerator TextView()
    {
        ResultText.enabled = true;
        Sound.PlaySe("taiko02");
        yield return new WaitForSeconds(1);

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

        UkemiComboText.enabled = true;
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1);

        if (tapCount > 0)
            yield break;

        UkemiComboTextPoint.enabled = true;
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1f);


        FallDistanceText.enabled = true;
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1);

        if (tapCount > 0)
            yield break;

        FallDistanceTextPoint.enabled = true;
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1.25f);

        if (tapCount > 0)
            yield break;

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


        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {

        HighScoreCheck();

        Save.FlagReSet();

        ani.enabled = true;

        yield return new WaitForSeconds(0.5f);

        Sound.PlaySe("beshi");
        yield return new WaitForSeconds(0.6f);



    }

    void Skip1()
    {
        ResultText.enabled = true;
        AddUkemiText.enabled = true;
        AddUkemiTextPoint.enabled = true;
        UkemiComboText.enabled = true;
        UkemiComboTextPoint.enabled = true;
        FallDistanceText.enabled = true;
        FallDistanceTextPoint.enabled = true;

        foreach (Text t in ResultWaitText)
        {
            t.enabled = true;
        }
        Sound.PlaySe("taiko01");

    }

    void HighScoreCheck()
    {

        if (Save.UkemiScore > Save.UkemiHighScore)
            Save.UkemiHighScore = Save.UkemiScore;
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

        UkemiComboTextPoint.text = Save.addUkemiMaxCombo.ToString();
        AddUkemiTextPoint.text = Save.addUkemiCounter.ToString();
        FallDistanceTextPoint.text = Save.distance.ToString();
        ScoreText.text = Save.UkemiScore.ToString();

        if (MaxPoint > 30000 && !isOut)
        {
            rank = Rank.PARFECT;
            Hanko.sprite = Kiwami;

        }
        else if (MaxPoint > 5000 && !isOut)
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

    }

}

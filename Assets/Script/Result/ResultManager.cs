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
    private GameObject AfterRank;
    [SerializeField]
    private Text BeforRankText;
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

    private enum Rank
    {
        BAD,
        GOOD,
        PARFECT
    }

    private Rank rank;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Save.AddUkemiPoint = 5;
        //Save.UkemiPoint = 8;
        ani.enabled = false;
        //AlphaSet();
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

        switch (rank)
        {
            case Rank.PARFECT:
            case Rank.GOOD:
                StartCoroutine(Crash());
                break;
            case Rank.BAD:
                StartCoroutine(Bad());
                break;
        }
    }

    IEnumerator Crash()
    {
        ani.enabled = true;
        ani.SetBool("Result",true);
        yield return new WaitForEndOfFrame();
        ani.SetBool("Result", false);

    }

    IEnumerator Bad()
    {
        ani.enabled = true;
        ani.SetBool("Result2", true);
        yield return new WaitForEndOfFrame();
        ani.SetBool("Result2", false);

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

        if (Save.UkemiPoint + Save.AddUkemiPoint > 3 && !isOut)
        {
            rank = Rank.PARFECT;
            Hanko.sprite = Kiwami;
        }
        else if (Save.UkemiPoint + Save.AddUkemiPoint > 0 && !isOut)
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



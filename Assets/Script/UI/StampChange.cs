using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StampChange : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite Parfect;
    [SerializeField]
    private Sprite Good;
    [SerializeField]
    private Sprite Bad;

    public enum Stamp
    {
        NONE = 0,
        BAD = 1,
        GOOD = 2,
        PARFECT = 3
    }

    /// <summary>
    /// これをFalseにすると強制的に消える
    /// </summary>
    public bool isChanging;

    // Start is called before the first frame update
    void Start()
    {
        image.enabled = false;
    }

    public IEnumerator StampChangeView(Stamp stamp)
    {
        if (isChanging)
            isChanging = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        isChanging = true;

        switch (stamp)
        {
            case Stamp.BAD:
                image.sprite = Bad;
                break;
            case Stamp.GOOD:
                image.sprite = Good;
                break;
            case Stamp.PARFECT:
                image.sprite = Parfect;
                break;
        }

        StartCoroutine(ColorFader());

    }

    IEnumerator ColorFader()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        image.enabled = true;
        for(int i = 0; i <= 15; i++)
        {
            if (!isChanging)
            {
                image.enabled = false;
                yield break;
            }
            image.color += new Color(0, 0, 0, 1.0f/15.0f);
            yield return new WaitForSeconds((1.0f / (60.0f / Time.timeScale)));
        }

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i <= 15; i++)
        {
            if (!isChanging)
            {
                image.enabled = false;
                yield break;
            }
            image.color -= new Color(0, 0, 0, 1.0f / 15.0f);
            yield return new WaitForSeconds((1.0f / (60.0f / Time.timeScale)));
        }

        image.enabled = false;
        isChanging = false;

    }
}

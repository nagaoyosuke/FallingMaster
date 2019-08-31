using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject Back;
    public GameObject[] MenuBar;

    public ScreenFader fader;

    [SerializeField]
    private bool isMenu;
    [SerializeField]
    private bool isBarTap;

    private float timeScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MenuTap()
    {
        if (isBarTap)
            return;

        Sound.PlaySe("taiko02");

        if (!isMenu)
        {
            isMenu = true;
            MenuChange(true);
            timeScale = Time.timeScale; 
            Time.timeScale = 0;
        }
        else
        {
            isMenu = false;
            MenuChange(false);
            Time.timeScale = timeScale;

        }
    }

    void MenuChange(bool value)
    {
        Back.SetActive(value);
        foreach(GameObject g in MenuBar)
        {
            g.SetActive(value);
        }
    }

    public void TitleTap()
    {
        if (isBarTap)
            return;
        isBarTap = true;
        Sound.PlaySe("taiko01");
        StartCoroutine(Title());
    }

    IEnumerator Title()
    {
        yield return StartCoroutine(FadeOut());
        Time.timeScale = 1.0f;
        Save.ReSet();
        Save.PointReset();
        MySceneManager.GoTitle();
    }

    public void ResultTap()
    {
        if (isBarTap)
            return;
        isBarTap = true;
        Sound.PlaySe("taiko01");
        StartCoroutine(Result());

    }

    IEnumerator Result()
    {
        yield return StartCoroutine(FadeOut());
        Time.timeScale = 1.0f;
        Save.ReSet();
        MySceneManager.GoResult();
    }

    public void BackGameTap()
    {
        if (isBarTap)
            return;

        isMenu = false;
        MenuChange(false);
        Sound.PlaySe("taiko01");
        Time.timeScale = timeScale;
    }

    IEnumerator FadeOut()
    {
        fader.isFadeOut = true;
        Sound.PlaySe("sceneswitch02");
        yield return new WaitUntil(() => fader.isFadeOut == false);
        Sound.StopBgm();
    }
}

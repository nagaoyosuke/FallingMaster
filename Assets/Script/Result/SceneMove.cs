using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMove : MonoBehaviour
{
    [SerializeField]
    private ScreenFader fader;

    private bool isPush;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoMenu()
    {
        if (isPush)
            return;
        isPush = true;
        fader.isFadeOut = true;
        StartCoroutine(_GoMenu());
    }

    public void GoMain()
    {
        if (isPush)
            return;
        isPush = true;
        fader.isFadeOut = true;
        StartCoroutine(_GoMain());
    }

    IEnumerator _GoMain()
    {
        yield return new WaitForSeconds(1.5f);
        MySceneManager.GoMain();
    }

    IEnumerator _GoMenu()
    {
        yield return new WaitForSeconds(1.5f);
        MySceneManager.GoTitle();
    }
}

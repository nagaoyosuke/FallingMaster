using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面タップを管理するクラス(02/02豊田)
/// </summary>

public class GetTap : MonoBehaviour
{
    private bool isTapped = false;


    // Use this for initialization
    void Start()
    {
        Sound.PlaySe("taiko02");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTapped)
        {
            Sound.PlaySe("osu01");
            isTapped = true;
            StartCoroutine(WaitingFadeOut());

        }
    }

    private IEnumerator WaitingFadeOut (){

        yield return new WaitForSeconds(0.75f);
        GameObject.Find("Panel").GetComponent<ScreenFader>().isFadeOut = true;
        yield return new WaitForSeconds(1.5f);

        MySceneManager.GoMain();
    }
}

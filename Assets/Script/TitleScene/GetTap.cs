using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTap : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(WaitingFadeOut());

        }
    }

    private IEnumerator WaitingFadeOut (){

        GameObject.Find("Panel").GetComponent<ScreenFader>().isFadeOut = true;
        yield return new WaitForSeconds(1.5f);

        MySceneManager.GoMain();
    }
}

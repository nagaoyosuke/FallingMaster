using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gomain(){
        panel.GetComponent<ScreenFader>().isFadeOut = true;
        StartCoroutine(wait());
    }


    IEnumerator wait(){
        Sound.PlaySe("osu01");

        yield return new WaitForSeconds(1.5f);
        Sound.StopBgm();

        MySceneManager.GoMain();


    }
}

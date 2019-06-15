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
        yield return new WaitForSeconds(1.5f);
        MySceneManager.GoMain();


    }
}

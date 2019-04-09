using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{


    private bool isFinishedAnimation = false;
    public Image grainImage;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //if(tapしたらisfinishedanimationtrue)

        if(isFinishedAnimation){
            StartTutorial();

        }
        
    }

    void StartTutorial(){
        isFinishedAnimation = false;

    }

}

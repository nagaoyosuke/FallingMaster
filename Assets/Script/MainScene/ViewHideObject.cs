using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewHideObject : MonoBehaviour
{
    private bool isHide;
    [SerializeField]
    private GameObject[] objects;

    [SerializeField]
    private Save.MainGameFlag ViewFlag = Save.MainGameFlag.THROW;

    [SerializeField]
    private Save.MainGameFlag HideFlag = Save.MainGameFlag.STARTWAIT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHide)
        {
            if (Save.maingameFlag == HideFlag)
            {
                isHide = true;
                Chenge(false);
            }
        }
        if (isHide)
        {
            if(Save.maingameFlag == ViewFlag)
            {
                isHide = false;
                Chenge(true);
            }
        }
    }

    void Chenge(bool value)
    {
        foreach(GameObject obj in objects)
        {
            obj.SetActive(value);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindManager : MonoBehaviour
{
    [SerializeField]
    private float windZ1 = 0;
    [SerializeField]
    private float windZ2 = 5;
    [SerializeField]
    private float windZ3 = -5;

    private Image image;

    [SerializeField]
    private Sprite StrongSp;
    [SerializeField]
    private Sprite EasySp;
    [SerializeField]
    private Sprite NoneSp;

    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.FindWithTag("Wind").GetComponent<Image>();
        //image.gameObject.SetActive(false);
        WindChenge();
    }

    void WindChenge()
    {
        float windZ = 0;
        Save.windZ = 0;
        int ran = Random.Range(0, 3);
        switch (ran)
        {
            case 0:
                windZ = windZ1;
                image.sprite = NoneSp;
                break;
            case 1:
                windZ = windZ2;
                image.sprite = EasySp;

                break;
            case 2:
                windZ = windZ3;
                image.sprite = StrongSp;

                break;
        }

        Save.windZ = windZ;
    }
}

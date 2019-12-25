using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessUkemiObjMaker : MonoBehaviour
{
    [SerializeField]
    private ButtonEnableManager ButtonMg;

    /// 受身オブシェクトたち
    [SerializeField]
    private GameObject Container;
    [SerializeField]
    private GameObject Bard;
    [SerializeField]
    private GameObject UFO;

    void Awake()
    {
        Save.stageState = Save.StageState.ENDLESS;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMove : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> initObj = new List<GameObject>();
    public List<GameObject> obj = new List<GameObject>();

    [SerializeField]
    private Transform _Camera;
    [SerializeField]
    private Transform _Player;

    [SerializeField]
    private Vector3 CmReStartPos;
    [SerializeField]
    private Vector3 PlReStartPos;

    private bool isHide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Save.ukemiCounter == 8 && !isHide)
            ThrowHide();
    }  

    void ThrowHide()
    {
        isHide = true;

        foreach(GameObject g in initObj)
        {
            g.SetActive(false);
        }
    }

    /// <summary>
    /// 無限ループさせるために座標をリセット
    /// EndlessUkemiObjMakerで呼ばれている
    /// </summary>
    public void MovePlCm()
    {
        print("もゔぇもゔぇ");
        //_Camera.position = CmReStartPos;
        //368Y座標あがる
        _Player.position += new Vector3(0, 360, 0);
        obj[obj.Count - 1].transform.position = PlReStartPos;
        ObjDestory();
    }

    void ObjDestory()
    {
        for(int i = 0; i < obj.Count - 1; i++)
        {
            var o = obj[i];
            Destroy(o);
        }
    }
}

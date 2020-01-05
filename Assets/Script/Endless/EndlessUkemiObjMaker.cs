using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessUkemiObjMaker : MonoBehaviour
{
    [SerializeField]
    private EndlessMove em;

    [SerializeField]
    private CameraManager CameraMove;
    /// <summary>
    /// 受け身するオブシェクト
    /// </summary>
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Rigidbody PlayerRb;

    [SerializeField]
    private BoxCollider PlayerBox;

    [SerializeField]
    private Animator PlayerAni;

    [SerializeField]
    private GameObject UkemiStartText;

    [SerializeField]
    private ButtonEnableManager ButtonMg;

    /// 受身オブシェクトたち
    [SerializeField]
    private GameObject[] ukemiObjs;

    /// 背景のオブシェクトたち
    [SerializeField]
    private GameObject[] NoukemiObjs;



    private float leftX = -32.0f;
    private float rightX = -5.0f;
    private float moveY = -30.0f;

    private bool isMaking;

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
        var count = Save.addUkemiCounter;

        if (count > 0)
        {
            if (count % 6 == 0 && !isMaking)
            {
                isMaking = true;
                UkemiObjMake(count);
            }

            if (count % 6 == 1 && isMaking)
            {
                isMaking = false;
            }
        }
    }

    void UkemiObjMake(int count)
    {
        if (count % 12 == 0)
            em.MovePlCm();

        var pos = Vector3.zero;

        var _moveY = (count % 12 + 1) * moveY - 10;
        var rd = count / 6;

        for (int i = 0; i < 6; i++)
        {
            NoUkemiObjeMake(_moveY, i);

            var obj = Instantiate(GetObj(ukemiObjs));

            pos.y = _moveY + moveY * i;
            pos.x = leftX;
            obj.transform.localEulerAngles = new Vector3(0, 180, 0);

            if (i % 2 == 1)
            {
                pos.x = rightX;

                var p = obj.GetComponentInChildren<AddUkemiPlane>();
                p.Power.x *= -1;
                obj.transform.localEulerAngles = new Vector3(0, 0, 0);

            }

            obj.transform.position = pos;

            var g = obj.GetComponentInChildren<AddUkemiGeneral>();
            g.ParameterReSet(CameraMove,Player,PlayerRb,PlayerBox,PlayerAni);

            var c = obj.GetComponentInChildren<AddUkemiCheck>();
            c.isInversion = true;
            c.UkemiStartText = UkemiStartText;
            c.FrameChange(c.parfectFlame + Random.Range(-3 - rd, 4 + rd), c.parfectingFlame + Random.Range(-1 - rd, 2 - rd), c.goodingFlame - rd);

            em.obj.Add(obj);


        }


    }


    /// <summary>
    /// 背景のオブシェクト生成
    /// </summary>
    void NoUkemiObjeMake(float _moveY,int i)
    {
        if (Random.Range(0, 2) == 0)
            return;

        var obj = Instantiate(GetObj(NoukemiObjs));
        var pos = Vector3.zero;

        pos.y = _moveY + moveY * i;
        pos.y += 20;

        pos.x = leftX + 20;
        pos.z = -20;

        var rx = Random.Range(-10.0f, 10.0f);
        var ry = Random.Range(-10.0f, 10.0f);
        var rz = Random.Range(-10.0f, 10.0f);

        obj.transform.localEulerAngles = new Vector3(rx, 180 + ry, rz);

        if (i % 2 == 1)
        {
            pos.x = rightX - 10;
            pos.z = 10;

            obj.transform.localEulerAngles = new Vector3(rx, ry, rz);

        }

        obj.transform.position = pos;

        em.obj.Add(obj);
    }

    GameObject GetObj(GameObject[] objs)
    {
        return objs[Random.Range(0, objs.Length)];
    }
}

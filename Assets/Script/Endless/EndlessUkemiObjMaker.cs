using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessUkemiObjMaker : MonoBehaviour
{
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
    private ButtonEnableManager ButtonMg;

    /// 受身オブシェクトたち
    [SerializeField]
    private GameObject Container;
    [SerializeField]
    private GameObject Bard;
    [SerializeField]
    private GameObject UFO;

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
        var count = Save.ukemiCounter - 1;
        if (count > 0)
        {
            if (count % 5 == 0 && !isMaking)
            {
                isMaking = true;
                UkemiObjeMake(count);
            }

            if (count % 5 == 1 && isMaking)
            {
                isMaking = false;
            }
        }
    }

    void UkemiObjeMake(int count)
    {
        var baseobj = UFO;
        var pos = Vector3.zero;

        var _moveY = (count + 1) * moveY - 10;

        for (int i = 0; i < 5; i++)
        {
            var obj = Instantiate(baseobj);

            pos.y = _moveY + moveY * i;
            pos.x = leftX;

            if (i % 2 == 0)
            {
                pos.x = rightX;

                var p = obj.GetComponentInChildren<AddUkemiPlane>();
                p.Power.x *= -1;
            }

            obj.transform.position = pos;

            var g = obj.GetComponentInChildren<AddUkemiGeneral>();
            g.ParameterReSet(CameraMove,Player,PlayerRb,PlayerBox,PlayerAni);

            var c = obj.GetComponentInChildren<AddUkemiCheck>();
            c.isInversion = true;

        }
    }
}

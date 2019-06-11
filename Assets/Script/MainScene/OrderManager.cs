using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 投げられるのが２回目以降に動くスクリプトの管理(06/12 長尾)
/// インスペクターからResetすると自動で参照してくれるから最後に押すと便利
/// </summary>
[RequireComponent(typeof(MonoBehaviour))]
public class OrderManager : MonoBehaviour
{
    /// <summary>
    /// 投げられる順番
    /// </summary>
    [SerializeField]
    private int number;

    private bool isSwitch;

    [SerializeField]
    private List<MonoBehaviour> mono = new List<MonoBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        if (number != Save.ukemiCounter)
            MonoSwitch(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSwitch)
        {
            if(number == Save.ukemiCounter)
            {
                MonoSwitch(true);
                isSwitch = true;
            }
        }
    }

    void MonoSwitch(bool value)
    {
        foreach (MonoBehaviour m in mono)
        {
            m.enabled = value;
        }
    }

    void Reset()
    {
        mono.Add(GetComponentInChildren<PlaneHit>());
        mono.Add(GetComponentInChildren<SlowManager>());
        mono.Add(GetComponentInChildren<UkemiButton>());
        mono.Add(GetComponentInChildren<UkemiEffect>());
        mono.Add(GetComponentInChildren<EventManager>());
        mono.Add(GetComponentInChildren<ThrowManager>());
        mono.Add(GetComponentInChildren<IdlingAnimetion>());
        //CameraManagerをインスペクターから入れる
        mono.Add(null);

    }
}

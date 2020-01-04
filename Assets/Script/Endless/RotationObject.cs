using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField]
    private Transform Trs;

    /// <summary>
    /// 一周するまでにかけるフレーム
    /// </summary>
    [SerializeField]
    private int flame;

    public enum XYZ
    {
        X,
        Y,
        Z
    }

    [SerializeField]
    private XYZ xyz;

    // Start is called before the first frame update
    void Start()
    {
        if (Trs == null)
            return;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float v = 360.0f / flame;
        var baseVec = GetVector(v);
        var vec = baseVec;

        while (true)
        {
            for (int i = 0; i < flame; i++)
            {
                Trs.localEulerAngles += vec;
                yield return new WaitForFixedUpdate();
            }

            Trs.localEulerAngles += GetVector(-360.0f);
            vec = baseVec;
        }
    }

    Vector3 GetVector(float v)
    {
        Vector3 value = Vector3.zero;

        switch (xyz)
        {
            case XYZ.X:
                value.x = v;
                break;
            case XYZ.Y:
                value.y = v;
                break;
            case XYZ.Z:
                value.z = v;
                break;
        }

        return value;
    }
}

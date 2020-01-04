using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2019/??/?? toyoda
/// ミノルを見る。
/// スマホパシャパシャに使っている。
/// </summary>
public class SeeMinoru : MonoBehaviour
{
    private GameObject minoru;

    private void Start()
    {
        minoru = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        this.transform.LookAt(minoru.transform);
    }
}

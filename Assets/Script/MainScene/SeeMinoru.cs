using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

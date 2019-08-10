﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUkemiPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && enabled)
        {
            if (!Save.isAddUkemi && Save.maingameFlag == Save.MainGameFlag.ADDUKEMI)
            {
                //other.gameObject.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
                //other.gameObject.GetComponentInParent<Rigidbody>().useGravity = false;
                //other.gameObject.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                //other.transform.rotation = new Quaternion(0, 0, 0, 0);
                Save.addUkemiRank = Save.AddUkemi.NOUKEMI;
            }

            //other.gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
            //other.gameObject.GetComponentInParent<Rigidbody>().useGravity = false;
            //other.gameObject.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;

        }
    }

    private void OnDisable()
    {
        GetComponent<BoxCollider>().enabled = false;

    }
}
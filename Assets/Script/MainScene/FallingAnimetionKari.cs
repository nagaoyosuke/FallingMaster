using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAnimetionKari : MonoBehaviour
{

    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.x < -0.5f)
        {
            transform.rotation = new Quaternion(90.0f, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            rb.freezeRotation = true;
        }
        print(transform.localRotation.x);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHit : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    private bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isHit)
        {
            if (other.tag == "Player")
            {
                isHit = false;

                var par = Instantiate(particle) as GameObject;
                par.transform.position = other.transform.position;
                par.transform.position += new Vector3(0, 2, 0);
                Time.timeScale = 1.0f;
            }
        }
    }
}

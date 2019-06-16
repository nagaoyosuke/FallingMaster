using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHit : MonoBehaviour
{
    [SerializeField]
    private GameObject Splash;

    [SerializeField]
    private GameObject Bubble;

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
                StartCoroutine(Effect(other.transform.position));

            }
        }
    }

    IEnumerator Effect(Vector3 pos)
    {
        var spl = Instantiate(Splash) as GameObject;
        spl.transform.position = pos;
        spl.transform.position += new Vector3(0, 2, 0);
        Time.timeScale = 1.0f;

        yield return new WaitForSeconds(1.0f);
        var bub = Instantiate(Bubble) as GameObject;
        bub.transform.position = pos;
        bub.transform.position += new Vector3(0, 2, 0);

    }
}

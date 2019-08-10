using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WindChenge();
    }

    void WindChenge()
    {
        float windZ = 0;
        Save.windZ = 0;
        int ran = Random.Range(0, 3);
        switch (ran)
        {
            case 0:
                windZ = 0;
                break;
            case 1:
                windZ = 5;
                break;
            case 2:
                windZ = -5;
                break;
        }

        Save.windZ = windZ;
    }
}

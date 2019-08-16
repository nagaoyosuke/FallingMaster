using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    [SerializeField]
    private float windZ1 = 0;
    [SerializeField]
    private float windZ2 = 5;
    [SerializeField]
    private float windZ3 = -5;
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
                windZ = windZ1;
                break;
            case 1:
                windZ = windZ2;
                break;
            case 2:
                windZ = windZ3;
                break;
        }

        Save.windZ = windZ;
    }
}

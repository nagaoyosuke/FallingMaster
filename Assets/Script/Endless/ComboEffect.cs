using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboEffect : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerTrs;
    [SerializeField]
    private GameObject Hanabi;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator HanabiMake(int number)
    {
        if (number > 5)
            number = 5;

        switch (number)
        {
            case 1:
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, 0), "Hanabi", 2.0f);
                break;

            case 2:
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, -10), "Hanabi", 2.0f);
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, 10), "Hanabi", 2.0f);
                break;
            case 3:
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, 0), "Hanabi", 2.0f);
                yield return new WaitForSeconds(0.1f);
                ObjCreate(Hanabi, new Vector3(-30, -15, -20), "Hanabi", 2.0f);
                yield return new WaitForSeconds(0.1f);
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, 20), "Hanabi", 2.0f);
                break;
            case 4:
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-45, -15, -10), "Hanabi", 2.0f);
                yield return new WaitForSeconds(0.1f);
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, -10), "Hanabi", 2.0f);
                yield return new WaitForSeconds(0.1f);
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-45, -15, 10), "Hanabi", 2.0f);
                yield return new WaitForSeconds(0.1f);
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, 10), "Hanabi", 2.0f);
                break;
            case 5:
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, -20), "Hanabi", 2.0f);
                yield return new WaitForSeconds(0.1f);
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-45, -15, -10), "Hanabi", 2.0f);
                yield return new WaitForSeconds(0.1f);
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-45, -15, 10), "Hanabi", 2.0f);
                yield return new WaitForSeconds(0.1f);
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, 20), "Hanabi", 2.0f);
                yield return new WaitForSeconds(0.1f);
                ObjCreate(Hanabi, PlayerTrs.localPosition + new Vector3(-30, -15, 0), "Hanabi", 2.0f);
                break;
        }

        yield return null;

    }

    void ObjCreate(GameObject pre, Vector3 pos, string se, float time)
    {
        var g = Instantiate(pre);
        g.transform.position = pos;
        Sound.PlaySe(se, 0.5f, 1, 0);
        Destroy(g, time);
    }
}

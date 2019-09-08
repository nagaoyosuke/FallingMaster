using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHit : MonoBehaviour
{
    [SerializeField]
    private GameObject Splash;

    [SerializeField]
    private GameObject Bubble;

    [SerializeField]
    private UkemiEffect effect_;

    private IUkemiEffect effect;

    private bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        effect = effect_.GetComponent<IUkemiEffect>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isHit)
        {
            if (other.tag == "Player")
            {
                if (Save.ukemiRank != Save.UkemiRank.NONE)
                    return;
                isHit = false;
                StartCoroutine(Effect(other.gameObject));
            }
        }
    }

    IEnumerator Effect(GameObject obj)
    {
        var pos = obj.transform.position;
        var spl = Instantiate(Splash) as GameObject;
        spl.transform.position = pos;
        spl.transform.position += new Vector3(0, 2, 0);
        Time.timeScale = 1.0f;
        Sound.PlaySe("waterdive");
        Save.ukemiRank = Save.UkemiRank.BAD;
        effect.BadEffect();
        var bub = Instantiate(Bubble) as GameObject;
        bub.transform.position = pos;
        bub.transform.position += new Vector3(0, 2, 0);

        yield return new WaitForSeconds(0.8f);

        obj.GetComponentInParent<Rigidbody>().useGravity = false;
        obj.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        //Save.ukemiRank = Save.UkemiRank.BAD;
        //effect.BadEffect();

        yield return new WaitForSeconds(1.0f);

        Save.maingameFlag = Save.MainGameFlag.RESULT;


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{
    [SerializeField]
    private Text ComboNumber;

    [SerializeField]
    private Text ComboCrearNumber;

    [SerializeField]
    private GameObject Bounas;

    [SerializeField]
    private Text BounsNumbner;

    [SerializeField]
    private ComboEffect comboEffect;

    [SerializeField]
    private ScoreCount scoreCount;

    public void TextChange()
    {
        var combo = Save.addUkemiCombo;
        MaxComboCheck(combo);
        ComboNumber.text = combo.ToString();

        if (combo % 5 == 0)
        {
            if (combo == 0)
                return;

            if ((Save.addUkemiCounter % 12) != 0)
            {
                StartCoroutine(comboEffect.HanabiMake(combo / 5));
            }

            Bounas.SetActive(true);

            ComboCrearNumber.text = combo.ToString();

            BounsNumbner.text = (combo * 20).ToString() + "点";
            Save.UkemiScore += combo * 20;
            scoreCount.TextChange();

            StartCoroutine(DelayClass.DelayCoroutin(90, () =>
            {
                Bounas.SetActive(false);
            }));

        }
    }

    void MaxComboCheck(int combo)
    {
        if (combo > Save.addUkemiMaxCombo)
        {
            Save.addUkemiMaxCombo = combo;
            ComboNumber.color = Color.red;
        }
        else
        {
            ComboNumber.color = Color.white;
        }
    }
}

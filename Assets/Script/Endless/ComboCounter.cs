using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{
    [SerializeField]
    private Text ComboNumber;

    public void TextChange()
    {
        MaxComboCheck();

        ComboNumber.text = Save.addUkemiCombo.ToString();
    }

    void MaxComboCheck()
    {
        if (Save.addUkemiCombo > Save.addUkemiMaxCombo)
        {
            Save.addUkemiMaxCombo = Save.addUkemiCombo;
            ComboNumber.color = Color.red;
        }
        else
        {
            ComboNumber.color = Color.white;
        }
    }
}

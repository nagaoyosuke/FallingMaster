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
        ComboNumber.text = Save.addUkemiCombo.ToString();
        MaxComboCheck();
    }

    void MaxComboCheck()
    {
        if (Save.addUkemiCombo > Save.addUkemiMaxCombo)
            Save.addUkemiMaxCombo = Save.addUkemiCombo;
    }
}

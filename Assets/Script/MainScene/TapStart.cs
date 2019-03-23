using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapStart : MonoBehaviour
{
    public void Tap(){
        Save.maingameFlag = Save.MainGameFlag.THROW;
    }

}

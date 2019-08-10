using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFind : MonoBehaviour
{
    [SerializeField]
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(GameObject.FindWithTag("MainCamera").GetComponent<IMainCameraMove>().ThrowAngleCameraMove);
    }

}

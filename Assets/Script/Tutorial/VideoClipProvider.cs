using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 2020/01/03 toyoda
/// チュートリアルで使用するmp4はここに保管している。
/// Tutorial Managerにのっける予定
/// </summary>
public class VideoClipProvider : MonoBehaviour
{

    [SerializeField] private VideoClip _throwTutorialClip;
    [SerializeField] private VideoClip _windTutorialClip;
    [SerializeField] private VideoClip _ukemiTutorialClip;

    public VideoClip throwTutorialClip => _throwTutorialClip;
    public VideoClip windTutorialClip => _windTutorialClip;
    public VideoClip ukemiTutorialClip => _ukemiTutorialClip;

}

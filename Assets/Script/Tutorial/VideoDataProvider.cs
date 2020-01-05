using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 2020/01/03 toyoda
/// チュートリアルで使用する動画関連はここに保管している。
/// Tutorial Managerにのっける予定
/// </summary>
public class VideoDataProvider : MonoBehaviour
{

    [SerializeField] private VideoClip _throwTutorialClip;
    [SerializeField] private VideoClip _windTutorialClip;
    [SerializeField] private VideoClip _ukemiTutorialClip;

    public VideoClip throwTutorialClip => _throwTutorialClip;
    public VideoClip windTutorialClip => _windTutorialClip;
    public VideoClip ukemiTutorialClip => _ukemiTutorialClip;

    [SerializeField] private VideoPlayer _throwTutorialPlayer;
    [SerializeField] private VideoPlayer _windTutorialPlayer;
    [SerializeField] private VideoPlayer _ukemiTutorialPlayer;

    public VideoPlayer throwTutorialPlayer => _throwTutorialPlayer;
    public VideoPlayer windTutorialPlayer => _windTutorialPlayer;
    public VideoPlayer ukemiTutorialPlayer => _ukemiTutorialPlayer;

}

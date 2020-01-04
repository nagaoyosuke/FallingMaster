using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 2020/01/03 toyoda
/// チュートリアルのウィンドウを操作する。
/// </summary>
public class TutorialController : MonoBehaviour
{

    [SerializeField] private Canvas tutorialCanvas;
    [SerializeField] private VideoPlayer videoPlayer;

    public void PlayVideoClip(VideoClip video)
    {
        tutorialCanvas.enabled = true;
        videoPlayer.clip = video;
        videoPlayer.Play();
    }

    public void CloseVideoPlayerWindow()
    {
        videoPlayer.Stop();
        videoPlayer.clip = null;
        tutorialCanvas.enabled = false;
    }
}

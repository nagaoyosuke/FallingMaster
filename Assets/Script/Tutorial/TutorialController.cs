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

    public void PlayVideoClip(VideoPlayer videoPlayer)
    {
        Sound.PlaySe("taiko02");
        tutorialCanvas.enabled = true;

        videoPlayer.gameObject.SetActive(true);
        videoPlayer.isLooping = true;
        videoPlayer.Play();

        Time.timeScale = 0.0f;
    }

    public void CloseVideoPlayerWindow()
    {
        var videoPlayer = GameObject.FindGameObjectWithTag("VideoPlayer").GetComponent<VideoPlayer>();

        Sound.PlaySe("taiko01");

        videoPlayer.Stop();
        videoPlayer.clip = null;
        videoPlayer.gameObject.SetActive(false);

        tutorialCanvas.enabled = false;
        Time.timeScale = 1.0f;
    }
}

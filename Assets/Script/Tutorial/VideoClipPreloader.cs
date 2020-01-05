using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


/// <summary>
/// 動画を事前に読み込む。
/// </summary>
public class VideoClipPreloader : MonoBehaviour
{
    [SerializeField] private VideoDataProvider provider;

    private void Start()
    {
        Preload(provider.throwTutorialPlayer, provider.throwTutorialClip);
        Preload(provider.windTutorialPlayer, provider.windTutorialClip);
        Preload(provider.ukemiTutorialPlayer, provider.ukemiTutorialClip);
    }


    public void Preload(VideoPlayer videoPlayer, VideoClip clip)
    {
        videoPlayer.clip = clip;
        videoPlayer.Prepare();
    }
}

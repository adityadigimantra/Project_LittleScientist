using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class videoloader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    IEnumerator Start()
    {
        string assetBundlePath = Application.streamingAssetsPath + "/videobundle";
        var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);
        yield return assetBundleCreateRequest;

        var assetBundle = assetBundleCreateRequest.assetBundle;
        var videoClipRequest = assetBundle.LoadAssetAsync<VideoClip>("Video");
        yield return videoClipRequest;

        var videoClip = videoClipRequest.asset as VideoClip;
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
    }

}

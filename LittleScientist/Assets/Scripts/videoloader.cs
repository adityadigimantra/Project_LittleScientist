using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class videoloader : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    IEnumerator Start()
    {
        rawImage = GetComponent<RawImage>();
        string assetBundlePath = Application.streamingAssetsPath + "/videobundle";
        var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);
        yield return assetBundleCreateRequest;

        var assetBundle = assetBundleCreateRequest.assetBundle;
        var videoClipRequest = assetBundle.LoadAssetAsync<VideoClip>("Video");
        yield return videoClipRequest;

        var videoClip = videoClipRequest.asset as VideoClip;
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
        videoPlayer.clip = videoClip;
        videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        rawImage.texture = videoPlayer.targetTexture;
        videoPlayer.Play();
    }

}

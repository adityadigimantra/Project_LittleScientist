using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class videoPlayer : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer vp;
    void Start()
    {
       
        vp = gameObject.AddComponent<VideoPlayer>();
        rawImage = GetComponent<RawImage>();

        vp.playOnAwake = false;
        vp.source = VideoSource.Url;
        vp.url = "https://little-scientist.s3.ap-south-1.amazonaws.com/MyVideo.mp4";
        vp.renderMode = VideoRenderMode.APIOnly;
        vp.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        rawImage.texture = vp.targetTexture;
        vp.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

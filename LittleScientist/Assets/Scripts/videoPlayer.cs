using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class videoPlayer : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer vp;
    void Start()
    {
        vp = gameObject.AddComponent<VideoPlayer>();
        rawImage = GetComponent<RawImage>();

        vp.playOnAwake = false;
        vp.source = VideoSource.VideoClip;
        vp.clip =Resources.Load<VideoClip>("/Resources/Video");
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

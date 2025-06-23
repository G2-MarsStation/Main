using UnityEngine;
using UnityEngine.Video;

public class VideoAutoStart : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }
}

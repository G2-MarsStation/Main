using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // Necessário para trocar de cena

public class VideoAutoStart : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject[] objectsToHide;
    public AudioSource audioToPauseAndResume;
    public string nextSceneName; // Nome da próxima cena

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            videoPlayer.Play();

            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null)
                    obj.SetActive(false);
            }

            if (audioToPauseAndResume != null && audioToPauseAndResume.isPlaying)
            {
                audioToPauseAndResume.Pause();
            }
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        if (audioToPauseAndResume != null)
        {
            audioToPauseAndResume.Play();
        }

        // Trocar de cena
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnDisable()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached -= OnVideoFinished;
    }
}

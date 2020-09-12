using UnityEngine;
using UnityEngine.UI;

public class soundController : MonoBehaviour
{
    [Header("SOUND BUTTON UI")]
    public Image soundButton;
    public Image soundIcon;

    [Header("Audio Sources")]
    public AudioSource[] audioSources;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SOUND"))
        {
            PlayerPrefs.SetInt("SOUND", 1);
        }

        if(PlayerPrefs.GetInt("SOUND") == 1)
        {
            SoundOn();
        }
        else
        {
            SoundOff();
        }
    }

    public void SoundButton()
    {
        if(PlayerPrefs.GetInt("SOUND") == 1)
        {
            SoundOff();
            PlayerPrefs.SetInt("SOUND", 0);
        }
        else
        {
            SoundOn();
            PlayerPrefs.SetInt("SOUND", 1);
        }
    }

    private void SoundOn()
    {
        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].mute = false;
        }

        soundButton.color = Color.white;
        soundIcon.color = Color.black;
    }

    private void SoundOff()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].mute = true;
        }

        soundButton.color = Color.black;
        soundIcon.color = Color.white;
    }
}

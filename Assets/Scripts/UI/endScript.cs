using UnityEngine;
using UnityEngine.SceneManagement;

public class endScript : MonoBehaviour
{
    public void RestartFromBegining()
    {
        PlayerPrefs.SetInt("CURRENT_LEVEL", 1);
        SceneManager.LoadScene(1);
    }
}

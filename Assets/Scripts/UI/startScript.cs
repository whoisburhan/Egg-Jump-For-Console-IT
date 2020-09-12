using UnityEngine;
using UnityEngine.SceneManagement;

public class startScript : MonoBehaviour
{
    private void Start()
    {
        //   PlayerPrefs.DeleteAll();

        Time.timeScale = 1f;

        if (!PlayerPrefs.HasKey("MAX_LEVEL"))
        {
            PlayerPrefs.SetInt("MAX_LEVEL", SceneManager.sceneCount-1);
        }

        if(SceneManager.sceneCount-1> PlayerPrefs.GetInt("MAX_LEVEL"))
        {
            PlayerPrefs.SetInt("CURRENT_LEVEL", PlayerPrefs.GetInt("MAX_LEVEL")+1);
            PlayerPrefs.SetInt("MAX_LEVEL", SceneManager.sceneCount);
        }

        if (!PlayerPrefs.HasKey("CURRENT_LEVEL"))
        {
            PlayerPrefs.SetInt("CURRENT_LEVEL", 1);
        }
        SceneManager.LoadScene("LEVEL-" + PlayerPrefs.GetInt("CURRENT_LEVEL").ToString());
    }
}

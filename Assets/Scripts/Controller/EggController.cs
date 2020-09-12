using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class EggController : MonoBehaviour
{
    #region Variables
    [Header("Progress Bar")]
    public Image progressBar;
    private float currentPorgess;
    [SerializeField]
    private Transform finishLine;

    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject continueToPlayPanel;
    public GameObject restartPanel;
    public GameObject levelCompletedPanel;
    public GameObject ShopPanel;

    [Header("UI Texts")]
    public Text restartPanelPercentageCompletedText;
    public Text continueToPlayPanelPercentageCompletedText;

    [Header("SOUNDS")]
    public AudioClip jumpAudio;
    //public AudioClip levelCompleteAudio;
    //public AudioClip gameOverAudio;

    [Header("Used by GroundScript")]
    public bool isGrounded = false;

    //Variables For Controlling
    private float eggSpeed = 2.5f;
    private float jumpForce = 500f;

    //Dummy Variables for videoAds test
    private bool isPlay = true;
    private bool isGameOver = false;
    private bool isShowAdsForContinueAvailable = true;
    private bool isAlreadyOnceAdsShow = false;

    private int coinReward = 125;

    private Rigidbody2D rb2d;
    private AudioSource audioSource;

    private GameObject obstacleHitByThePlayer = null;

    #endregion

    private void Awake() => Time.timeScale = 1f;

    private void Start()
    {
     //    PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("CURRENT_LEVEL"))
        {
            PlayerPrefs.SetInt("CURRENT_LEVEL", 1);
        }

        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();


        if (ShopPanel.activeSelf)
        {
            ShopPanel.SetActive(false);
        }

        PlayerActivityBringingBack();
    }

    private void Update()
    {
        #region egg_controller
        if (isPlay)
        {
            if (Input.touchCount > 0 && !isGrounded)
            {
                transform.Translate(transform.right * eggSpeed * Time.deltaTime);
            }
        }

        if (isGameOver)
        {
            if(Input.touchCount > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        #endregion

        #region Progress Bar
        if(transform.position.x <= finishLine.transform.position.x)
        {
            currentPorgess = transform.position.x / finishLine.position.x;
            progressBar.fillAmount = currentPorgess;
        }
        #endregion

        #region starPanelactivity
        if (transform.position.x > 0.1f)
        {
            if (startPanel.activeSelf)
            {
                startPanel.SetActive(false);
            }
        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Ground")
        {
            print("Egg is Landed");
            //   isGrounded = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x,0f);
            rb2d.AddForce(Vector2.up * jumpForce);

            PlaySound(jumpAudio,1.35f);

        }
        if(col.tag == "Obstacles")
        {
            isShowAdsForContinueAvailable = UnityAdsManager.Instance.IsAdsReady("rewardedVideo");
            if(!isShowAdsForContinueAvailable) isShowAdsForContinueAvailable = UnityAdsManager.Instance.IsAdsReady("rewardedVideo1");
            if (isShowAdsForContinueAvailable && !isAlreadyOnceAdsShow)
            {
                isAlreadyOnceAdsShow = true;
                if(!continueToPlayPanel.activeSelf)
                {
                    continueToPlayPanel.SetActive(true);
                }
                continueToPlayPanelPercentageCompletedText.text = ((int)(currentPorgess * 100f)).ToString() + "% COMPLETED";
                /***PausePlayerActivity**/
                PlayerActivityShutDown();
                obstacleHitByThePlayer = col.gameObject;
            }
            else
            {
                PlayerActivityShutDown();
                RestartPanelActivity();
            }
        }
        if(col.tag == "FinishLine")
        {
            if (!levelCompletedPanel.activeSelf)
            {
                levelCompletedPanel.SetActive(true);
            }
            PlayerPrefs.SetInt("CURRENT_LEVEL", SceneManager.GetActiveScene().buildIndex + 1); //Promote to next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayerActivityShutDown();
            //PlaySound(levelCompleteAudio);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void PlaySound(AudioClip audio, float pitch = 1f)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = audio;
        audioSource.pitch = pitch;
        audioSource.Play();
    }

    #region RestartPanel
    public void RestartPanelActivity()
    {
        if (!restartPanel.activeSelf)
        {
            restartPanel.SetActive(true);
        }

        if (continueToPlayPanel.activeSelf)
        {
            continueToPlayPanel.SetActive(false);
        }

        restartPanelPercentageCompletedText.text = ((int)(currentPorgess * 100f)).ToString() + "% COMPLETED";
        isGameOver = true;

        //PlaySound(gameOverAudio);

    }
    #endregion

    #region if Player hit by obstacles or enemies
    public void PlayerActivityShutDown()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        isPlay = false;

    }


    public void PlayerActivityBringingBack()
    {
        if (obstacleHitByThePlayer != null)
        {
            Destroy(obstacleHitByThePlayer);
        }
        GetComponent<PolygonCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        Time.timeScale = 1f;
        isPlay = true;
        if (continueToPlayPanel.activeSelf)
        {
            continueToPlayPanel.SetActive(false);
        }
    }
    #endregion

    #region LevelCompletedPanel Functions
    public void Claim()
    {
        PlayerPrefs.SetInt("COIN", PlayerPrefs.GetInt("COIN") + coinReward);
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene("LEVEL-" + PlayerPrefs.GetInt("CURRENT_LEVEL").ToString());
    }

    public void Claim3X()
    {
        //After Suceessful Video Shown
        PlayerPrefs.SetInt("COIN", PlayerPrefs.GetInt("COIN") + (coinReward*3));
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene("LEVEL-" + PlayerPrefs.GetInt("CURRENT_LEVEL").ToString());
    }
    #endregion

    #region Unity Rewarded Ads Function

    public void YesIWantToShowAds()
    {
        string placementID = UnityAdsManager.Instance.IsAdsReady("rewardedVideo") ? "rewardedVideo" : "rewardedVideo1";
        Time.timeScale = 0f;
        UnityAdsManager.Instance.ShowRewardedRegularAd(OnRewardedAdClosed,placementID);
    }


    private void OnRewardedAdClosed(ShowResult result)
    {
        Debug.Log("Rewarded ad Closed");
        switch (result)
        {
            case ShowResult.Finished:
                PlayerActivityBringingBack();
                break;
            case ShowResult.Skipped:
                Time.timeScale = 1f;
                RestartPanelActivity();
                break;
            case ShowResult.Failed:
                Time.timeScale = 1f;
                RestartPanelActivity();
                break;
        }
    }

    #endregion
}

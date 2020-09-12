using System;
using UnityEngine;
//#if UNITY_ADS
using UnityEngine.Advertisements;
//#endif

public class UnityAdsManager : MonoBehaviour
{
#region Instance
    private static UnityAdsManager instance;
    public static UnityAdsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UnityAdsManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawn UnityAdsManager", typeof(UnityAdsManager)).GetComponent<UnityAdsManager>();
                }
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }
#endregion

    [Header("Config")]
#if UNITY_ANDROID
    private string gameID = "3791505";
#endif
#if UNITY_IOS
    private string gameID = "3791504";
#endif
    [SerializeField] private bool testMode = false;
    private string rewardedViwedPlacementId = "rewardedVideo";
    private string regulerPlacementId = "video";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Advertisement.Initialize(gameID, testMode);
    }

    public void ShowRegularAd(Action<ShowResult> callback)
    {
#if UNITY_ADS
        if (Advertisement.IsReady(regulerPlacementId))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(regulerPlacementId, so);
        }
        else
            Debug.Log("Ad not ready, consider waiting a bit more.. or going online");
#else
        Debug.Log("Ads not spported");
#endif
    }

    public void ShowRewardedRegularAd(Action<ShowResult> callback,string placementID)
    {
#if UNITY_ADS
        if (Advertisement.IsReady(placementID))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(placementID, so);
        }
        else
        {
            Debug.Log("Ad not ready, consider waiting a bit more.. or going online");
        }
#else
        Debug.Log("Ads not spported");
#endif

    }



    public bool IsAdsReady(string placement)
    {
        return Advertisement.IsReady(placement);
    }
}

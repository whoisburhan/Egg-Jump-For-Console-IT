using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class coinShopAndEverythingElse : MonoBehaviour
{
    [Header("COIN SHOW TEXT")]
    public Text coinShowText;

    [Header("Egg Selection View")]
    public SpriteRenderer eggSpriteRenderer;
    public Image selectedEggShow;

    [Header("SHOP_ITEMS")]
    public GameObject[] shopItems;

    [Header("Egg Sprites")]
    public Sprite[] eggSprites;

    [Header("InSufficient Coin Barrier")]
    public GameObject inSufficientCoinGameObject;

    private int randomUnlockPrice = 1000;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("COIN"))
        {
            PlayerPrefs.SetInt("COIN",1500);
        }

        coinShowText.text = PlayerPrefs.GetInt("COIN").ToString();

        if (!PlayerPrefs.HasKey("SHOP_ITEMS"))
        {
            PlayerPrefs.SetString("SHOP_ITEMS", "YNNNNNNNN");
        }

        if(!PlayerPrefs.HasKey("CURRENT_EGG_INDEX"))
        {
            PlayerPrefs.SetInt("CURRENT_EGG_INDEX", 0);
        }

        SelectEgg(PlayerPrefs.GetInt("CURRENT_EGG_INDEX"));

        EggSlotsActivityChecker();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    #region Selecting Egg
    public void SelectEgg(int x)
    {
        eggSpriteRenderer.sprite = eggSprites[x];
        selectedEggShow.sprite = eggSprites[x];
        PlayerPrefs.SetInt("CURRENT_EGG_INDEX", x);
    }
    #endregion

    #region Everythis on Egg Shop Slots

    public void ShowCoin()
    {
        coinShowText.text = PlayerPrefs.GetInt("COIN").ToString();
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<EggController>().PlayerActivityShutDown();
       
    }

    //Check and show currently availabe Eggs for selection
    public void EggSlotsActivityChecker()
    {
        string shopItemsString = PlayerPrefs.GetString("SHOP_ITEMS");
        for(int i = 0; i < shopItems.Length; i++)
        {
            if(shopItemsString[i] == 'Y')
            {
                if (shopItems[i].activeSelf)
                {
                    shopItems[i].SetActive(false);
                }
            }

            else
            {
                if (!shopItems[i].activeSelf)
                {
                    shopItems[i].SetActive(true);
                }
            }
        }
    }

    //Check and only allow to active "Unlock Random" Button while have sufficient amount of coin
    public void UnlockRandomButtonActivityCheck()
    {
        int coinAmount = PlayerPrefs.GetInt("COIN");
        if (coinAmount < randomUnlockPrice || !UnpurchasedEggLeft())
        {
            if (!inSufficientCoinGameObject.activeSelf)
            {
                inSufficientCoinGameObject.SetActive(true);
            }
        }
        else
        {
            if (inSufficientCoinGameObject.activeSelf)
            {
                inSufficientCoinGameObject.SetActive(false);
            }
        }
    }

    private bool UnpurchasedEggLeft()
    {
        string shopItem = PlayerPrefs.GetString("SHOP_ITEMS");
        for(int i=0;i<shopItems.Length; i++)
        {
            if(shopItem[i] == 'N')
            {
                return true;
            }
        }
        return false;
    }

    public void UnlockRandom()
    {
        PlayerPrefs.SetInt("COIN", PlayerPrefs.GetInt("COIN") - randomUnlockPrice);
        coinShowText.text = PlayerPrefs.GetInt("COIN").ToString();
        StartCoroutine("RandomEggSlotsUnlock");
    }

    IEnumerator RandomEggSlotsUnlock()
    {
        UnlockRandomButtonActivityCheck();

        string shopItemsString = PlayerPrefs.GetString("SHOP_ITEMS");
        int nominatedSlots = Random.Range(0, shopItemsString.Length);

        #region Lottery - 1
        //------------------------ Lottery - 01------------------------
        while (shopItemsString[nominatedSlots] == 'Y')
        {
            nominatedSlots = Random.Range(0, shopItemsString.Length);
        }

        Color shopItemsColor = shopItems[nominatedSlots].GetComponent<Image>().color;
        shopItems[nominatedSlots].GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(.1f);
        shopItems[nominatedSlots].GetComponent<Image>().color = shopItemsColor;
        #endregion

        #region Lottery - 2
        //------------------------ Lottery - 02------------------------
        
        nominatedSlots = Random.Range(0, shopItemsString.Length);
        while (shopItemsString[nominatedSlots] == 'Y')
        {
            nominatedSlots = Random.Range(0, shopItemsString.Length);
        }
        shopItems[nominatedSlots].GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(.1f);
        shopItems[nominatedSlots].GetComponent<Image>().color = shopItemsColor;
        #endregion

        #region Lottery - 3
        //------------------------ Lottery - 03------------------------
        nominatedSlots = Random.Range(0, shopItemsString.Length);
        while (shopItemsString[nominatedSlots] == 'Y')
        {
            nominatedSlots = Random.Range(0, shopItemsString.Length);
        }
        shopItems[nominatedSlots].GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(.1f);
        shopItems[nominatedSlots].GetComponent<Image>().color = shopItemsColor;
        #endregion

        #region Lottery - 4
        //------------------------ Lottery - 04------------------------
        nominatedSlots = Random.Range(0, shopItemsString.Length);
        while (shopItemsString[nominatedSlots] == 'Y')
        {
            nominatedSlots = Random.Range(0, shopItemsString.Length);
        }
        shopItems[nominatedSlots].GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(.1f);
        shopItems[nominatedSlots].GetComponent<Image>().color = shopItemsColor;
        #endregion

        #region Lottery - 5
        //------------------------ Lottery - 05------------------------
        nominatedSlots = Random.Range(0, shopItemsString.Length);
        while (shopItemsString[nominatedSlots] == 'Y')
        {
            nominatedSlots = Random.Range(0, shopItemsString.Length);
        }
        shopItems[nominatedSlots].GetComponent<Image>().color = Color.black;

        #endregion

        if (!shopItems[nominatedSlots].activeSelf)
        {
            shopItems[nominatedSlots].SetActive(true);
        }
        char[] shopItemsStringChar = shopItemsString.ToCharArray();     //C# doesn't allow index modifying in string so convert it to char[]
        shopItemsStringChar[nominatedSlots] = 'Y';
        string str = new string(shopItemsStringChar);
        Debug.Log(str);
        print(str);
        PlayerPrefs.SetString("SHOP_ITEMS", str);

        yield return new WaitForSeconds(.25f);

        EggSlotsActivityChecker();
        SelectEgg(nominatedSlots);
        
    }

    public void AdsForCoins()
    {
        //After Successful add show
        PlayerPrefs.SetInt("COIN", PlayerPrefs.GetInt("COIN") + 100);
        ShowCoin();
        UnlockRandomButtonActivityCheck();
    }

    #endregion

    #region Reward Ads Functions for coin

    public void ShowAdsForCoins()
    {
        bool adsForCoinAvailable = UnityAdsManager.Instance.IsAdsReady("rewardedVideo2");
        if (!adsForCoinAvailable) adsForCoinAvailable = UnityAdsManager.Instance.IsAdsReady("rewardedVideo3");
        if (adsForCoinAvailable)
        {
            Debug.Log("Ads available");
            string placementID = UnityAdsManager.Instance.IsAdsReady("rewardedVideo2") ? "rewardedVideo2" : "rewardedVideo3";
            UnityAdsManager.Instance.ShowRewardedRegularAd(OnRewardedAdClosed, placementID);
        }
    }

    private void OnRewardedAdClosed(ShowResult result)
    {
        Debug.Log("Rewarded ad Closed");
        switch (result)
        {
            case ShowResult.Finished:
                AdsForCoins();
                break;
            case ShowResult.Skipped:
                Debug.Log("Video Skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("Video Failed");
                break;
        }
    }
    #endregion
}

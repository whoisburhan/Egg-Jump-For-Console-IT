using UnityEngine;
using UnityEngine.UI;

public class coinRewardTextAnimation : MonoBehaviour
{
    private int rewardCoin = 125;
    private int alreadyRewarded = 0;
    private float duration;
    private float durationTime = .00000000000005f;

    private Text rewardCoinText;

    private void Start()
    {
        rewardCoinText = GetComponent<Text>();
        duration = durationTime;
    }

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0 && alreadyRewarded<rewardCoin)
        {
            alreadyRewarded += 5;
            rewardCoinText.text = "+" + alreadyRewarded.ToString();
            duration = durationTime;
        }
    }

}

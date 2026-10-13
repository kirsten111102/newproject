using UnityEngine;
using TMPro;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinText;
    public void SetCoinCount(int coinCount)
    {
        coinText.text = "Coins: " + coinCount;
    }
}

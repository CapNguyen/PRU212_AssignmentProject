using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int coinAccquired;
    [SerializeField] TextMeshProUGUI coinsCountText;

    public void Add(int count)
    {
        coinAccquired += count;
        coinsCountText.text = "COINS: " + coinAccquired.ToString(); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
	[SerializeField] GameObject weaponParent;
	public void PlayerGameOver()
    {
        Debug.Log("Game over");
        GetComponent<Player>().enabled = false;
        gameOverPanel.SetActive(true);
		weaponParent.SetActive(false);
    }

	
}

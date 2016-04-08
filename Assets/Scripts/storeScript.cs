using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class storeScript : MonoBehaviour
{
	public Button buyTnt;
	public Button buyLantern;
	public Button buy1Up;
	public Button Return;
	public int countGold;
	public int countSilver;

	// Use this for initialization
	void Start () 
	{
		countGold = PlayerPrefs.GetInt ("goldCount");
		countSilver = PlayerPrefs.GetInt ("silverCount");
		buyTnt = buyTnt.GetComponent<Button> ();
		buyLantern = buyLantern.GetComponent<Button> ();
		buy1Up = buy1Up.GetComponent<Button> ();
		Return = Return.GetComponent<Button> ();
	}
	public void ReturnMenu()
	{
		PlayerPrefs.Save ();
		Application.LoadLevel (0);
	}

	public void BuyTnt()
	{
		if (countGold > 0) {
			int itemAmount = PlayerPrefs.GetInt ("dynamiteCount");
			itemAmount++;
			countGold--;
			PlayerPrefs.SetInt ("dynamiteCount", itemAmount);
			PlayerPrefs.SetInt ("goldCount", countGold);
		}
	}
}


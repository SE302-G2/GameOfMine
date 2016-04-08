using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GUI : MonoBehaviour {

	public Image playerInventory;
	public static bool InventoryOpen = false;
	public void ShowInventory(bool value){
		playerInventory.gameObject.SetActive (value);
		InventoryOpen = value;
	}

}

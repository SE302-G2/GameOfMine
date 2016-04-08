using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PickaxeManager : MonoBehaviour {

	public List<Pickaxe> pickaxes;
	//created by tugberk in sprint 3
	//gets the user selected pickaxe
	public Pickaxe GetPickaxe()
	{
		foreach(Pickaxe pickaxe in pickaxes)
		{
			if(pickaxe.id == PlayerPrefs.GetInt("Pickaxe"))
			{
				return pickaxe;
			}
		}
		return null;
	}
}

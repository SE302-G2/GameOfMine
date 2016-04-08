using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ItemManager:MonoBehaviour
{
	public List<Item> items;

	public Item GetItem(int id)
	{
		foreach(Item item in items)
		{
			if(item.id == id)
			{
				return item;
			}
		}
		return null;
	}
}



using UnityEngine;
using System.Collections;

[System.Serializable]
public class Block{
	/// <summary>
	/// Game block.
	/// </summary>
    public string display_name;
	/// <summary>
	/// The identifier of the block.
	/// </summary>
    public int id;
	/// <summary>
	/// The sprite of the block.
	/// </summary>
    public Sprite sprite;

    public int hitPoint;

}

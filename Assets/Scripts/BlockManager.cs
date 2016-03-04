using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour{

	/// <summary>
	/// The blocks.
	/// </summary>
    public List<Block> Blocks;
    
    //private void Start()
    //{
    //    byte blockID = 1;
    //    string blockName = FindBlock(blockID).display_name;
    //    Debug.Log(blockName);
    //}

	/// <summary>
	/// Finds the block.
	/// </summary>
	/// <returns>The block.</returns>
	/// <param name="id">İdentifier.</param>
    public Block FindBlock(byte id)
    {
        foreach(Block block in Blocks)
        {
            if(block.id == id)
            {
                return block;
            }
        }
        return null;
    }

}

using UnityEngine;
using System.Collections;

public class WorldGen : MonoBehaviour {
	/// <summary>
	/// The block manager.
	/// </summary>
    private BlockManager blockManager;
	/// <summary>
	/// The width of the map.
	/// </summary>
    private int width = 64;
	/// <summary>
	/// The height of the map.
	/// </summary>
    private int height = 128;
	/// <summary>
	/// The blocks in the game.
	/// </summary>
    private Block[,] blocks;

	/// <summary>
	/// Called when game starts.
	/// </summary>
    private void Start()
    {
        blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
        blocks = new Block[width, height];

        //GenerateBlocks();
        //SpawnBlocks();
    }
    
	/// <summary>
	/// Generates the blocks.
	/// </summary>
    private void GenerateBlocks()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                blocks[x, y] = blockManager.FindBlock(3); 
            }
        }
    }
    
	/// <summary>
	/// Spawns the blocks on the world map.
	/// </summary>
    private void SpawnBlocks()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject blockGO = new GameObject();
                SpriteRenderer sr = blockGO.AddComponent<SpriteRenderer>();
                sr.sprite = blocks[x, y].sprite;
                blockGO.name = blocks[x, y].display_name;
                blockGO.transform.position = new Vector3(x, y);
            }
        }
    }

}

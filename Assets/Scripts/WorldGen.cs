using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldGen : MonoBehaviour {
	/// <summary>
	/// The block manager.
	/// </summary>
	/// 
	public GameObject player;
	public float hg = 20f;
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

        GenerateBlocks();
        SpawnBlocks();
    }
    
//	/// <summary>
//	/// Generates the blocks.
//	/// </summary>
   private void GenerateBlocks()
    {
		int playerX = Random.Range (0, width);
		int goldcount = 20;
		int goldnum = 0;

       for(int x = 0; x < width; x++)
        {
			float valuee = Mathf.PerlinNoise (x * 0.05f, 5 * 0.05f);
			float heightt = valuee * 10f + hg;

           for(int y = 0; y < height; y++)
            {
				float num = Random.Range(10,70);
				float num2 = Random.Range(1,30);
				float num3 = Random.Range (1,30);
				if (y < heightt & num > num2) {
					blocks [x, y] = blockManager.FindBlock (1); 
				} 
				else if(num < num2 & y < heightt){
					if (num2 < num3) {
						blocks [x, y] = blockManager.FindBlock (2);
						goldnum += 1;
					} else if (num2 > num3) {
						
						blocks [x, y] = blockManager.FindBlock (3);
					} else {
						blocks [x, y] = blockManager.FindBlock (1);
					}


				}

				else {
					blocks[x, y] = blockManager.FindBlock(0); 
				}
					
               
            }
			if (x == playerX) {
				drawPlayer(x, heightt+3);
			}

       }
    }
	public void destroyBlock(GameObject Block){
	
		int x = (int) Block.transform.position.x;
		int y = (int) Block.transform.position.y;
		blocks [x, y] = blockManager.FindBlock (3);
		GameObject.Destroy (Block);
	}
    
//	/// <summary>
//	/// Spawns the blocks on the world map.
//	/// </summary>
    private void SpawnBlocks()
    {
        for (int x = 0; x < width; x++)
        {
           for (int y = 0; y < height; y++)
			{ if(blocks[x, y] != null){    
                GameObject blockGO = new GameObject();
					BoxCollider2D bc = blockGO.AddComponent<BoxCollider2D>();	
						
				SpriteRenderer sr = blockGO.AddComponent<SpriteRenderer>();
                sr.sprite = blocks[x, y].sprite;
                blockGO.name = blocks[x, y].display_name;
                blockGO.transform.position = new Vector3(x, y);
					blockGO.tag = "block";
				}}
        }
    }
	private void drawPlayer(float x, float y){
		GameObject.Instantiate (player, new Vector3(x, y), Quaternion.identity);
	}

}

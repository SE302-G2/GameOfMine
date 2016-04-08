using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WorldGen : MonoBehaviour {
	/// <summary>
	/// The block manager.
	/// </summary>
	/// 
	public GameObject player;
    private BlockManager blockManager;
	private Pickaxe pickaxe;
	/// <summary>
	/// The width of the map.
	/// </summary>
    public static int widht = 19;
	public List<Chunk> chunks;
	/// <summary>
	/// Called when game starts.
	/// </summary>
    private void Start()
    {
        blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
		chunks = new List<Chunk> ();
		chunks.Add(new Chunk(blockManager,0));
		spawnPlayer (8.5f, 0);
    }


	//updated by Tugberk in sprint 3
	//
	public void Update()
	{
		int playerChuck = Mathf.FloorToInt(GameObject.FindGameObjectWithTag("Player").transform.position.y / 18);
		Debug.Log (playerChuck);
		for (int i = playerChuck; i > playerChuck - 2; i--) {
			bool spawn = true;
			foreach (Chunk chunk in chunks) {
				if (chunk.position == i) {
					spawn = false;
				}
			}

			if (spawn) {

				Chunk newChuck = new Chunk (blockManager, i);
				newChuck.GenerateBlocks();
				SpawnBlocks (newChuck);
				chunks.Add (newChuck);

			}

		}

	}
    
	public void destroyBlock(GameObject Block){
	
		int x = (int) Block.transform.position.x;
		int y = (int) Block.transform.position.y;

		Chunk chunk = ChunkAtPos (0, 0);
		GameObject.Destroy (Block);
	}
    
	private Chunk ChunkAtPos(float x, float y)
	{

		return null;
	}

//	/// <summary>
//	/// Spawns the blocks on the world map.
//	/// </summary>
	//Updated by Tugberk on sprint 3
	//Spawns the blocks chunk by chunk
	private void SpawnBlocks(Chunk chunk)
    {
		for (int x = 0; x < widht; x++)
        {
			for (int y = 0; y < Chunk.size; y++)
			{ 
				if(chunk.blocks[x, y] != null && y != GameObject.FindGameObjectWithTag("Player").transform.position.y
					&& x !=GameObject.FindGameObjectWithTag("Player").transform.position.x){  

		            GameObject blockGO = new GameObject();
					BoxCollider2D bc = blockGO.AddComponent<BoxCollider2D>();	
							
					SpriteRenderer sr = blockGO.AddComponent<SpriteRenderer>();
					sr.sprite = chunk.blocks[x, y].sprite;
					blockGO.name = chunk.blocks[x, y].display_name;
					blockGO.transform.position = new Vector3(x, (chunk.position * Chunk.size) + y);

					blockGO.tag = "block";

					GameObject wall = new GameObject ();
					BoxCollider2D wc = wall.AddComponent<BoxCollider2D> ();
					SpriteRenderer wr = wall.AddComponent<SpriteRenderer> ();
					wc.transform.position = new Vector3 (-1, (chunk.position * Chunk.size) + y);

					GameObject wall2 = new GameObject ();
					BoxCollider2D wc2 = wall2.AddComponent<BoxCollider2D> ();
					SpriteRenderer wr2 = wall2.AddComponent<SpriteRenderer> ();
					wc2.transform.position = new Vector3 (widht+1, (chunk.position * Chunk.size) + y);
				}
			}
        }
    }
	//Created by Tugberk on sprint 3
	//creates a player and returns it
	private GameObject spawnPlayer(float x, float y){
		
		GameObject playerObj = GameObject.Instantiate (player, new Vector3 (x, y), Quaternion.identity) as GameObject;
		return playerObj;

	}

}

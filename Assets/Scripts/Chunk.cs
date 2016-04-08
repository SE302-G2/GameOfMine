using UnityEngine;
using System.Collections;

//Created by tugberk in sprint 3
public class Chunk {
	
	public static int size = 20;
	public static float heightModifier = 20f;
	public int position;
	public Block[,] blocks;
	private BlockManager blockManager;

	public Chunk(BlockManager blockManager, int position)
	{
		this.blockManager = blockManager;
		this.position = position;
		blocks = new Block[WorldGen.widht, size];
	}

	//generates the blocks, will be updated
	public void GenerateBlocks()
	{
		int goldcount = 20;
		int goldnum = 0;

		for(int x = 0; x < WorldGen.widht; x++)
		{
			float pValue = Mathf.PerlinNoise (x * 0.05f, 5 * 0.05f);
			float pheight = pValue * 10f + heightModifier;

			for(int y = 0; y < size; y++)
			{
				float num = Random.Range(10,70);
				float num2 = Random.Range(1,30);
				float num3 = Random.Range (1,30);
				int probTnt = Mathf.FloorToInt(Random.Range (1, 20));
				int probLife = Mathf.FloorToInt(Random.Range (1, 20));
				if (y < pheight & num > num2) {
					blocks [x, y] = blockManager.FindBlock (1); 
				} 
				else if(num < num2 & y < pheight)
				{
					if (probTnt == 5) {
						blocks [x, y] = blockManager.FindBlock (4);
						continue;
					}
					if (probLife == 5) {
						blocks [x, y] = blockManager.FindBlock (5);
						continue;
					}
					if (num2 < num3) {
						blocks [x, y] = blockManager.FindBlock (2);
						goldnum += 1;
					} else if (num2 > num3) {
						blocks [x, y] = blockManager.FindBlock (3);
					}
					else 
					{
						blocks [x, y] = blockManager.FindBlock (1);
					}
				}
				else 
				{
					blocks[x, y] = blockManager.FindBlock(0); 
				}

			}
		}
	}
}

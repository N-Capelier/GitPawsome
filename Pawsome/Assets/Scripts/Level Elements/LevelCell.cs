using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCell
{
	public Vector2 position;

	public bool isWall = false;

	public CellInteractor interactor;
	public List<GameObject> objects = new List<GameObject>();

	public int gCost, hCost, fCost;
	public Vector2Int cameFromCell = new Vector2Int(100, 100);

	public void CalculateFCost()
	{
		fCost = gCost + hCost;
	}

	public LevelCell(float worldXPos, float worldYPos)
	{
		position = new Vector2(worldXPos, worldYPos);
	}
}
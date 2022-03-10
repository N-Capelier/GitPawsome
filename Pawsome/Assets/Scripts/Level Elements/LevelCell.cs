using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCell
{
	public Vector2 position;

	public bool isWall = false;
	public bool isPlayerStartCell = false;

	public CellInteractor interactor;
	public Entity entityOnCell;

	//pathfinding
	public int gCost, hCost, fCost;
	public Vector2Int cameFromCell = new Vector2Int(100, 100);

	//spells
	public int bonAppetit = 0;

	public void BonAppetit()
	{
		if (bonAppetit == 0)
			return;

		bonAppetit--;

		if (entityOnCell == null)
			return;

		entityOnCell.HealMana(1);
	}

	public void CalculateFCost()
	{
		fCost = gCost + hCost;
	}

	public LevelCell(float worldXPos, float worldYPos)
	{
		position = new Vector2(worldXPos, worldYPos);
	}
}
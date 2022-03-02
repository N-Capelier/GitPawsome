using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCell
{
	public Vector2 worldPosition;

	public bool isWall = false;

	public CellInteractor interactor;
	public List<GameObject> objects = new List<GameObject>();

	public LevelCell(float worldXPos, float worldYPos)
	{
		worldPosition = new Vector2(worldXPos, worldYPos);
	}
}
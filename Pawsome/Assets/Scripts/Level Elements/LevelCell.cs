using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCell
{
	public Vector2 position;

	public CellInteractor cellInteractor;
	public List<GameObject> objects = new List<GameObject>();

	public LevelCell(float xPos, float yPos)
	{
		position = new Vector2(xPos, yPos);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachFinder
{
	LevelGrid grid;

	public int[] FindReach(int _startX, int _startY, int _endX, int _endY)
	{
		Vector2Int _start = new Vector2Int(_startX, _startY);
		Vector2Int _end = new Vector2Int(_endX, _endY);

		

		return null;
	}

	public ReachFinder(LevelGrid _grid)
	{
		grid = _grid;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
	LevelGrid grid;

	public int[] FindPath(Vector2 _start, Vector2 _end)
	{
		return FindPath((int)_start.x, (int)_start.y, (int)_end.x, (int)_end.y);
	}

	public int[] FindPath(int _startX, int _startY, int _endX, int _endY)
	{
		Vector2Int _start = new Vector2Int(_startX, _startY);
		Vector2Int _end = new Vector2Int(_endX, _endY);

		// A* Equivalent

		return null;
	}

	public PathFinder(LevelGrid _grid)
	{
		grid = _grid;
	}
}

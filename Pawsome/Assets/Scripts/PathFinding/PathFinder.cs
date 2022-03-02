using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
	LevelGrid grid;

	public List<(int x, int y)> FindPath(int _startX, int _startY, int _endX, int _endY)
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

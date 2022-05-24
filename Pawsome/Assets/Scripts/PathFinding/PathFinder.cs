using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Nicolas
/// Last modified by Nicolas
/// </summary>
public class PathFinder
{
	LevelGrid grid;
	bool allowDiagonals;

	const int MOVE_STRAIGHT_COST = 10;
	const int MOVE_DIAGONAL_COST = 14;

	public Vector2Int[] FindPath(Vector2Int[] _cells, int _startX, int _startY, int _endX, int _endY)
	{
		Vector2Int _startCell = new Vector2Int(_startX, _startY);
		Vector2Int _endCell = new Vector2Int(_endX, _endY);

		List<Vector2Int> _openList = new List<Vector2Int>();
		_openList.Add(_startCell);
		List<Vector2Int> _closedList = new List<Vector2Int>();

		List<Vector2Int> _cellsList = new List<Vector2Int>();
		_cellsList.Add(_startCell);

		for (int i = 0; i < _cells.Length; i++)
		{
			_cellsList.Add(_cells[i]);
		}

		_cells = _cellsList.ToArray();

		for (int i = 0; i < _cells.Length; i++)
		{
			LevelCell _cell = grid.GetCell(_cells[i]);
			_cell.gCost = 99999999;
			_cell.hCost = 0;
			_cell.CalculateFCost();
			_cell.cameFromCell = new Vector2Int(100, 100);
		}

		grid.GetCell(_startCell).gCost = 0;
		grid.GetCell(_startCell).hCost = CalculateDistanceCost(_startCell, _endCell);
		grid.GetCell(_startCell).CalculateFCost();

		while(_openList.Count > 0)
		{
			Vector2Int _currentCell = GetLowestFCostCell(_openList);
			if(_currentCell == _endCell)
			{
				return CalculatePath(_endCell);
			}

			_openList.Remove(_currentCell);
			_closedList.Add(_currentCell);

			foreach(Vector2Int _neighbourCell in GetNeighbourList(_currentCell))
			{
				if (_closedList.Contains(_neighbourCell)) continue;

				int _tentativeGCost = grid.GetCell(_currentCell).gCost + CalculateDistanceCost(_currentCell, _neighbourCell);

				if(_tentativeGCost < grid.GetCell(_neighbourCell).gCost)
				{
					grid.GetCell(_neighbourCell).cameFromCell = _currentCell;
					grid.GetCell(_neighbourCell).gCost = _tentativeGCost;
					grid.GetCell(_neighbourCell).hCost = CalculateDistanceCost(_neighbourCell, _endCell);
					grid.GetCell(_neighbourCell).CalculateFCost();

					if(!_openList.Contains(_neighbourCell))
					{
						_openList.Add(_neighbourCell);
					}
				}
			}
		}

		return null;
	}

	private Vector2Int[] CalculatePath(Vector2Int _endCell)
	{
		List<Vector2Int> _path = new List<Vector2Int>();

		_path.Add(_endCell);

		Vector2Int _currentCell = _endCell;

		while(grid.GetCell(_currentCell).cameFromCell != new Vector2Int(100, 100))
		{
			_path.Add(grid.GetCell(_currentCell).cameFromCell);
			_currentCell = grid.GetCell(_currentCell).cameFromCell;
		}

		_path.Reverse();

		return _path.ToArray();
	}

	List<Vector2Int> GetNeighbourList(Vector2Int _cell)
	{
		List<Vector2Int> _neighbourList = new List<Vector2Int>();

		if(_cell.x - 1 >= 0)
		{
			//left
			_neighbourList.Add(new Vector2Int(_cell.x - 1, _cell.y));
			if(allowDiagonals)
			{
				//left down
				if (_cell.y - 1 >= 0) _neighbourList.Add(new Vector2Int(_cell.x - 1, _cell.y - 1));
				//left up
				if (_cell.y + 1 < grid.GetHeigth()) _neighbourList.Add(new Vector2Int(_cell.x - 1, _cell.y + 1));
			}
		}
		if(_cell.x + 1 < grid.GetWidth())
		{
			//right
			_neighbourList.Add(new Vector2Int(_cell.x + 1, _cell.y));
			if(allowDiagonals)
			{
				//right down
				if (_cell.y - 1 >= 0) _neighbourList.Add(new Vector2Int(_cell.x + 1, _cell.y - 1));
				//right up
				if (_cell.y + 1 < grid.GetHeigth()) _neighbourList.Add(new Vector2Int(_cell.x + 1, _cell.y + 1));
			}
		}
		//down
		if (_cell.y - 1 >= 0) _neighbourList.Add(new Vector2Int(_cell.x, _cell.y - 1));
		//up
		if (_cell.y + 1 < grid.GetHeigth()) _neighbourList.Add(new Vector2Int(_cell.x, _cell.y + 1));

		return _neighbourList;
	}

	int CalculateDistanceCost(Vector2Int a, Vector2Int b)
	{
		int _xDist = Mathf.Abs(a.x - b.x);
		int _yDist = Mathf.Abs(a.y - b.y);
		int _remaining = Mathf.Abs(_xDist - _yDist);
		return MOVE_DIAGONAL_COST * Mathf.Min(_xDist, _yDist) + MOVE_STRAIGHT_COST * _remaining;
	}

	Vector2Int GetLowestFCostCell(List<Vector2Int> _cells)
	{
		Vector2Int _result = _cells[0];
		for (int i = 1; i < _cells.Count; i++)
		{
			if(grid.GetCell(_cells[i]).fCost < grid.GetCell(_result).fCost)
			{
				_result = _cells[i];
			}
		}

		return _result;
	}

	public PathFinder(LevelGrid _grid, bool _canUseDiagonals)
	{
		grid = _grid;
		allowDiagonals = _canUseDiagonals;
	}

	public PathFinder()
	{
		grid = LevelGrid.Instance;
		allowDiagonals = false;
	}
}

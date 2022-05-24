using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Created by Nicolas
/// Last modified by Nicolas
/// </summary>
public class ReachFinder
{
	LevelGrid grid;
	bool reachEntities;

	public ReachFinder(LevelGrid _grid, bool _reachEntities)
	{
		grid = _grid;
		reachEntities = _reachEntities;
	}

	public Vector2Int[] FindDiamondReach(int _startX, int _startY, int _scale)
	{
		List<Vector2Int> _result = new List<Vector2Int>();
		Vector2Int _center = new Vector2Int(_startX, _startY);

		for (int x = 0; x < grid.GetWidth(); x++)
		{
			for (int y = 0; y < grid.GetHeigth(); y++)
			{
				if (!grid.CheckCell(x, y) || _center == new Vector2Int(x, y))
					continue;

				if(Mathf.Abs(x - _startX) + Mathf.Abs(y - _startY) <= _scale && grid.CheckCell(x, y))
				{
					if(!grid.cells[x, y].isWall && (grid.cells[x, y].entityOnCell == null || reachEntities))
						_result.Add(new Vector2Int(x, y));
				}
			}
		}

		return _result.ToArray();
	}

	public Vector2Int[] FindCircleReach(int _startX, int _startY, int _radius)
	{
		List<Vector2Int> _result = new List<Vector2Int>();

		Vector2Int _startPos = new Vector2Int(_startX, _startY);

		for (int x = 0; x < grid.GetWidth(); x++)
		{
			for (int y = 0; y < grid.GetHeigth(); y++)
			{
				if (!grid.CheckCell(x, y) || new Vector2Int(x, y) == _startPos)
					continue;

				if(Vector2.Distance(new Vector2Int(x, y), _startPos) < _radius)
				{
					if (!grid.cells[x, y].isWall && (grid.cells[x, y].entityOnCell == null || reachEntities))
						_result.Add(new Vector2Int(x, y));
				}
			}
		}

		return _result.ToArray();
	}

	public Vector2Int[] FindSquareReach(int _startX, int _startY, int _scale)
	{
		List<Vector2Int> _result = new List<Vector2Int>();
		
		for (int i = 1; i <= _scale; i++)
		{
			for (int _pointer = -_scale; _pointer <= _scale; _pointer++)
			{
				if (grid.CheckCell(_startX + _pointer, _startY - i))
				{
					if (!grid.cells[_startX + _pointer, _startY - i].isWall && (grid.cells[_startX + _pointer, _startY - i].entityOnCell == null || reachEntities))
					{
						_result.Add(new Vector2Int(_startX + _pointer, _startY - i));
					}
				}

				if (grid.CheckCell(_startX + _pointer, _startY + i))
				{
					if (!grid.cells[_startX + _pointer, _startY + i].isWall && (grid.cells[_startX + _pointer, _startY + i].entityOnCell == null || reachEntities))
					{
						_result.Add(new Vector2Int(_startX + _pointer, _startY + i));
					}
				}

				if (grid.CheckCell(_startX + i, _startY + _pointer))
				{
					if (!grid.cells[_startX + i, _startY + _pointer].isWall && (grid.cells[_startX + i, _startY + _pointer].entityOnCell == null || reachEntities))
					{
						_result.Add(new Vector2Int(_startX + i, _startY + _pointer));
					}
				}

				if (grid.CheckCell(_startX - i, _startY + _pointer))
				{
					if (!grid.cells[_startX - i, _startY + _pointer].isWall && (grid.cells[_startX - i, _startY + _pointer].entityOnCell == null || reachEntities))
					{
						_result.Add(new Vector2Int(_startX - i, _startY + _pointer));
					}
				}

			}
		}

		return _result.Distinct().ToArray();
	}

	public Vector2Int[] FindLineReach(int _startX, int _startY, int _scale)
	{
		List<Vector2Int> _xPositive = new List<Vector2Int>();
		List<Vector2Int> _xNegative = new List<Vector2Int>();
		List<Vector2Int> _yPositive = new List<Vector2Int>();
		List<Vector2Int> _yNegative = new List<Vector2Int>();

		bool _runXPositive = true, _runXNegative = true, _runYPositive = true, _runYNegative = true;

		for (int i = 1; i <= _scale; i++)
		{
			if(grid.CheckCell(_startX + i, _startY))
			{
				if (!grid.cells[_startX + i, _startY].isWall && _runXPositive && (grid.cells[_startX + i, _startY].entityOnCell == null || reachEntities))
					_xPositive.Add(new Vector2Int(_startX + i, _startY));
			}
			else
			{
				_runXPositive = false;
			}

			if(grid.CheckCell(_startX - i, _startY))
			{
				if (!grid.cells[_startX - i, _startY].isWall && _runXNegative && (grid.cells[_startX - i, _startY].entityOnCell == null || reachEntities))
					_xNegative.Add(new Vector2Int(_startX - i, _startY));
			}
			else
			{
				_runXNegative = false;
			}

			if(grid.CheckCell(_startX, _startY + i))
			{
				if (!grid.cells[_startX, _startY + i].isWall && _runYPositive && (grid.cells[_startX, _startY + i].entityOnCell == null || reachEntities))
					_yPositive.Add(new Vector2Int(_startX, _startY + i));
			}
			else
			{
				_runYPositive = false;
			}

			if(grid.CheckCell(_startX, _startY - i))
			{
				if (!grid.cells[_startX, _startY - i].isWall && _runYNegative && (grid.cells[_startX, _startY - i].entityOnCell == null || reachEntities))
					_yNegative.Add(new Vector2Int(_startX, _startY - i));
			}
			else
			{
				_runYNegative = false;
			}

			if (!_runXPositive && !_runXNegative && !_runYPositive && !_runYNegative)
				break;
		}

		List<Vector2Int> _result = new List<Vector2Int>();
		_result.AddRange(_xPositive);
		_result.AddRange(_xNegative);
		_result.AddRange(_yPositive);
		_result.AddRange(_yNegative);

		return _result.ToArray();
	}
}

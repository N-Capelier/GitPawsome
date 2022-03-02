using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReachFinder
{
	LevelGrid grid;

	public ReachFinder(LevelGrid _grid)
	{
		grid = _grid;
	}

	public List<(int x, int y)> FindDiamondReach(int _startX, int _startY, int _scale)
	{
		

		return null;
	}

	public List<(int x, int y)> FindCircleReach(int _startX, int _startY, int _radius)
	{


		return null;
	}

	public List<(int x, int y)> FindSquareReach(int _startX, int _startY, int _scale)
	{
		List<(int x, int y)> _result = new List<(int x, int y)>();
		
		for (int i = 1; i <= _scale; i++)
		{
			for (int _pointer = -_scale; _pointer <= _scale; _pointer++)
			{
				if (LevelGrid.CheckCell(_startX + _pointer, _startY - i))
				{
					if (!grid.cells[_startX + _pointer, _startY - i].isWall)
					{
						_result.Add((_startX + _pointer, _startY - i));
					}
				}

				if (LevelGrid.CheckCell(_startX + _pointer, _startY + i))
				{
					if (!grid.cells[_startX + _pointer, _startY + i].isWall)
					{
						_result.Add((_startX + _pointer, _startY + i));
					}
				}

				if (LevelGrid.CheckCell(_startX + i, _startY + _pointer))
				{
					if (!grid.cells[_startX + i, _startY + _pointer].isWall)
					{
						_result.Add((_startX + i, _startY + _pointer));
					}
				}

				if (LevelGrid.CheckCell(_startX - i, _startY + _pointer))
				{
					if (!grid.cells[_startX - i, _startY + _pointer].isWall)
					{
						_result.Add((_startX - i, _startY + _pointer));
					}
				}

			}
		}

		return _result.Distinct().ToList();
	}

	public List<(int x, int y)> FindLineReach(int _startX, int _startY, int _scale)
	{
		List<(int x, int y)> _xPositive = new List<(int x, int y)>();
		List<(int x, int y)> _xNegative = new List<(int x, int y)>();
		List<(int x, int y)> _yPositive = new List<(int x, int y)>();
		List<(int x, int y)> _yNegative = new List<(int x, int y)>();

		bool _runXPositive = true, _runXNegative = true, _runYPositive = true, _runYNegative = true;

		for (int i = 1; i <= _scale; i++)
		{
			if(LevelGrid.CheckCell(_startX + i, _startY))
			{
				if (!grid.cells[_startX + i, _startY].isWall && _runXPositive)
					_xPositive.Add((_startX + i, _startY));
			}
			else
			{
				_runXPositive = false;
			}

			if(LevelGrid.CheckCell(_startX - i, _startY))
			{
				if (!grid.cells[_startX - i, _startY].isWall && _runXNegative)
					_xNegative.Add((_startX - i, _startY));
			}
			else
			{
				_runXNegative = false;
			}

			if(LevelGrid.CheckCell(_startX, _startY + i))
			{
				if (!grid.cells[_startX, _startY + i].isWall && _runYPositive)
					_yPositive.Add((_startX, _startY + i));
			}
			else
			{
				_runYPositive = false;
			}

			if(LevelGrid.CheckCell(_startX, _startY - i))
			{
				if (!grid.cells[_startX, _startY - i].isWall && _runYNegative)
					_yNegative.Add((_startX, _startY - i));
			}
			else
			{
				_runYNegative = false;
			}

			if (!_runXPositive && !_runXNegative && !_runYPositive && !_runYNegative)
				break;
		}

		List<(int x, int y)> _result = new List<(int x, int y)>();
		_result.AddRange(_xPositive);
		_result.AddRange(_xNegative);
		_result.AddRange(_yPositive);
		_result.AddRange(_yNegative);

		return _result;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Nicolas
/// Last modified by Nicolas
/// </summary>
public class LevelGrid : Singleton<LevelGrid>
{
	[Header("Params")]
	[SerializeField] int gridWidth;
	[SerializeField] int gridHeight;
	float cellSize = 1f;

	[Header("References")]
	[SerializeField] GameObject cellInteractorPrefab;

	public LevelCell[,] cells;

	private void Start()
	{
		CreateSingleton();
		InitGrid();
	}

	private void InitGrid()
	{
		cells = new LevelCell[gridWidth, gridHeight];

		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				cells[x, y] = new LevelCell(x * cellSize, y * cellSize);

				CellInteractor _cellInteractor = Instantiate(cellInteractorPrefab, new Vector3(x * cellSize, -0.047f, y * cellSize), Quaternion.identity).GetComponent<CellInteractor>();
				_cellInteractor.transform.parent = transform;
				_cellInteractor.gameObject.name = $"[{x}|{y}] Cell Interactor";
				cells[x, y].interactor = _cellInteractor;
				_cellInteractor.levelCell = cells[x, y];
				if(x + y <= 3)
				{
					_cellInteractor.levelCell.isPlayerStartCell = true;
					_cellInteractor.SetRendererAlpha(.3f);
					_cellInteractor.SetRendererColor(Color.blue);
				}
			}
		}

		cells[2, 2].isWall = true;
		cells[5, 2].isWall = true;
		cells[2, 5].isWall = true;
		cells[5, 5].isWall = true;
	}

	public bool CheckCell(int x, int y)
	{
		if (x >= 0 && x < Instance.gridWidth && y >= 0 && y < Instance.gridHeight)
			return true;
		else return false;
	}

	public void HideAllInteractors()
	{
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				cells[x, y].interactor.SetRendererAlpha(0f);
			}
		}
	}

	#region Position Conversion

	public Vector2 GridToWorldPosition(Vector2 _gridPosition)
	{
		return GridToWorldPosition((int)_gridPosition.x, (int)_gridPosition.y);
	}

	public Vector2 GridToWorldPosition(int _x, int _y)
	{
		return new Vector2(_x * cellSize, _y * cellSize);
	}

	public Vector2 WorldToGridPosition(Vector2 _worldPosition)
	{
		return WorldToGridPosition(_worldPosition.x, _worldPosition.y);
	}

	public Vector2 WorldToGridPosition(float _x, float _y)
	{
		return new Vector2((int)(_x / cellSize), (int)(_y / cellSize));
	}

	#endregion

	#region Get elements

	public bool Exist(Vector2Int _cell)
	{
		if (_cell.x >= GetWidth() || _cell.y >= GetWidth() || _cell.x < 0 || _cell.y < 0)
		{
			return false;
		}
		else
			return true;
	}

	public int GetWidth()
	{
		return gridWidth;
	}

	public int GetHeigth()
	{
		return gridHeight;
	}

	public LevelCell GetCell(Vector2 _gridPosition)
	{
		return GetCell((int)_gridPosition.x, (int)_gridPosition.y);
	}

	public LevelCell GetCell(int _x, int _y)
	{
		return cells[_x, _y];
	}

	#endregion
}

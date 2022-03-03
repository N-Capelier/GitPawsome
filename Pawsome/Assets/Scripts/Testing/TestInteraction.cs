using UnityEngine;
using System.Collections.Generic;

public class TestInteraction : MonoBehaviour
{
	private void Start()
	{
		BattleInputManager.Interaction += OnUserInteract;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			foreach(LevelCell _cell in LevelGrid.Instance.cells)
			{
				_cell.interactor.SetRendererColor(Color.blue);
			}
		}
	}

	private void OnUserInteract(CellInteractor interactor)
	{
		//ReachFinder _finder = new ReachFinder(LevelGrid.Instance);

		//Vector2Int[] _reachableCells = _finder.FindDiamondReach((int)interactor.levelCell.worldPosition.x, (int)interactor.levelCell.worldPosition.y, 3);

		//foreach (Vector2Int _cell in _reachableCells)
		//{
		//	LevelGrid.Instance.cells[_cell.x, _cell.y].interactor.SetRendererColor(Color.red);
		//}

		PathFinder _pathFinder = new PathFinder(LevelGrid.Instance, true);

		List<Vector2Int> _cellsList = new List<Vector2Int>();

		for (int i = 0; i < LevelGrid.Instance.GetWidth(); i++)
		{
			for (int j = 0; j < LevelGrid.Instance.GetHeigth(); j++)
			{
				_cellsList.Add(new Vector2Int(i, j));
				LevelGrid.Instance.cells[i, j].interactor.SetRendererColor(Color.blue);
			}
		}

		Vector2Int[] _path = _pathFinder.FindPath(_cellsList.ToArray(), 0, 0, (int)interactor.levelCell.worldPosition.x, (int)interactor.levelCell.worldPosition.y);

		for (int i = 0; i < _path.Length; i++)
		{
			LevelGrid.Instance.cells[_path[i].x, _path[i].y].interactor.SetRendererColor(Color.red);
		}
	}
}

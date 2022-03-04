using UnityEngine;
using System.Collections.Generic;

public class TestInteraction : MonoBehaviour
{
	[SerializeField] Entity player;

	private void Start()
	{
		BattleInputManager.PrimaryInteraction += OnUserInteract;
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
		PathFinder _pathFinder = new PathFinder(LevelGrid.Instance, false);

		List<Vector2Int> _cellsList = new List<Vector2Int>();

		for (int i = 0; i < LevelGrid.Instance.GetWidth(); i++)
		{
			for (int j = 0; j < LevelGrid.Instance.GetHeigth(); j++)
			{
				_cellsList.Add(new Vector2Int(i, j));
				LevelGrid.Instance.cells[i, j].interactor.SetRendererColor(Color.blue);
			}
		}

		Vector2Int[] _path = _pathFinder.FindPath(_cellsList.ToArray(), (int)player.transform.position.x, (int)player.transform.position.z, (int)interactor.levelCell.position.x, (int)interactor.levelCell.position.y);

		foreach (Vector2Int _cell in _path)
		{
			LevelGrid.Instance.cells[_cell.x, _cell.y].interactor.SetRendererColor(Color.red);
		}

		player.MoveAlongPath(_path);
	}
}

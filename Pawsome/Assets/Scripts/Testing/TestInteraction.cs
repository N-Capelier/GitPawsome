using UnityEngine;

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
		//interactor.SetRendererColor(Color.red);

		ReachFinder _finder = new ReachFinder(LevelGrid.Instance);

		Vector2Int[] _reachableCells = _finder.FindDiamondReach((int)interactor.levelCell.worldPosition.x, (int)interactor.levelCell.worldPosition.y, 3);

		foreach (Vector2Int _cell in _reachableCells)
		{
			LevelGrid.Instance.cells[_cell.x, _cell.y].interactor.SetRendererColor(Color.red);
		}
	}
}

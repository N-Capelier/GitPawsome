using UnityEngine;

public class TestInteraction : MonoBehaviour
{
	private void Start()
	{
		BattleInputManager.Interaction += OnUserInteract;
	}

	private void OnUserInteract(CellInteractor interactor)
	{
		//interactor.SetRendererColor(Color.red);

		ReachFinder _finder = new ReachFinder(LevelGrid.Instance);

		Vector2Int[] _reachableCells = _finder.FindCircleReach((int)interactor.levelCell.worldPosition.x, (int)interactor.levelCell.worldPosition.y, 5);

		foreach (Vector2Int _cell in _reachableCells)
		{
			LevelGrid.Instance.cells[_cell.x, _cell.y].interactor.SetRendererColor(Color.red);
		}
	}
}

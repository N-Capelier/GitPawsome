using System.Collections;
using System.Collections.Generic;
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

		//List<(int x, int y)> _reachableCells = _finder.FindLineReach((int)interactor.levelCell.worldPosition.x, (int)interactor.levelCell.worldPosition.y, 2);

		//foreach((int x, int y) _cell in _reachableCells)
		//{
		//	LevelGrid.Instance.cells[_cell.x, _cell.y].interactor.SetRendererColor(Color.red);
		//}
	}
}

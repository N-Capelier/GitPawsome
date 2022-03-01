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
		interactor.SetRendererColor(Color.red);
	}
}

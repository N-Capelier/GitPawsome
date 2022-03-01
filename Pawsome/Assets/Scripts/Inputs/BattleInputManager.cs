using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInputManager : MonoBehaviour
{
	[SerializeField] LayerMask interactionLayer;

	public delegate void InteractionHandler(CellInteractor interactor);
	public static event InteractionHandler Interaction;

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			RayCastFromCamera();
		}
	}
	void RayCastFromCamera()
	{
		RaycastHit _hit;
		Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(_ray, out _hit, Mathf.Infinity, interactionLayer))
		{
			CellInteractor _interactor = _hit.transform.GetComponent<CellInteractor>();
			Interaction?.Invoke(_interactor);
		}
	}
}

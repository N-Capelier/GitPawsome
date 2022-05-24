using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Created by Nicolas
/// Last modified by Rémi
/// </summary>
public class BattleInputManager : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer;

    public delegate void InteractionHandler(CellInteractor interactor);
    public static event InteractionHandler PrimaryInteraction;
    public static event InteractionHandler SecondaryInteraction;

    [ReadOnly]
    public CellInteractor hoveredInteractor;

    private void Update()
    {
        CellHovering();

        if (Input.GetMouseButtonDown(1)) OnRightClick();
        if (Input.GetMouseButtonDown(0)) OnLeftClick();
    }

    void OnLeftClick()
    {
        if (hoveredInteractor != null)
            PrimaryInteraction?.Invoke(hoveredInteractor);
    }

    void OnRightClick()
    {
        if (hoveredInteractor != null)
            SecondaryInteraction?.Invoke(hoveredInteractor);
    }

    void CellHovering()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, interactionLayer))
        {
            CellInteractor _interactor = _hit.transform.GetComponent<CellInteractor>();
            hoveredInteractor = _interactor;
        }
        else
		{
            hoveredInteractor = null;
		}
    }

    //Use this only if you know what you are doing
    //Currently I dont
    public static void EmergencyInteractionTrigger()
    {
        PrimaryInteraction?.Invoke(null);
    }
}
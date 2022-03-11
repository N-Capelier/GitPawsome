using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using NaughtyAttributes;

public class UnitCard : MonoBehaviour
{
    public enum State { Base, Hover, Selected }

    [SerializeField]
    Image portrait;
    [SerializeField]
    TextMeshProUGUI nameDisplay;
    [SerializeField]
    TextMeshProUGUI manaValueDisplay;
    [SerializeField]
    TextMeshProUGUI hpValueDisplay;

    [Header("Stats Elements")]
    [SerializeField]
    StatsCouple defStatDisplay;
    [SerializeField]
    StatsCouple atkStatDisplay;
    [SerializeField]
    StatsCouple powStatDisplay;
    [SerializeField]
    StatsCouple moveStatDisplay;

    [Header("Interractor Elements")]
    [SerializeField]
    Image highlight;
    

    [Space, SerializeField, ReadOnly]
    private State _currentState;
    [ReadOnly]
    public InstaCat linkedUnit;

    private UnitPlacementUI placementUI;

    public Action<UnitCard> CardSelected;

    public State CurrentState 
    {
        get { return _currentState; }
        set 
        {
            UpdateState(value); 
        }    
    }

    public void OnDestroy()
    {
        OnUnMount();
    }

    public void OnMount(InstaCat unit, UnitPlacementUI _placementUI)
    {
        placementUI = _placementUI;
        linkedUnit = unit;

        //Set base unit Infos
        portrait.sprite = unit.CatSprite;
        nameDisplay.text = unit.catName;
        manaValueDisplay.text = unit.baseMana.ToString();
        hpValueDisplay.text = unit.baseHealth.ToString();

        //Set Stats Icon
        defStatDisplay.statImage.sprite = placementUI.mode.GetStatIcon(defStatDisplay.linkedStat);
        atkStatDisplay.statImage.sprite = placementUI.mode.GetStatIcon(atkStatDisplay.linkedStat);
        powStatDisplay.statImage.sprite = placementUI.mode.GetStatIcon(powStatDisplay.linkedStat);
        moveStatDisplay.statImage.sprite = placementUI.mode.GetStatIcon(moveStatDisplay.linkedStat);

        //Set Stats Value
        defStatDisplay.statValueDisplay.text = unit.baseDefense.ToString();
        atkStatDisplay.statValueDisplay.text = unit.baseAttack.ToString();
        powStatDisplay.statValueDisplay.text = unit.basePower.ToString();
        moveStatDisplay.statValueDisplay.text = unit.movePoints.ToString();

        //HighLight
        highlight.color = placementUI.mode.GetHighLightColor(_currentState);
    }

    public void OnUnMount()
    {
        CardSelected = null;
    }

    public void OnHoverIn()
    {
        switch (CurrentState)
        {
            case State.Base:
                CurrentState = State.Hover;
                break;
            default:
                break;
        }
    }

    public void OnHoverOut()
    {
        switch (CurrentState)
        {
            case State.Hover:
                CurrentState = State.Base;
                break;
            default:
                break;
        }
    }

    public void OnClick() 
    {
        switch (CurrentState)
        {
            case State.Hover:
                CurrentState = State.Selected;
                CardSelected?.Invoke(this);
                break;
            case State.Selected:
                CurrentState = State.Hover;
                CardSelected?.Invoke(null);
                break;
            default:
                break;
        }
    }

    public void UpdateState(State newState)
    {
        if (newState == _currentState)
            return;
        highlight.color = placementUI.mode.GetHighLightColor(newState);
        _currentState = newState;
    }
}

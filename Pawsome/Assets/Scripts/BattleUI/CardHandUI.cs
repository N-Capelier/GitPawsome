using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Rémi
/// Last modified by Rémi
/// </summary>
public class CardHandUI : MonoBehaviour
{
    public List<CardContainer> allCardContainer;

    public BattleUIMode mode;

    private CardContainer selectedCard;

    public void OnMount(BattleUIMode _mode)
    {
        mode = _mode;
        mode.fsm.EnterTurn += OnTurnStart;
    }

    public void OnUnMount()
    {
        mode.fsm.EnterTurn -= OnTurnStart;
        //BattleStateMachine.SelectSpell += 
    }

    private void OnTurnStart()
    {
        ChangeTarget(mode.GetCurrentPlaying().hand);
    }

    void ChangeTarget(List<Spell> spells)
    {
        for(int i = 0; i < spells.Count; i++)
        {
            allCardContainer[i].OnUnMount();
            allCardContainer[i].OnMount(spells[i], this);
            allCardContainer[i].CardSelected += SelectCard;
        }
    }

    public void SelectCard(CardContainer card)
    {
        if (card != selectedCard)
        {
            if (selectedCard != null)
            {
                if(selectedCard.CurrentState != UnitCard.State.Hover)
                {
                    selectedCard.UpdateState(UnitCard.State.Base);
                }
                BattleInputManager.EmergencyInteractionTrigger();
                if(card != null)
                PlayerTurnState.EmergencySpellCast(card.cardIndex);
            }

            selectedCard = card;
        }
    }
}

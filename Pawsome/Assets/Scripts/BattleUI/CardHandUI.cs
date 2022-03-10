using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                selectedCard.UpdateState(UnitCard.State.Base);

            selectedCard = card;
        }
    }
}

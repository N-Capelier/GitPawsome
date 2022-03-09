using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandUI : MonoBehaviour
{
    public List<CardContainer> allCardContainer;

    public BattleUIMode mode;

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
            allCardContainer[i].OnMount(spells[i], this);
    }
}

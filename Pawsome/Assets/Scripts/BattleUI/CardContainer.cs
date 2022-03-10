using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using System;

public class CardContainer : MonoBehaviour
{
    [SerializeField]
    Image background;
    [SerializeField]
    Image picture;
    [SerializeField]
    Image range;
    [SerializeField]
    TextMeshProUGUI spellName;
    [SerializeField]
    TextMeshProUGUI manaCost;
    [SerializeField]
    TextMeshProUGUI spellDescription;
    [SerializeField]
    Button cardInterractor;

    [Header("Interractor Elements")]
    [SerializeField]
    Image highlight;

    [Space, SerializeField, ReadOnly]
    Spell linkedSpell;
    [SerializeField, ReadOnly]
    private UnitCard.State _currentState;

    CardHandUI cardHand;

    UnitCard.State CurrentState
    {
        get { return _currentState; }
        set
        {
            UpdateState(value);
        }
    }

    public Action<CardContainer> CardSelected;


    public void OnMount(Spell _linkedSpell, CardHandUI _cardHand)
    {
        if(CurrentState == UnitCard.State.Selected)
            CurrentState = UnitCard.State.Base;
        
        cardHand = _cardHand;
        linkedSpell = _linkedSpell;
        DrawCard();
    }

    public void OnUnMount()
    {
        CardSelected = null;
    }

    void DrawCard()
    {
        background.sprite = cardHand.mode.GetCardBackground(linkedSpell.spellClass);
        picture.sprite = linkedSpell.spellSprite;
        //TODO: implemente range once it's included into spell
        //range.sprite = ;
        spellName.text = linkedSpell.spellName;
        spellDescription.text = linkedSpell.description;
    }

    public void OnCardClicked()
    {
        //TODO: Link to spell usage logic
        Debug.Log("CardClicked");
    }

    public void OnHoverIn()
    {
        switch (CurrentState)
        {
            case UnitCard.State.Base:
                CurrentState = UnitCard.State.Hover;
                break;
            default:
                break;
        }
    }

    public void OnHoverOut()
    {
        switch (CurrentState)
        {
            case UnitCard.State.Hover:
                CurrentState = UnitCard.State.Base;
                break;
            default:
                break;
        }
    }

    public void OnClick()
    {
        switch (CurrentState)
        {
            case UnitCard.State.Hover:
                CurrentState = UnitCard.State.Selected;
                CardSelected.Invoke(this);
                break;
            case UnitCard.State.Selected:
                CurrentState = UnitCard.State.Hover;
                CardSelected.Invoke(null);
                break;
            default:
                break;
        }
    }

    public void UpdateState(UnitCard.State newState)
    {
        if (newState == _currentState)
            return;
        highlight.color = cardHand.mode.GetHighLightColor(newState);
        _currentState = newState;
    }
}

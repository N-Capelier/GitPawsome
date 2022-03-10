using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitPlacementUI : MonoBehaviour
{
    public BattleInitUIMode mode;

    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    Button placeButton;
    [SerializeField]
    UnitCard cardTemplate;
    [SerializeField]
    Transform cardsRoot;

    private UnitCard selectedCard;
    private List<UnitCard> allCards;

    public Action<InstaCat> ConfirmPlacement;

    private void OnDestroy()
    {
        OnUnMount();
    }

    public void OnMount(List<InstaCat> units, BattleInitUIMode _mode)
    {
        mode = _mode;
        ConfirmPlacement += mode.StartGridPlacement;

        allCards = new List<UnitCard>();

        foreach(InstaCat unit in units)
        {
            var card = Instantiate(cardTemplate, cardsRoot);
            card.OnMount(unit, this);
            card.CardSelected += SelectUnit;
            allCards.Add(card);
        }

        string titleOrderText = string.Empty;
        switch (units.Count)
        {
            case 1:
                titleOrderText = "last";
                break;
            case 2:
                titleOrderText = "second";
                break;
            case 3:
                titleOrderText = "first";
                break;
            default:
                break;
        }
        title.text = string.Format("Choose your {0} to play", titleOrderText);
        cardTemplate.gameObject.SetActive(false);
        placeButton.interactable = false;
    }

    public void OnUnMount()
    {
        ConfirmPlacement -= mode.StartGridPlacement;
        foreach (var card in allCards)
		{
            card.CardSelected -= SelectUnit;
            Destroy(card.gameObject);
        }
        cardTemplate.gameObject.SetActive(true);
    }

    public void SelectUnit(UnitCard card)
    {
        if (card != selectedCard)
        {
            if(selectedCard != null)
            {
                if(selectedCard.CurrentState != UnitCard.State.Hover)
                selectedCard.UpdateState(UnitCard.State.Base);
            }
            selectedCard = card;
            if (selectedCard != null)
                placeButton.interactable = true;
            else
                placeButton.interactable = false;
        }
    }

    public void StartPlacement()
    {
        ConfirmPlacement?.Invoke(selectedCard.linkedUnit);
    }
}

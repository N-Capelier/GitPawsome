using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

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

    [Space, SerializeField, ReadOnly]
    Spell linkedSpell;

    CardHandUI cardHand;


    public void OnMount(Spell _linkedSpell, CardHandUI _cardHand)
    {
        cardHand = _cardHand;
        linkedSpell = _linkedSpell;
        DrawCard();
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
}

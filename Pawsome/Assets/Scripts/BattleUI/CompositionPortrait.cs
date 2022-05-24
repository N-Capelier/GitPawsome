using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Created by Rémi
/// Last modified by Rémi
/// </summary>
public class CompositionPortrait : MonoBehaviour
{
    [SerializeField]
    Image portrait;
    [SerializeField]
    Image portaitBorder;

    [SerializeField]
    Image healthFiller;
    [SerializeField]
    TextMeshProUGUI healthDisplay;

    [SerializeField]
    Image manaFiller;
    [SerializeField]
    TextMeshProUGUI manaDisplay;
    
    [Space, SerializeField, ReadOnly]
    Entity linkedEntity;

    CompositionUI compositionUI;

    private void OnDestroy()
    {
        OnUnMount();
    }

    public void OnMount(Entity _linkedEntity, CompositionUI _compUI)
    {
        linkedEntity = _linkedEntity;
        compositionUI = _compUI;

        linkedEntity.HealthChanged += UpdateHealth;
        linkedEntity.ManaChanged += UpdateMana;
        compositionUI.mode.fsm.EnterTurn += UpdatePortaitsGraphics;

        UpdateHealth();
        UpdateMana();
    }

    public void OnUnMount()
    {
		try
		{
            linkedEntity.HealthChanged -= UpdateHealth;
            linkedEntity.ManaChanged -= UpdateMana;
            compositionUI.mode.fsm.EnterTurn -= UpdatePortaitsGraphics;
        }
        catch
		{
            // Ingénieur informaticien, je suis
		}
    }

    void UpdateHealth()
    {
        var instaCat = linkedEntity.InstaCat;

        healthDisplay.text = instaCat.health.ToString();
        healthFiller.fillAmount = (float)instaCat.health / (float)instaCat.GetHealth();
    }

    void UpdateMana()
    {
        var instaCat = linkedEntity.InstaCat;

        manaDisplay.text = instaCat.mana.ToString();
        manaFiller.fillAmount = (float)instaCat.mana / (float)instaCat.GetMana();
    }

    void UpdatePortaitsGraphics()
    {
        portrait.sprite = linkedEntity.InstaCat.CatSprite;
        portaitBorder.color = BattleUIMode.GetBorderColor(linkedEntity);

        if (linkedEntity.isPlaying)
            transform.localScale = Vector3.one * 1.3f;
        else
            transform.localScale = Vector3.one;
    }
}

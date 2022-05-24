using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

/// <summary>
/// Created by Rémi
/// Last modified by Rémi
/// </summary>
[System.Serializable]
public struct StatsCouple
{
    public AllStats linkedStat;
    public Image statImage;
    public TextMeshProUGUI statValueDisplay;
}

public class EntityDetailsUI : MonoBehaviour
{
    [System.Serializable]
    internal class FillableBar
    {
        [SerializeField]
        internal Image filler;
        [SerializeField]
        internal TextMeshProUGUI valueDisplay;

        public void UpdateFillable(float current, float max)
        {
            filler.fillAmount = current / max;
            valueDisplay.text = string.Format("{0} / {1}", current, max);
        }
    }

    [Header("Entity Presentation")]
    [SerializeField]
    Image portrait;
    [SerializeField]
    Image border;
    [SerializeField]
    Image classIcon;
    [SerializeField]
    TextMeshProUGUI entityName;

    [Header("Entity Stats")]
    [SerializeField]
    StatsCouple atkDisplay;
    [SerializeField]
    StatsCouple defDisplay;
    [SerializeField]
    StatsCouple powDisplay;
    [SerializeField]
    StatsCouple moveDisplay;
    [SerializeField]
    FillableBar healthBar;
    [SerializeField]
    FillableBar manaBar;

    [Header("Status Modificator")]
    [SerializeField]
    Transform statusModRoot;
    [SerializeField]
    StatusElement statusElementTemplate;

    [Space, SerializeField, ReadOnly]
    Entity currentPlayingEntity;
    [Space, SerializeField, ReadOnly]
    Entity currentDisplayedEntity;

    BattleUIMode mode;

    private void OnDestroy()
    {
        OnUnMount();
    }

    public void OnMount(BattleUIMode _mode)
    {
        mode = _mode;
        mode.fsm.EnterTurn += OnTurnStart;
        InitStatsIcon();
    }

    public void OnUnMount()
    {
        mode.fsm.EnterTurn -= OnTurnStart;
        currentDisplayedEntity.HealthChanged -= OnHealthChanged;
        currentDisplayedEntity.ManaChanged -= OnManaChanged;
    }

    public void OnTurnStart()
    {
        currentPlayingEntity = mode.GetCurrentPlaying();
        ChangeTarget(currentPlayingEntity);
    }

    void InitStatsIcon()
    {
        atkDisplay.statImage.sprite = mode.GetStatIcon(atkDisplay.linkedStat);
        defDisplay.statImage.sprite = mode.GetStatIcon(defDisplay.linkedStat);
        powDisplay.statImage.sprite = mode.GetStatIcon(powDisplay.linkedStat);
        moveDisplay.statImage.sprite = mode.GetStatIcon(moveDisplay.linkedStat);
    }

    public void ChangeTarget(Entity newTarget)
    {
        //Unsuscribing from previous displayed entity
        if (currentDisplayedEntity != null)
        {
            currentDisplayedEntity.HealthChanged -= OnHealthChanged;
            currentDisplayedEntity.ManaChanged -= OnManaChanged;
        }

        //Subscribing from new displayed entity
        currentDisplayedEntity = newTarget;
        currentDisplayedEntity.HealthChanged += OnHealthChanged;
        currentDisplayedEntity.ManaChanged += OnManaChanged;


        //Set Enity Presentation Elements
        portrait.sprite = newTarget.InstaCat.largePortrait;
        border.color = BattleUIMode.GetBorderColor(newTarget);
        classIcon.sprite = mode.GetClassIcon(newTarget.InstaCat);
        entityName.text = newTarget.InstaCat.catName;

        //Set Entity Stats
        //Regular Stats
        atkDisplay.statValueDisplay.text = newTarget.InstaCat.attack.ToString();
        defDisplay.statValueDisplay.text = newTarget.InstaCat.defense.ToString();
        powDisplay.statValueDisplay.text = newTarget.InstaCat.attack.ToString(); //TODO: Replace this by the power value once implemented
        moveDisplay.statValueDisplay.text = newTarget.InstaCat.movePoints.ToString();

        //Bar Values
        healthBar.UpdateFillable((float)newTarget.InstaCat.health, (float)newTarget.InstaCat.GetHealth());
        manaBar.UpdateFillable((float)newTarget.InstaCat.mana, (float)newTarget.InstaCat.GetMana());
    } 

    //TODO: Implement status UI logic once status are implemented

    void OnHealthChanged() => healthBar.UpdateFillable((float)currentDisplayedEntity.InstaCat.health, (float)currentDisplayedEntity.InstaCat.GetHealth());
    void OnManaChanged() => manaBar.UpdateFillable((float)currentDisplayedEntity.InstaCat.mana, (float)currentDisplayedEntity.InstaCat.GetMana());
}

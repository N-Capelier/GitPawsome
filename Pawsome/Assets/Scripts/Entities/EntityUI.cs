using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created by Rémi
/// Last modified by Rémi
/// </summary>
public class EntityUI : MonoBehaviour
{
    public Transform uiTransform;
    public Entity linkedEntity;

    public Image healthFiller;
    public Image manafiller;


    void Start()
    {
        uiTransform.forward = Camera.main.transform.forward;
        linkedEntity.HealthChanged += UpdateHealth;
        linkedEntity.ManaChanged += UpdateMana;
    }


    public void UpdateHealth()
    {
        healthFiller.fillAmount = (float)linkedEntity.InstaCat.health / (float)linkedEntity.InstaCat.GetHealth();
    }

    public void UpdateMana()
    {
        manafiller.fillAmount = (float)linkedEntity.InstaCat.mana / (float)linkedEntity.InstaCat.GetMana();
    }

}

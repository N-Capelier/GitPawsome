using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        healthFiller.fillAmount = linkedEntity.InstaCat.health / linkedEntity.InstaCat.baseHealth;
    }

    public void UpdateMana()
    {
        manafiller.fillAmount = linkedEntity.InstaCat.mana / linkedEntity.InstaCat.baseMana;
    }

}

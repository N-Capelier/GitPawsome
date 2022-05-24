using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class NurceButton : MonoBehaviour
{
    public Image Me;
    public int id;
    public Slider HpBar;
    public Slider TimeBar;
    public TextMeshProUGUI Hp;
    public bool StopProduce = false;
    public bool dead = false;
    public int TimeBeforeAlive;
    public int TimeBeforeRecover;

    public float StartTime;
    public int health, maxHealth;
    private void Start()
    {
        HpBar.value = (health * 100) / maxHealth;
        if(dead)
        {
            Hp.text = "DEAD";
            return;
        }
        Hp.text = health.ToString() + " / " + maxHealth.ToString();
    }

    public void OnClick()
    {
        BuildingManager.Instance.InfirmaryDontWannaUseCat(id);

    }

    void Update()
    {
        if(dead)
        {
            if (Time.time - StartTime > TimeBeforeAlive)
            {
                Me.sprite = BuildingManager.Instance.InfirmaryBuilding.myInfirmary[id].CatInInfirmary.CatSprite;
                BuildingManager.Instance.InfirmaryBuilding.GetComponent<Infirmary>().myInfirmary[id].CatInInfirmary.Dead = false;
                dead = false;
                Hp.text = health.ToString() + " / " + maxHealth.ToString();
                StartTime = Time.time;
            }

            TimeBar.value = ((Time.time - StartTime) * 100) / (StartTime + TimeBeforeRecover);
        }
        if (StopProduce) return;
        if (Time.time - StartTime > TimeBeforeRecover)
        {
            health += BuildingManager.Instance.InfirmaryBuilding.HpRecover;
            BuildingManager.Instance.InfirmaryBuilding.GetComponent<Infirmary>().myInfirmary[id].CatInInfirmary.Heal(BuildingManager.Instance.InfirmaryBuilding.HpRecover);

            if (health > maxHealth)
            {
                health = maxHealth;
                StopProduce = true;
            }

            Hp.text = health.ToString() + " / " + maxHealth.ToString();
            HpBar.value = (health * 100) / maxHealth;
            StartTime = Time.time;
        }
        TimeBar.value = ((Time.time - StartTime)*100) / (StartTime + TimeBeforeRecover);
        
        
    }
}

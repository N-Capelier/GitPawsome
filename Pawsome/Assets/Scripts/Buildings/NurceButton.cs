using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NurceButton : MonoBehaviour
{
    
    public int id;
    public Slider HpBar;
    public Slider TimeBar;
    public TextMeshProUGUI Hp;
    public bool StopProduce = false;

    public float StartTime;
    public int health, maxHealth;
    private void Start()
    {

        Hp.text = health.ToString() + " / " + maxHealth.ToString();
        HpBar.value = (health * 100) / maxHealth;
    }

    public void OnClick()
    {
        BuildingManager.Instance.InfirmaryDontWannaUseCat(id);

    }
    public void GiveMyData()
    {
        BuildingManager.Instance.GiveInfoToInfirmary(id, StartTime, health);
    }

    void Update()
    {
        if (StopProduce) return;
        if (Time.time - StartTime > 60)
            {
                StartTime = Time.time;
                health += BuildingManager.Instance.InfirmaryBuilding.HpRecoverPerMinute;
            if (health > maxHealth)
            {
                health = maxHealth;
                StopProduce = true;
            }

            Hp.text = health.ToString() + " / " + maxHealth.ToString();
            HpBar.value = (health * 100) / maxHealth;

            TimeBar.value = ((StartTime + 60f)*100) /Time.time ;
        }
        
    }
}

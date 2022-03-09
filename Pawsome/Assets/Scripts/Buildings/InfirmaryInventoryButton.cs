using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InfirmaryInventoryButton : MonoBehaviour
{
    
    public int id;
    public Slider HpBar;
    public TextMeshProUGUI Hp;
    
    public void OnClick()
    {
        Debug.Log("clik");
    }
    /*
    private void Start()
    {
        ProductionTime = BuildingManager.Instance.GetProductionTime(id);
        ProgressBar.gameObject.SetActive(false);
    }
    public void GenerateMySelf()
    {
        if (!IsProducing)
        {
            if(BuildingManager.Instance.TryProduce(id))
            {
                ProgressBar.gameObject.SetActive(true);
                IsProducing = true;
                ProgressBar.value = 0;
                StartTime = Time.time;
            }
        }
    }

    private void Update()
    {
        if(!IsProducing)
        {
            return;
        }
        if((Time.time - StartTime) > ProductionTime)
        {
            IsProducing = false;
            BuildingManager.Instance.ProductionDone(id);
            ProgressBar.gameObject.SetActive(false);
            return;
        }
        ProgressBar.value = (float)((100 * (Time.time - StartTime)) / ProductionTime);
    }*/

}

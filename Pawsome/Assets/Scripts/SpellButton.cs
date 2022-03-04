using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    public int id;
    public Slider ProgressBar;
    bool IsProducing = false;
    private float StartTime;
    private double ProductionTime;

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
    }

}

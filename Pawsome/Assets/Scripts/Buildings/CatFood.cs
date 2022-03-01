using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFood : MonoBehaviour
{
    public int CreationTime;
    public int FoodsPerCollection;
    public bool CanCollect;
    public float LastTimeCollect;
   

    // Update is called once per frame
    void Update()
    {
        if(!CanCollect)
        {
            CanCollect = (Time.time - LastTimeCollect) > CreationTime;
            return;
        }
        CanCollect = false;
        LastTimeCollect = Time.time;
        PlayerManager.Instance.IncrementCatFood(FoodsPerCollection);
        GameManager.Instance.UpdateUI();
    }


}

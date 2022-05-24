using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class InfirmiaryInformations
{
    public InstaCat CatInInfirmary;
    public float StartTime;
    public int idInBag;
    public InfirmiaryInformations(InstaCat theCat, float RecoverStartTime, int id)
    {
        this.CatInInfirmary = theCat;
        this.StartTime = RecoverStartTime;
        this.idInBag = id;
    }
}

/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class Infirmary : MonoBehaviour
{
    public int RecoverPrice;
    public int RevivePrice;
    public int TimeBeforeAlive;
    public int TimeBeforeRecover;
    public int HpRecover;
    public int catBonusTime;
    public bool CatIn = false;
    public List<InfirmiaryInformations> myInfirmary = new List<InfirmiaryInformations>();

    public void AddCatToInfirmary(InstaCat catTemp, int id)
    {
        myInfirmary.Add(new InfirmiaryInformations(catTemp, Time.time, id));
    }

    public void OutCatToInfirmary(int i)
    {
        myInfirmary.Remove(myInfirmary[i]);
    }
    
}


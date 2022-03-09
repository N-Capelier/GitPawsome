using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


public class Infirmary : MonoBehaviour
{
    public int HpRecoverPerMinute;
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


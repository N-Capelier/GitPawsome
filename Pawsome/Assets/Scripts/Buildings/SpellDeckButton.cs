using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDeckButton : MonoBehaviour
{
    public int idInBag;

    
    public void OnClick()

    {
        BuildingManager.Instance.DontWannaUseSpell(idInBag);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class Invocation : MonoBehaviour
{
    [Header("Price")]
    public int FirstPrice;
    public int SecondPrice;
    public int ThirdPrice;

    public bool TryBuy(int choice)
    {
        switch (choice)
        {
            case 1:
                return PlayerManager.Instance.CatBell > FirstPrice;

            case 2:
                return PlayerManager.Instance.CatBell > SecondPrice;

            case 3:
                return PlayerManager.Instance.CatBell > ThirdPrice;

        }
        return false;
    }
}

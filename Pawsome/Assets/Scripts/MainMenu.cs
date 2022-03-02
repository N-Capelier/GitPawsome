using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject CatFoodMenu;
    public TextMeshProUGUI NameInput;

    public void Play()
    {
        SceneManagement.instance.LoadScene(1);
    }
    public void Exit()
    {
        SceneManagement.instance.Quit();
    }
    public void Options()
    {
        OptionsMenu.SetActive(true);
    }
    public void ExitOptions()
    {
        OptionsMenu.SetActive(false);
    }
    public void ChangeName()
    {
        if (NameInput.text != "") PlayerManager.Instance.PlayerName = NameInput.text;
    }

    public void CatFoodUI()
    {
        CatFoodMenu.SetActive(true);
    }
    public void ExitCatFood()
    {
        CatFoodMenu.SetActive(false);
    }
    public void UpgradeCatFood()
    {
        if (CatFood.Instance.TryUpgrad()) CatFood.Instance.Upgraded();
    }
}

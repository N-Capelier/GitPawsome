using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public TextMeshProUGUI NameInput;

    public void Play()
    {
        Debug.Log("fuesigfiesf");

        AudioManager.instance.StopPlaying("MainThemeMusic");
        AudioManager.instance.StopPlaying("LobbyMusic");
        AudioManager.instance.Play("LobbyMusic");

        SceneManagement.instance.LoadScene("Lounge");
    }
    public void Play2()
    {
        AudioManager.instance.StopPlaying("MainThemeMusic");
        AudioManager.instance.StopPlaying("LobbyMusic");
        AudioManager.instance.Play("BattleMusic");

        SceneManagement.instance.LoadScene("Nico");
    }
    public void ReturnToMenu()
    {
        AudioManager.instance.StopPlaying("BattleMusic");
        AudioManager.instance.StopPlaying("LobbyMusic");
        AudioManager.instance.Play("LobbyMusic");
        SceneManagement.instance.LoadScene("MainMenu");
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

    
}

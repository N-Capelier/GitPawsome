using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;
    private AsyncOperation sceneLoading;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            return;
        }
        else { Destroy(gameObject); }
    }

    public void LoadScene(int buildIndex)
    {
        sceneLoading = SceneManager.LoadSceneAsync(buildIndex);
    }
    public void LoadScene(string sceneName)
    {
        sceneLoading = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(WaitToLoad());
    }

    IEnumerator WaitToLoad()
    {
        sceneLoading.allowSceneActivation = false;
        yield return new WaitUntil(() => sceneLoading.progress > 0.89);
        sceneLoading.allowSceneActivation = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator fade;

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        StartCoroutine(LevelChangeWait(sceneName));
    }

    public void ChangeScene(int id)
    {
        Time.timeScale = 1f;
        StartCoroutine(LevelChangeWait(id));
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    IEnumerator LevelChangeWait(string sceneName, float time = 1f)
    {
        fade.Play("Show");
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerator LevelChangeWait(int sceneName, float time = 1f)
    {
        fade.Play("Show");
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(sceneName);
    }
}

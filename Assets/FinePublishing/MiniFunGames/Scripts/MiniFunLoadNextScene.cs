using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MiniFunLoadNextScene : MonoBehaviour
{
    [SerializeField]
    int sceneId = 2;
    [SerializeField]
    UnityEvent onStartLoad = new UnityEvent();

    public static bool FinishedLoading
    {
        get
        {
            return finishedLoading;
        }
        private set
        {
            finishedLoading = value;
        }
    }

    static bool finishedLoading = true;

    [SerializeField]
    float delay = 2.0f;
    [SerializeField]

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        ///
        FinishedLoading = false;

        ///
        yield return new WaitForSeconds(delay);

        ///
        var sceneLoader = SceneManager.LoadSceneAsync(sceneId);
        sceneLoader.allowSceneActivation = false;
        sceneLoader.completed += SceneLoader_completed;

        ///
        onStartLoad.Invoke();

        ///
        while (sceneLoader.progress < 0.9f)
        {
            yield return null;
        };

        ///
        sceneLoader.allowSceneActivation = true;
    }

    private void SceneLoader_completed(AsyncOperation obj)
    {
        FinishedLoading = true;
    }
}

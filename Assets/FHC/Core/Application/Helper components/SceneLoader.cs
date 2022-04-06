using UnityEngine;
using System.Collections;
using FH.Core.Architecture;
using UnityEngine.SceneManagement;

namespace FH.Core.HelperComponent
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        string sceneName = "";
        [SerializeField]
        LoadSceneMode loadSceneMode = LoadSceneMode.Single;

        [Header("Async loading")]
        [SerializeField]
        bool async = false;
        [SerializeField]
        float mininumSyncLoadingTime = 0;

        public void Load()
        {
            if (async)
            {
                StartCoroutine(LoadSceneAsync());
            }
            else
            {
                SceneManager.LoadScene(sceneName, loadSceneMode);
            }
        }

        IEnumerator LoadSceneAsync()
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            if (mininumSyncLoadingTime == 0)
            {
                asyncOperation.allowSceneActivation = true;
            }
            else
            {
                asyncOperation.allowSceneActivation = false;
                float timeTracking = 0;
                while (timeTracking < mininumSyncLoadingTime)
                {
                    timeTracking += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }

    }

}
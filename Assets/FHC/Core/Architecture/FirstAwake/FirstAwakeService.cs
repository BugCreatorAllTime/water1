using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace FH.Core.Architecture
{
    public class FirstAwakeService : MonoBehaviour
    {
        void Awake()
        {
            var allTransforms = FindObjectsOfType<Transform>();
            foreach (Transform tf in allTransforms)
            {
                if (tf.gameObject.scene != gameObject.scene)
                {
                    continue;
                }

                var firstAwakeComponents = tf.GetComponents<IFirstWakeComponent>();
                foreach (var firstAwakeComponent in firstAwakeComponents)
                {
                    if (!firstAwakeComponent.Awoke)
                    {
                        firstAwakeComponent.FirstAwake();
                        firstAwakeComponent.Awoke = true;
                    }
                }
            }

        }
    }

}
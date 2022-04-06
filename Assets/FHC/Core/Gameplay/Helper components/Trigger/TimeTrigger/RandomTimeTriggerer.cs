using UnityEngine;
using FH.Core.Architecture;

namespace FH.Core.Gameplay.HelperComponent
{
    public class RandomTimeTriggerer : MonoBehaviour
    {
        [SerializeField]
        float minTimeInterval;
        [SerializeField]
        float maxTimeInterval;
        [SerializeField]
        float startTimeOffset = 0;
        [SerializeField]
        bool unscaledTime = false;
        [SerializeField]
        bool nextIntervalManually = false;

        [SerializeField]
        OrderedEventDispatcher onTrigger = new OrderedEventDispatcher();

        float timeTracker;
        float currentInterval;
        bool counting = true;

        public void OnEnable()
        {
            StartNewCounter(startTimeOffset);
        }

        public void StartNewCounter(float timeOffset = 0)
        {
            counting = true;
            timeTracker = 0;
            currentInterval = GetRandomInterval() + timeOffset;
        }

        public void Update()
        {
            if (!counting)
            {
                return;
            }

            timeTracker += unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            if (timeTracker >= currentInterval)
            {
                counting = false;
                onTrigger.Dispatch();

                if (!nextIntervalManually)
                {
                    StartNewCounter();
                }
            }
        }

        float GetRandomInterval()
        {
            return Random.Range(minTimeInterval, maxTimeInterval);
        }
    }

}
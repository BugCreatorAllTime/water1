using UnityEngine;
using System.Collections;
using FH.Core.Architecture;
using UnityEngine.Serialization;

namespace FH.Core.Gameplay.HelperComponent
{
    public class TimeTrigger : MonoBehaviour
    {
        [SerializeField]
        float countDownTime = 2;
        [SerializeField]
        bool unscaledTime = false;

        [Space]
        [SerializeField, FormerlySerializedAs("timeOutEvenent")]
        OrderedEventDispatcher timeOutEvent = new OrderedEventDispatcher();

        float currentTime;

        public float CountDownTime
        {
            get
            {
                return countDownTime;
            }

            set
            {
                countDownTime = value;
            }
        }

        bool counting = false;

        void Awake()
        {
            if (!counting)
            {
                enabled = false;
            }
        }

        public void TimeOut()
        {
            currentTime = 0;
            Update();
        }

        public void TriggerTimeOutEvent()
        {
            timeOutEvent.Dispatch();
        }

        public void StopCountDown()
        {
            counting = false;
            enabled = false;
        }

        void Update()
        {
            ///
            if (!counting)
            {
                return;
            }

            ///
            currentTime -= unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            if (currentTime <= 0)
            {
                timeOutEvent.Dispatch();
                enabled = false;
            }
        }

        public void StartCountDown()
        {
            counting = true;
            enabled = true;
            currentTime = CountDownTime;
        }

        public void StartCountDownDelay(float delay)
        {
            Invoke("StartCountDown", delay);
        }

        public void OnDisable()
        {
            counting = false;
        }
    }

}
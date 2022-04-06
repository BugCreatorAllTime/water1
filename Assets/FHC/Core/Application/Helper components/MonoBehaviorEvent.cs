using UnityEngine;
using FH.Core.Architecture;

namespace FH.Core.HelperComponent
{
    [DisallowMultipleComponent]
    public class MonoBehaviorEvent : MonoBehaviour
    {

        [Header("Activation")]
        [SerializeField]
        OrderedEventDispatcher onAwake = new OrderedEventDispatcher();
        [SerializeField]
        OrderedEventDispatcher onEnable = new OrderedEventDispatcher();
        [SerializeField]
        OrderedEventDispatcher onStart = new OrderedEventDispatcher();
        [SerializeField]
        OrderedEventDispatcher onDisable = new OrderedEventDispatcher();

        [Space]
        [SerializeField]
        private OrderedEventDispatcher onParticleCollision;

        //[Header("Others")]
        //[SerializeField]
        //OrderedEventDispatcher onSceneLoaded = new OrderedEventDispatcher();

        public void Destroy()
        {
            Destroy(gameObject);
        }

        void Awake()
        {
            onAwake.Dispatch();
        }

        void OnEnable()
        {
            onEnable.Dispatch();
        }

        void Start()
        {
            onStart.Dispatch();
        }

        void OnDisable()
        {
            onDisable.Dispatch();
        }

        private void OnParticleCollision(GameObject other)
        {
            onParticleCollision?.Dispatch();
        }
    }

}
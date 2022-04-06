using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    public class AnimatorSpeedSetter : MonoBehaviour
    {
        [SerializeField]
        Animator animator;
        [SerializeField]
        float speed = 1;
        [SerializeField]
        bool setAtAwake = false;

        public void Awake()
        {
            if (setAtAwake)
            {
                Set();
            }
        }

        [ContextMenu("Set")]
        public void Set()
        {
            animator.speed = speed;
        }

        public void Reset()
        {
            animator = GetComponent<Animator>();
        }
    }

}
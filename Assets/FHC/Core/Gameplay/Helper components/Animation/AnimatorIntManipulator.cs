using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Core.Gameplay.HelperComponent
{
    public class AnimatorIntManipulator : MonoBehaviour
    {
        [SerializeField]
        Animator animator;
        [SerializeField]
        string intName;

        public void SetValue(int value)
        {
            animator.SetInteger(intName, value);
        }

        public void ChangeValue(int amount)
        {
            var newValue = animator.GetInteger(intName) + amount;
            animator.SetInteger(intName, newValue);
        }
    }

}
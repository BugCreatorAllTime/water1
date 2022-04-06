using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture
{
    public abstract class FirstWakeComponent : MonoBehaviour, IFirstWakeComponent
    {
        bool awoke = false;

        public bool Awoke
        {
            get
            {
                return awoke;
            }

            set
            {
                awoke = value;
            }
        }

        public abstract void FirstAwake();
    }

}
using UnityEngine;
using System.Collections.Generic;
using FH.Core.Architecture.Pool;

namespace FH.Core.Gameplay.HelperComponent
{
    public class ComponentsActivationResetter : MonoBehaviour, IResetableObject
    {
        [SerializeField]
        Behaviour componentToAdd = null;
        [SerializeField]
        List<Behaviour> components = new List<Behaviour>();
        List<bool> state = new List<bool>();

        public void ResetToLastSavedState()
        {
            for (int i = 0; i < state.Count; i++)
            {
                components[i].enabled = state[i];
            }
        }

        public void SaveCurrentState()
        {
            state = new List<bool>();
            for (int i = 0; i < components.Count; i++)
            {
                try
                {
                    state.Add(components[i].enabled);
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e, this);
                    throw;
                }
            }
        }

        [ContextMenu("Get All Components")]
        void GetAllComponents()
        {
            Behaviour[] allComponents = GetComponentsInChildren<Behaviour>(true);
            components = new List<Behaviour>(allComponents);
            components.Remove(this);
        }

        [ContextMenu("Get All Components From Children Only")]
        void GetAllComponentsFromChildrenOnly()
        {
            components = new List<Behaviour>();
            for (int i = 0; i < transform.childCount; i++)
            {
                components.AddRange(transform.GetChild(i).GetComponentsInChildren<Behaviour>(true));
            }
        }

        void OnDrawGizmos()
        {
            if (componentToAdd != null)
            {
                AddComponentToList(componentToAdd);
                componentToAdd = null;
            }
        }

        void AddComponentToList(Behaviour component)
        {
            if (!components.Contains(component))
            {
                components.Add(component);
            }
        }

        [ContextMenu("Remove null elements")]
        void RemoveNoneElements()
        {
            for (int i = components.Count - 1; i >= 0; i--)
            {
                if (components[i] == null)
                {
                    components.RemoveAt(i);
                }
            }
        }
    }

}
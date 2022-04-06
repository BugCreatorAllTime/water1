using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FH.Core.Gameplay.HelperComponent
{
    public class ActionGroupInvoker : MonoBehaviour
    {
        [SerializeField]
        List<ActionGroup> actionGroups = new List<ActionGroup>();

        public void InvokeActionGroupByName(string groupName)
        {
            foreach (var item in actionGroups)
            {
                if (item.Name == groupName)
                {
                    item.Actions.Invoke();
                    break;
                }
            }
        }

        public void InvokeActionGroupById(int groupId)
        {
            actionGroups[groupId].Actions.Invoke();
        }

    }

}
#if UNITY_EDITOR
using FH;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

class FindReferencesUtility
{
    [MenuItem("CONTEXT/Component/Find references to this")]
    private static void FindReferences(MenuCommand data)
    {
        Object context = data.context;
        if (context)
        {
            var comp = context as Component;
            if (comp)
                FindReferencesTo(comp);
        }
    }

    [MenuItem("CONTEXT/Component/Find references to GameObject")]
    private static void FindReferencesToGameObject(MenuCommand data)
    {
        Object context = data.context;
        if (context)
        {
            var comp = context as Component;
            if (comp)
            {
                FindReferencesTo(comp.gameObject);
            }
        }
    }

    [MenuItem("Assets/Find references to this")]
    private static void FindReferencesToAsset(MenuCommand data)
    {
        var selected = Selection.activeObject;
        if (selected)
            FindReferencesTo(selected);
    }

    private static void FindReferencesTo(Object to)
    {
        var referencedBy = new List<Object>();
        var allObjects = GetAllGameObjects();
        for (int j = 0; j < allObjects.Length; j++)
        {
            var go = allObjects[j];

            if (PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
            {
                if (PrefabUtility.GetCorrespondingObjectFromSource(go) == to)
                {
                    FHLog.Log(string.Format("referenced by {0}, {1}", go.name, go.GetType()), go);
                    referencedBy.Add(go);
                }
            }

            var components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                var c = components[i];
                if (!c) continue;

                var so = new SerializedObject(c);
                var sp = so.GetIterator();

                while (sp.NextVisible(true))
                    if (sp.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        if (sp.objectReferenceValue == to)
                        {
                            FHLog.Log(string.Format("referenced by {0}, {1}", c.name, c.GetType()), c);
                            referencedBy.Add(c.gameObject);
                        }
                    }
            }
        }

        if (referencedBy.Any())
            Selection.objects = referencedBy.ToArray();
        else FHLog.Log("no references in scene");
    }

    static GameObject[] GetAllGameObjects()
    {
        var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        List<GameObject> allGameObjects = new List<GameObject>();
        foreach (var rootObject in roots)
        {
            var allTransforms = rootObject.GetComponentsInChildren<Transform>(true);
            foreach (var item in allTransforms)
            {
                allGameObjects.Add(item.gameObject);
            }
        }
        return allGameObjects.ToArray();
    }

}
#endif
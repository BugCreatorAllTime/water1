using FH.Core.Architecture.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPoolMemberSparker : MonoBehaviour
{
    [Space]
    [SerializeField]
    GeneralPoolMember prototype;

    [Space]
    [SerializeField]
    int maxActiveStars = 30;
    [SerializeField]
    int maxStars = 100;

    [Space]
    [SerializeField]
    Transform corner_1;
    [SerializeField]
    Transform corner_2;

    [Space]
    [SerializeField]
    float rate;

    bool inited = false;

    public void Awake()
    {
        TryInit();
    }

    private void TryInit()
    {
        ///
        if (inited)
        {
            return;
        }

        ///
        if (!Entry.Instance.GeneralPool.ContainsPrototype(prototype.PrototypeId))
        {
            Entry.Instance.GeneralPool.PushPrototype(prototype);
        }

        ///
        inited = true;
    }

    [ContextMenu("Spark")]
    public void Spark()
    {
        ///
        TryInit();

        ///
        StopAllCoroutines();

        ///
        StartCoroutine(SparkLoop());
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator SparkLoop()
    {
        ///
        float t = 0;

        ///
        var activeStarsCount = Entry.Instance.GeneralPool.GetActiveMembersCountValueHandle(prototype.PrototypeId);

        ///
        while (true)
        {
            ///
            t += Time.deltaTime;

            ///
            int count = (int)Mathf.Floor(t * rate);
            t -= count / rate;
            ///
            for (int i = 0; i < count; i++)
            {
                ///
                if (activeStarsCount.Value >= maxActiveStars)
                {
                    break;
                }

                ///
                SpawnRandom();
            }

            ///
            yield return null;
        }
    }

    void SpawnRandom()
    {
        ///
        var instance = Entry.Instance.GeneralPool.TakeInstance(prototype.PrototypeId, true);
        instance.transform.position = GetRandomPosition();

        ///
        instance.gameObject.SetActive(true);
    }

    Vector3 GetRandomPosition()
    {
        ///
        var pos1 = corner_1.position;
        var pos2 = corner_2.position;

        ///
        var minX = Mathf.Min(pos1.x, pos2.x);
        var maxX = Mathf.Max(pos1.x, pos2.x);
        var minY = Mathf.Min(pos1.y, pos2.y);
        var maxY = Mathf.Max(pos1.y, pos2.y);

        ///
        var x = Random.Range(minX, maxX);
        var y = Random.Range(minY, maxY);

        ///
        return new Vector3(x, y, pos1.z);
    }
}

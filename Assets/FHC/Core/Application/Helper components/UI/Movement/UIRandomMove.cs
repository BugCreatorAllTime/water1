using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    public class UIRandomMove : OutsiteTargetRectTransform
    {
        [SerializeField]
        Vector2 minPosition;
        [SerializeField]
        Vector2 maxPosition;
        [SerializeField]
        float speed;

        [SerializeField]
        bool deltaMoving = true;

        Vector2 stablePosition;

        Vector2 currentDestination;

        public void Awake()
        {
            GetStablePosition();
            currentDestination = GetRandomPosition();
        }

        public void OnEnable()
        {
            GetStablePosition();
        }

        void GetStablePosition()
        {
            if (deltaMoving)
            {
                stablePosition = TargetRectTransform.anchoredPosition;
            }
        }

        Vector2 GetRandomPosition()
        {
            if (deltaMoving)
            {
                return new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y)) + stablePosition;
            }
            else
            {
                return new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
            }
        }

        public void Update()
        {
            Vector2 currentPosition = TargetRectTransform.anchoredPosition;

            if (currentDestination == currentPosition)
            {
                currentDestination = GetRandomPosition();
            }

            TargetRectTransform.anchoredPosition = Vector2.MoveTowards(currentPosition, currentDestination, speed * Time.deltaTime);
        }

    }

}
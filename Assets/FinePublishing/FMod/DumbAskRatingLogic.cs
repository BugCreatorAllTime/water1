using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class DumbAskRatingLogic : AskRatingLogic
    {
        [SerializeField]
        List<int> askRatingThresholds = new List<int>();

        public override bool ShouldAskRating(int thresholdId, float gamesCount)
        {
            if (thresholdId >= askRatingThresholds.Count)
            {
                return false;
            }

            if (Mathf.Ceil(gamesCount) != askRatingThresholds[thresholdId])
            {
                return false;
            }

            return true;
        }
    }

}
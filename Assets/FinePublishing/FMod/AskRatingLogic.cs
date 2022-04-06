using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public abstract class AskRatingLogic : MonoBehaviour
    {
        public abstract bool ShouldAskRating(int thresholdId, float gamesCount);

    }

}
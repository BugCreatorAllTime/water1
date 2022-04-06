using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FMod
{
    public class GameOverActionController : MonoBehaviour
    {
        public const string GamesCountKey = "GamesCountKey";
        public const string NextAskRatingThresholdIdKey = "NextAskRatingThresholdKey";
        public const string IsRatedKey = "IsRatedKey";

        [SerializeField]
        float gameRestartCountIncrement = 1.0f / 5.0f;
        [SerializeField]
        AskRatingLogic askRatingLogic;

        [Header("Events")]
        [SerializeField]
        UnityEvent onShouldShowAds = new UnityEvent();
        [SerializeField]
        UnityEvent onShouldShowAskRating = new UnityEvent();

        bool isInited = false;

        float gamesCount;
        int nextAskRatingThresholdId;
        bool isRated;

        static bool gameRestartedWithLoadingScene = false;

        public void Start()
        {
            if (gameRestartedWithLoadingScene)
            {
                gameRestartedWithLoadingScene = false;
                DoAction(gameRestartCountIncrement);
            }
        }

        [System.Obsolete]
        public void GameRestaredWithLoadingScene()
        {           
            gameRestartedWithLoadingScene = true;
        }

        public void GameRestaredWithoutLoadingScene()
        {
           
            DoAction(gameRestartCountIncrement);
        }

        public void GameOvered()
        {
            ///
            DoAction(1); 
        }

        private void DoAction(float gamesCountIncrement)
        {
            ///
            LoadState();

            ///
            gamesCount += gamesCountIncrement;

            ///
            if (ShouldAskRating())
            {
                nextAskRatingThresholdId++;
                if (!NativeReviewRequest.RequestReview())
                {                  
                    onShouldShowAskRating.Invoke();
                }
                else
                {
                   
                }
            }
            else
            {
                onShouldShowAds.Invoke();
            }

#if UNITY_EDITOR
            Debug.LogFormat("Game Overed Counts: {0}", gamesCount);
#endif

            ///
            SaveState();
        }

        public void SetRated()
        {            
            isRated = true;
            SaveState();
        }

        void LoadState()
        {
            gamesCount = PlayerPrefs.GetFloat(GamesCountKey, 0);
            nextAskRatingThresholdId = PlayerPrefs.GetInt(NextAskRatingThresholdIdKey, 0);
            isRated = PlayerPrefs.GetInt(IsRatedKey, 0) == 1;
        }

        void SaveState()
        {
            PlayerPrefs.SetFloat(GamesCountKey, gamesCount);
            PlayerPrefs.SetInt(NextAskRatingThresholdIdKey, nextAskRatingThresholdId);
            PlayerPrefs.SetInt(IsRatedKey, isRated ? 1 : 0);

            PlayerPrefs.Save();
        }

        bool ShouldAskRating()
        {
#if UNITY_STANDALONE
            return false;
#endif
            ///
            if (isRated)
            {
                return false;
            }

            ///
            return askRatingLogic.ShouldAskRating(nextAskRatingThresholdId, gamesCount);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class ChangeRandomCharacter : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> characters = new List<GameObject>();

        public void OnEnable()
        {
            SetRandomCharacter();
        }

        public void SetRandomCharacter()
        {
            HideAllCharacters();
            ShowRandomCharacter();
        }

        void HideAllCharacters()
        {
            foreach (var item in characters)
            {
                item.SetActive(false);
            }
        }

        void ShowRandomCharacter()
        {
            int i = Random.Range(0, characters.Count);
            characters[i].SetActive(true);
        }
    }

}
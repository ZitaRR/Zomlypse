using UnityEngine;
using Zomlypse.Enums;

namespace Zomlypse.Behaviours
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Entity Player { get; set; }

        private CardDeck deck;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        private void Start()
        {
            SceneLoader.OnActivation += SceneActivation;
            SceneLoader.OnDeactivation += SceneDeactivation;
        }

        private void SceneActivation(string scene, SceneState state)
        {
            if (state == SceneState.Active)
            {
                deck = GameObject.Find("Deck").GetComponent<CardDeck>();
                deck.AddCard(Player);
            }
        }

        private void SceneDeactivation(string scene, SceneState state)
        {

        }

        public void ChangeScene(string sceneName)
        {
            SceneLoader.LoadScene(sceneName);
        }
    }
}

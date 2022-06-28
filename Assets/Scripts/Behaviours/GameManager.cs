using System;
using UnityEngine;
using Zomlypse.Enums;
using Zomlypse.States;

namespace Zomlypse.Behaviours
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static event Action OnFrame;

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
            StateMachine.SetState<MenuState>();

            SceneLoader.OnActivation += SceneActivation;
            SceneLoader.OnDeactivation += SceneDeactivation;
        }

        private void Update()
        {
            OnFrame?.Invoke();
        }

        private void SceneActivation(string scene, SceneState state)
        {
            if (state == SceneState.Active)
            {
                StateMachine.SetState<PlayState>();
                deck = GameObject.Find("Deck").GetComponent<CardDeck>();
                StartCoroutine(deck.AddCard(Player));
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

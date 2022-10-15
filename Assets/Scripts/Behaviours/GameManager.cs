using System;
using UnityEngine;
using Zomlypse.Enums;
using Zomlypse.IO;
using Zomlypse.States;
using Zomlypse.Singleton;

namespace Zomlypse.Behaviours
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static event Action OnFrame;

        public Entity Player { get; set; }
        public Notifications Notifications { get => notifications; }
        public CardDeck Deck { get => deck; }
        public UI UserInterface { get => userInterface; }
        public TextLinker Linker { get => linker; }
        public EntityManager Entities { get => entities; }

        [SerializeField]
        private Notifications notifications;
        [SerializeField]
        private CardDeck deck;
        [SerializeField]
        private UI userInterface;
        [SerializeField]
        private TextLinker linker;
        [SerializeField]
        private EntityManager entities;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(Instance);

            FileManager.Initialize();
            UserInterface.Initialize(this);
            Entities.Initialize(this);
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

                Deck.Add(Player);
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

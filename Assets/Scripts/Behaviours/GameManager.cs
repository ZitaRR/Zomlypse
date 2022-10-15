using System;
using UnityEngine;
using Zomlypse.Enums;
using Zomlypse.IO;
using Zomlypse.States;
using Zomlypse.Behaviours;

namespace Zomlypse.Behaviours
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static event Action OnFrame;

        public Entity Player { get; set; }
        public Notifications Notifications { get => notifications; }
        public CardDeck Deck { get => deck; }

        [SerializeField]
        private Notifications notifications;
        [SerializeField]
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

            FileManager.Initialize();

            Transform ui = GameObject.FindGameObjectWithTag("UI").transform;
            Component[] components = new Component[ui.childCount];

            for (int i = 0; i < components.Length; i++)
            {
                components[i] = ui.GetChild(i);
            }


            UI.Initialize(components);
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

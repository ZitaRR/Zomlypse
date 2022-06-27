using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Zomlypse.Enums;

namespace Zomlypse.Behaviours
{
    public class SceneLoader : MonoBehaviour
    {
        private const string LOADING_SCENE = "LoadingScene";
        private const string MENU_PREFIX = "Menu_";
        private const string PLAY_PREFIX = "Play_";

        public static string Current { get; private set; }
        public static SceneState State { get; private set; }

        public static event Action<string, SceneState> OnActivation;
        public static event Action<string, SceneState> OnDeactivation;
        private static string targetScene;
        private static string previousScene;
        private static SceneState previousState;

        [Header("Settings")]
        [SerializeField]
        private float fadeDuration;

        [Header("UI")]
        [SerializeField]
        private Slider loadingBar;
        [SerializeField]
        private TextMeshProUGUI status;

        private CanvasGroup canvas;

        static SceneLoader()
        {
            Current = SceneManager.GetActiveScene().name;
            State = GetStateFromSceneName(Current);
        }

        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvas.alpha = 0f;

            StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            yield return StartCoroutine(SetAlpha(1f, fadeDuration));
            AsyncOperation loading = SceneManager.LoadSceneAsync(targetScene);
            loading.allowSceneActivation = false;
            loading.completed += (_) =>
            {
                Scene scene = SceneManager.GetSceneByName(targetScene);
                SceneManager.SetActiveScene(scene);

                Current = targetScene;
                State = GetStateFromSceneName(Current);
                OnActivation?.Invoke(Current, State);
            };

            while (!loading.isDone)
            {
                float progress = (loading.progress + .1f);
                loadingBar.value = progress;
                if (progress >= 1f)
                {
                    break;
                }
                yield return null;
            }

            yield return StartCoroutine(Unload(loading));
        }

        private IEnumerator Unload(AsyncOperation loading)
        {
            yield return StartCoroutine(SetAlpha(0f, fadeDuration));
            loading.allowSceneActivation = true;
        }

        private IEnumerator SetAlpha(float alpha, float duration)
        {
            float start = canvas.alpha;
            float time = 0;

            while (time < duration)
            {
                canvas.alpha = Mathf.Lerp(start, alpha, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            canvas.alpha = alpha;
        }

        public static SceneState GetStateFromSceneName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} cannot be empty or null");
            }

            int index = name.IndexOf('_') + 1;
            if (index <= -1)
            {
                throw new ArgumentException("Invalid scene name");
            }

            string prefix = name.Substring(0, index);
            switch (prefix)
            {
                case LOADING_SCENE:
                    return SceneState.Loading;
                case MENU_PREFIX:
                    return SceneState.Menu;
                case PLAY_PREFIX:
                    return SceneState.Active;
                default:
                    throw new ArgumentException("Invalid scene name");
            }
        }

        public static void LoadScene(string sceneName)
        {
            targetScene = sceneName;
            previousScene = Current;
            previousState = State;
            Current = LOADING_SCENE;
            State = SceneState.Loading;

            OnDeactivation?.Invoke(previousScene, previousState);
            SceneManager.LoadScene(LOADING_SCENE);
        }
    }
}

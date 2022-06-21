using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Zomlypse.Behaviours
{
    public class SceneLoader : MonoBehaviour
    {
        private const string LOADING_SCENE = "LoadingScene";

        public static string Current { get; private set; }

        public static event Action<string> OnActivation;
        public static event Action<string> OnDeactivation;

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
            AsyncOperation loading = SceneManager.LoadSceneAsync(Current);
            loading.allowSceneActivation = false;

            while (!loading.isDone)
            {
                float progress = (loading.progress + .1f);
                loadingBar.value = progress;
                if (progress >= 1)
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
            OnActivation?.Invoke(Current);
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

        public static void LoadScene(string sceneName)
        {
            OnDeactivation?.Invoke(Current);
            Current = sceneName;
            SceneManager.LoadScene(LOADING_SCENE);
        }
    }
}

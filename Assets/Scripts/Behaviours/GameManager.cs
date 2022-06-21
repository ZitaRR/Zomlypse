using UnityEngine;

namespace Zomlypse.Behaviours
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

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

        public void ChangeScene(string sceneName)
        {
            SceneLoader.LoadScene(sceneName);
        }
    }
}

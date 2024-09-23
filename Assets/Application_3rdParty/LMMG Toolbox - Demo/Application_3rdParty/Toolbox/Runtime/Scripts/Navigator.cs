namespace Toolbox.Runtime.Scripts
{
    using UnityEngine.SceneManagement;
    
    public static class Navigator
    {
        #region Methods
        
        #region Public Methods

        /// <summary>
        /// Loads a scene.
        /// </summary>
        /// <param name="sceneIndex"> The index of the scene to load </param>
        /// <param name="loadSceneMode"> The load scene mode to use </param>
        public static void Navigate(int sceneIndex, LoadSceneMode loadSceneMode)
        {
            SceneManager.LoadScene(sceneIndex, loadSceneMode);

            if (loadSceneMode != LoadSceneMode.Single) return;
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex != sceneIndex)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }

        /// <summary>
        /// Loads a scene.
        /// </summary>
        /// <param name="sceneName"> The name of the scene to load </param>
        /// <param name="loadSceneMode"> The load scene mode to use </param>
        public static void Navigate(string sceneName, LoadSceneMode loadSceneMode)
        {
            SceneManager.LoadScene(sceneName, loadSceneMode);
            
            if (loadSceneMode != LoadSceneMode.Single) return;
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name != sceneName)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }

        #endregion

        #endregion
    }
}
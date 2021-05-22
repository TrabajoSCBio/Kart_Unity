using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Header("ScriptableObjects")]
        public ConfigurationRace Configuration; 
        [Tooltip("You want to return menu or return map")]
        public TMPro.TextMeshProUGUI text;
        public bool returnMenu;
        private void Awake() {
            text.text = "¡Has quedado " + Configuration.rankPlayer + "º!";
        }
        public void LoadTargetScene() 
        {
            if(returnMenu) 
            {
                SceneManager.LoadSceneAsync("IntroMenu");
            } else {
                SceneManager.LoadSceneAsync(Configuration.SceneRacing);
            }
        }
    }
}

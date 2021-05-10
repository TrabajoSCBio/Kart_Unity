using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroGameManager : MonoBehaviour
{
    public GameObject ChooseRacingPanel; 
    public GameObject ConfigurationPanel; 
    public GameObject OnePlayerPanel;
    public GameObject LaodGamePanel;
    public GameObject StartButton;
    public GameObject TextPressAnyButton;
    public Text textResolution;
    [HideInInspector]
    public string SceneRacing;

    private List<string> Resolutions = new List<string>();
    private int indexList;
    private void Awake() 
    {
        indexList = 1;
        Resolutions.Add("1920x1080");
        Resolutions.Add("1680x1050");
        Resolutions.Add("1600x1024");
        Resolutions.Add("1600x900");
        Resolutions.Add("1440x900");
        Resolutions.Add("1360x768");
        Resolutions.Add("1280x1024");
        Resolutions.Add("1280x960");
        Resolutions.Add("1280x800");
        Resolutions.Add("1280x720");
        Resolutions.Add("1024x768");
        Resolutions.Add("800x600");
    }
    public void SetSceneRacing(string scene) 
    {
        SceneRacing = scene;
    }
    public void LoadMainSceneRacing() 
    {
        SceneManager.LoadScene(SceneRacing);
    }
    public void BackPanel() 
    {
        if(ConfigurationPanel.activeSelf) {
            ChooseRacingPanel.SetActive(true);
            ConfigurationPanel.SetActive(false);
            StartButton.SetActive(false);
        } else {
            OnePlayerPanel.SetActive(false);
        }
    }
    public void ChangeVolume(Slider slide)
    {
        AudioListener.volume = slide.value;
    }
    public void ChangeResolution(bool RightOrLeft) 
    {
        int beforeIndexList;
        if(indexList >= 1 && indexList <= Resolutions.Count) 
        {
            beforeIndexList = indexList;
            indexList = (RightOrLeft) ? (indexList != Resolutions.Count ? ++indexList : indexList) 
            : (indexList != 1 ? --indexList : indexList);
            if(beforeIndexList != indexList)
                SetResolution(indexList);
        }
    }
    public void StartGame() 
    {
        StartCoroutine(LoadRacing());
    }
    public void ExitGame() 
    {
        Application.Quit();
    }
    private IEnumerator LoadRacing() 
    {
        AsyncOperation Load = SceneManager.LoadSceneAsync(SceneRacing);
        Load.allowSceneActivation = false;
        LaodGamePanel.SetActive(true);
        OnePlayerPanel.SetActive(false);
        float progress = 0;

        while(progress < 1) 
        {
            progress = Mathf.Clamp01(Load.progress/0.9f);
            yield return null;
        }
        TextPressAnyButton.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        Load.allowSceneActivation = true;
    }
    private void SetResolution(int index) 
    {
        textResolution.text = Resolutions[--index];
        switch(index) 
        {
            case 1:
            Screen.SetResolution(1920,1080,Screen.fullScreenMode);
            break;
            case 2:
            Screen.SetResolution(1680,1050,Screen.fullScreenMode);
            break;
            case 3:
            Screen.SetResolution(1600,1024,Screen.fullScreenMode);
            break;
            case 4:
            Screen.SetResolution(1600,900,Screen.fullScreenMode);
            break;
            case 5:
            Screen.SetResolution(1440,900,Screen.fullScreenMode);
            break;
            case 6:
            Screen.SetResolution(1360,768,Screen.fullScreenMode);
            break;
            case 7:
            Screen.SetResolution(1280,1024,Screen.fullScreenMode);
            break;
            case 8:
            Screen.SetResolution(1280,960,Screen.fullScreenMode);
            break;
            case 9:
            Screen.SetResolution(1280,800,Screen.fullScreenMode);
            break;
            case 10:
            Screen.SetResolution(1280,720,Screen.fullScreenMode);
            break;
            case 11:
            Screen.SetResolution(1024,768,Screen.fullScreenMode);
            break;
            case 12:
            Screen.SetResolution(800,600,Screen.fullScreenMode);
            break;
            default:
            break;
        }
    }
}

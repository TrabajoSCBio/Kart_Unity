using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class IntroGameManager : MonoBehaviour
{
    [Header("ScriptableObjects")]
    public ConfigurationRace configuration;
    [Header("Objetos de la escena")]
    public GameObject ChooseRacingPanel; 
    public GameObject ConfigurationPanel; 
    public GameObject OnePlayerPanel;
    public GameObject LaodGamePanel;
    public GameObject StartButton;
    public GameObject TextPressAnyButton;
    public GameObject TimePerLap;
    public GameObject ExtraTimePerLap;
    public GameObject KartMenu;
    public GameObject ConfigurationKartPanel;
    [Header("Elementos UI")]
    public Toggle IsTimed;
    public Toggle ActiveObjects;
    public Toggle Body;
    public Toggle Kart;
    public TextMeshProUGUI textStartButton;
    public TextMeshProUGUI textResolution;
    public TextMeshProUGUI textScreen;
    public TextMeshProUGUI NumberLaps;
    public TextMeshProUGUI NumberBots;
    public TextMeshProUGUI NumberTimePerLap;
    public TextMeshProUGUI NumberExtraTime;
    public TextMeshProUGUI NumberSpeed;
    [Header("Materials")]
    public Material BodyMaterial;
    public Material KartMaterial;
    [Header("Variables")]
    public int minLaps;
    public int maxLaps;
    public int minBots;
    public int maxBots;
    public int minTime;
    public int maxTime;
    public int minExtraTime;
    public int maxExtraTime;
    public int minSpeed;
    public int maxSpeed;
    [HideInInspector]
    public string SceneRacing;

    private List<string> Resolutions = new List<string>();
    private int indexListResolution;
    private int indexListScreen;
    private int laps;
    private int bots;
    private int timeLap;
    private int extraTime;
    private int speed;
    private void Awake() 
    {
        indexListResolution = 1;
        indexListScreen = 1;
        laps = minLaps;
        bots = minBots;
        timeLap = minTime;
        extraTime = minExtraTime;
        speed = minSpeed;
        NumberLaps.text = minLaps.ToString();
        NumberBots.text = minBots.ToString();
        NumberTimePerLap.text = minTime.ToString();
        NumberExtraTime.text = minExtraTime.ToString();
        NumberSpeed.text = minSpeed.ToString();
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
        IsTimed.onValueChanged.AddListener(OnIsTimedChanged);
        Body.onValueChanged.AddListener(OnSetBody);
        Kart.onValueChanged.AddListener(OnSetKart);
    }
    public void SetSceneRacing(string scene) 
    {
        SceneRacing = scene;
    }
    public void ChangeVolume(Slider slide)
    {
        AudioListener.volume = slide.value;
    }
    public void ChangeResolution(bool RightOrLeft) 
    {
        int beforeIndexList = indexListResolution;
        indexListResolution = AddOrSusNumber(indexListResolution, 1, Resolutions.Count, 1,RightOrLeft);
        if(beforeIndexList != indexListResolution)
            SetResolution(indexListResolution);
    }    
    public void ChangeScreen(bool RightOrLeft) 
    {
        int beforeIndexList = indexListScreen;
        indexListScreen = AddOrSusNumber(indexListScreen, 1, 3, 1,RightOrLeft);
        if(beforeIndexList != indexListScreen)
            SetScreenWindow(indexListScreen);
    }
    public void ChangeNumberLaps(bool RightOrLeft)
    {
        int beforeNumber = laps;
        laps = AddOrSusNumber(laps, minLaps, maxLaps, 1,RightOrLeft);
        if(beforeNumber != laps)
            NumberLaps.text = laps.ToString();
    }
    public void ChangeNumberBots(bool RightOrLeft)
    {
        int beforeNumber = bots;
        bots = AddOrSusNumber(bots, minBots, maxBots, 1,RightOrLeft);
        if(beforeNumber != bots)
            NumberBots.text = bots.ToString();
    }
    public void ChangeNumberTime(bool RightOrLeft)
    {
        int beforeNumber = timeLap;
        timeLap = AddOrSusNumber(timeLap, minTime, maxTime, 5,RightOrLeft);
        if(beforeNumber != timeLap)
            NumberTimePerLap.text = timeLap.ToString();
    }
    public void ChangeNumberExtraTime(bool RightOrLeft)
    {
        int beforeNumber = extraTime;
        extraTime = AddOrSusNumber(extraTime, minExtraTime, maxExtraTime, 5,RightOrLeft);
        if(beforeNumber != extraTime)
            NumberExtraTime.text = extraTime.ToString();
    }
    public void ChangeNumberSpeed(bool RightOrLeft)
    {
        int beforeNumber = speed;
        speed = AddOrSusNumber(speed, minSpeed, maxSpeed, 5,RightOrLeft);
        if(beforeNumber != speed)
            NumberSpeed.text = speed.ToString();
    }
    public void SaveColors(Image colorImage) 
    {
        BodyMaterial.color = Body.isOn ? colorImage.color : BodyMaterial.color;
        KartMaterial.color = Kart.isOn ? colorImage.color : KartMaterial.color;
    }
    public void BackPanel() 
    {
        if(ConfigurationPanel.activeSelf) {
            ConfigurationKartPanel.SetActive(true);
            ConfigurationPanel.SetActive(false);
            KartMenu.SetActive(true);
            textStartButton.text = "Siguiente";
        } else if(ConfigurationKartPanel.activeSelf){
            KartMenu.SetActive(false);
            ConfigurationKartPanel.SetActive(false);
            StartButton.SetActive(false);
            ChooseRacingPanel.SetActive(true);
        } else {
            OnePlayerPanel.SetActive(false);
        }
    }
    public void NextOrStartGame() 
    {
        if(ConfigurationPanel.activeSelf) 
        {
            SaveConfiguration();
            StartCoroutine(LoadRacing());
        } else {
            ConfigurationPanel.SetActive(true);
            ConfigurationKartPanel.SetActive(false);
            KartMenu.SetActive(false);
            textStartButton.text = "Empezar";
        }
    }
    public void ExitGame() 
    {
        Application.Quit();
    }
    private void SaveConfiguration() 
    {
        configuration.laps = laps;
        configuration.bots = bots;
        configuration.timeLap = timeLap;
        configuration.extraTime = extraTime;
        configuration.maxSpeed = (float)speed;
        configuration.isTimed = IsTimed.isOn;
        configuration.activeObjects = ActiveObjects.isOn;
        configuration.SceneRacing = SceneRacing;
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
    private void SetScreenWindow(int index)
    {
        switch(index) 
        {
            case 1:
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            textScreen.text = "Completa";
            break;
            case 2:
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
            textScreen.text = "Ventana Completa";
            break;
            case 3:
            Screen.fullScreenMode = FullScreenMode.Windowed;
            textScreen.text = "Ventana";
            break;
        }
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
    private void OnIsTimedChanged(bool state) 
    {
        if(state) 
        {
            TimePerLap.SetActive(true);
            ExtraTimePerLap.SetActive(true);
        } else {
            TimePerLap.SetActive(false);
            ExtraTimePerLap.SetActive(false);
        }
    }
    private void OnSetBody(bool state) 
    {
        if(Body.isOn && Kart.isOn)
            Kart.isOn = false;
        if(!Body.isOn && !Kart.isOn)
            Body.isOn = true;
    }
    private void OnSetKart(bool state) 
    {
        if(Body.isOn && Kart.isOn)
            Body.isOn = false;
        if(!Body.isOn && !Kart.isOn)
            Kart.isOn = true;
    }
    private int AddOrSusNumber(int currentNumber,int min, int max, int increment, bool RightOrLeft) 
    {
        int number = min;
        if(currentNumber >= min && currentNumber <= max) 
        {
            currentNumber = (RightOrLeft) ? (currentNumber != max ? currentNumber + increment : currentNumber) 
            : (currentNumber != min ? currentNumber - increment : currentNumber);
        }
        return currentNumber;
    }
}

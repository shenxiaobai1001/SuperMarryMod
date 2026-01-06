using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public GameObject center;
    public Slider sl_music;
    public Slider sl_sound;
    public TMP_InputField input_life;
    public Dropdown dp_level;
    public GameObject btn_changeLV;
    public Dropdown dp_mode;
    public GameObject btn_changeMD;
    public Dropdown dp_Resolution;
    public GameObject btn_Resolution;
    public GameObject btn_Close;

    float musicValue = 0;
    float soundValue = 0;
    int lifeCount = 0;
    int gameMode = 0;
    int width;
    int height;

    string gameLevel = "";
    // Start is called before the first frame update
    void Start()
    {
        sl_music.onValueChanged.AddListener(OnChangeMusic);
        sl_sound.onValueChanged.AddListener(OnChangeSound);
        input_life.onEndEdit.AddListener(OnInputLife);
        dp_level.onValueChanged.AddListener(OnLevelDropChanged);
        btn_changeLV.Click(OnClickChangeLV);
        dp_mode.onValueChanged.AddListener(OnModeDropChanged);
        btn_changeMD.Click(OnClickChangeMD);
        dp_Resolution.onValueChanged.AddListener(OnResolutionDropChanged);
        btn_Resolution.Click(OnClickResolution);
        btn_Close.Click(OnClose);
        center.SetActive(false);
    }

    void OnChangeMusic(float value)
    {
        musicValue= sl_music.value;
        soundValue = sl_sound.value;
        Sound.OnSetVolume(musicValue, soundValue);
    }

    void OnChangeSound(float value)
    {
        musicValue = sl_music.value;
        soundValue = sl_sound.value;
        Sound.OnSetVolume(musicValue, soundValue);
    }

    void OnInputLife(string value)
    {
        if (value ==string.Empty) return;
        lifeCount = int.Parse(value);
        ModData.mLife = lifeCount;
    }

    private void OnLevelDropChanged(int index)
    {
        int selectedIndex = dp_level.value;

        string selectedText = dp_level.options[selectedIndex].text;

        gameLevel = selectedText;
    }

    private void OnModeDropChanged(int index)
    {
        int selectedIndex = dp_mode.value;
        gameMode = selectedIndex;
    }

    private void OnResolutionDropChanged(int index)
    {
        int selectedIndex = dp_Resolution.value;
        string selectedText = dp_Resolution.options[selectedIndex].text;
        PFunc.Log(selectedText);
        string[] reso = selectedText.Split('x');
        width = int.Parse(reso[0]);
        height = int.Parse(reso[1]);
    }

    void OnClickResolution()
    {
        PFunc.Log("OnClickResolution", width, height);
        Screen.SetResolution( width,height, FullScreenMode.Windowed);
    }

    void OnClickChangeLV()
    {
        PFunc.Log("OnClickChangeLV", gameLevel);
        GameModController.Instance.OnLoadScene(gameLevel);
    }

    void OnClickChangeMD()
    {
        
    }

    void OnClose()
    {
        PFunc.Log("OnClose");
        center.SetActive(false);
    }
}

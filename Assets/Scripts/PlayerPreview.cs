using System.Collections;
using System.Collections.Generic;
using System.IO;
using _2dfmFile;
using Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPreview : MonoBehaviour
{
    public Text txtPlayerName;
    public Text txtScriptCount;
    public Text txtFrameCount;
    public Text txtSoundCount;
    
    private PlayerData _playerData;
    
    void Start()
    {
        var playerFilePath = Application.streamingAssetsPath + "/Players/DONGDONG.player";
        _playerData = PlayerFileReader.Read2dfmPlayerFile(playerFilePath);

        txtPlayerName.text = _playerData.Name;
        txtScriptCount.text = _playerData.ScriptCount.ToString();
        txtFrameCount.text = _playerData.SpriteFrameCount.ToString();
        txtSoundCount.text = _playerData.SoundCount.ToString();
    }
}

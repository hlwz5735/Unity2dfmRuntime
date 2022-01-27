using System.Collections;
using System.Collections.Generic;
using System.IO;
using _2dfmFile;
using Data;
using UnityEngine;

public class PlayerPreview : MonoBehaviour
{
    public PlayerData playerData;
    
    void Start()
    {
        var playerFilePath = Application.streamingAssetsPath + "/Players/DONGDONG.player";
        playerData = PlayerFileReader.Read2dfmPlayerFile(playerFilePath);
    }
}

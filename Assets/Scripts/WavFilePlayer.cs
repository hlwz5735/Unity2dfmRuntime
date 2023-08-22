using System;
using _2dfmFile;
using Attributes;
using Audio;
using Data;
using Game;
using UnityEngine;

public class WavFilePlayer : MonoBehaviour
{
    private PlayerData playerData;

    public AudioSource audioSource;


    [CustomLabel("声音序号")] public int soundNo = 0;

    private void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        var playerFilePath = Application.streamingAssetsPath + "/Players/DONGDONG.player";
        this.playerData = PlayerFileReader.read2dfmPlayerFile(playerFilePath);
    }

    private AudioClip getAudioClip()
    {
        int idx = Math.Min(this.playerData.soundCount - 1, Math.Max(0, this.soundNo));
        var wavBytes = playerData.sounds[idx].bytes;
        return WavUtility.ToAudioClip(wavBytes);
    }

    public void playAudio()
    {
        var clip = this.getAudioClip();
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

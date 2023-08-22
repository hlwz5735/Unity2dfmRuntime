using System;
using System.Collections.Generic;
using _2dfmFile;
using Audio;
using Data;
using SpriteFrame;
using UnityEngine;

namespace Game {
    public class GameStage : MonoBehaviour {
        private StageData _stageData;
        private Sprite[] _spritePool;
        private AudioClip[] _soundPool;

        public AudioSource bgmPlayer;
        public string stageName;

        private List<GameStageEntity> _entities = new List<GameStageEntity>();

        public void setStageData(StageData data)
        {
            _stageData = data;
            _spritePool = new Sprite[_stageData.pictures.Count];
            _soundPool = new AudioClip[_stageData.sounds.Count];
        }
        
        private void Start()
        {
            if (String.IsNullOrEmpty(stageName))
            {
                return;
            }
            var filePath = Application.streamingAssetsPath + "/Stages/" + stageName + ".stage";
            var data = StageFileReader.read2dfmStageFile(filePath);
            this.setStageData(data);
            
            bgmPlayer = GetComponent<AudioSource>();
            // 播放BGM
            var clip = soundAt(_stageData.bgmId);
            bgmPlayer.clip = clip;
            bgmPlayer.loop = true;
            bgmPlayer.Play();
        }
     
        public void loadSpriteFrame(int idx)
        {
            if (idx < 0 || idx >= _stageData.pictures.Count)
            {
                return;
            }
            var spriteFrame = _stageData.pictures[idx];
            PaletteData palette = null;
            if (!spriteFrame.hasPrivatePalette)
            {
                palette = _stageData.commonPalettes[0];
            }

            var sprite = SpriteFrameUtil.genSprite(spriteFrame, palette);
            sprite.name = _stageData.name + "_" + idx;
            this._spritePool[idx] = sprite;
        }

        public void loadSound(int idx)
        {
            if (idx < 0 || idx >= _stageData.sounds.Count)
            {
                return;
            }

            var sound = _stageData.sounds[idx];
            var clip = WavUtility.ToAudioClip(sound.bytes, 0, _stageData.name + "_" + sound.name);
            _soundPool[idx] = clip;
        }

        public Sprite spriteAt(int idx)
        {
            if (idx < 0 || idx >= _stageData.pictures.Count)
            {
                return null;
            }

            if (_spritePool[idx] == null) {
                loadSpriteFrame(idx);
            }
            
            return _spritePool[idx];
        }

        public AudioClip soundAt(int idx)
        {
            if (idx < 0 || idx >= _stageData.sounds.Count)
            {
                return null;
            }
            if (_soundPool[idx] == null) {
                loadSound(idx);
            }
            
            return _soundPool[idx];
        }
    }
}

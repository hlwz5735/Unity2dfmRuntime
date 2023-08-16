using System;
using Data;
using SpriteFrame;
using UnityEngine;

namespace Game {
    public class GameStage {
        public readonly StageData stageData;
        private Sprite[] _spritePool;
        
        public GameStage(StageData stageData)
        {
            this.stageData = stageData;
            this._spritePool = new Sprite[stageData.pictures.Count];
        }
     
        public void loadSpriteFrame(int idx)
        {
            if (idx < 0 || idx >= this.stageData.pictures.Count)
            {
                return;
            }
            var spriteFrame = stageData.pictures[idx];
            PaletteData palette = null;
            if (!spriteFrame.hasPrivatePalette)
            {
                palette = stageData.commonPalettes[0];
            }

            var sprite = SpriteFrameUtil.genSprite(spriteFrame, palette);
            sprite.name = this.stageData.name + "_" + idx;
            this._spritePool[idx] = sprite;
        }

        public Sprite spriteAt(int idx)
        {
            if (idx < 0 || idx >= this.stageData.pictures.Count)
            {
                return null;
            }

            if (_spritePool[idx] == null) {
                loadSpriteFrame(idx);
            }
            
            return _spritePool[idx];
        }
    }
}

using System;
using System.Collections.Generic;
using Data;
using SpriteFrame;
using UnityEngine;

namespace Game
{
    
    [Serializable]
    public class GamePlayer
    {
        public readonly PlayerData playerData;
        private List<Sprite> _spritePool;

        public GamePlayer(PlayerData playerData)
        {
            this.playerData = playerData;
            this._spritePool = new List<Sprite>(playerData.spriteFrameCount);
        }

        public void load()
        {
            for (int i = 0; i < this.playerData.spriteFrameCount; i++)
            {
                loadSpriteFrame(i);
            }
        }
        
        public void loadSpriteFrame(int idx)
        {
            if (idx < 0 || idx >= this.playerData.spriteFrameCount)
            {
                return;
            }
            var spriteFrame = playerData.spriteFrames[idx];
            PaletteData palette = null;
            if (!spriteFrame.hasPrivatePalette)
            {
                palette = playerData.publicPalettes[0];
            }

            var sprite = SpriteFrameUtil.genSprite(spriteFrame, palette);
            sprite.name = this.playerData.name + "_" + idx;
            this._spritePool.Add(sprite);
        }

        public Sprite spriteAt(int idx)
        {
            if (idx < 0 || idx >= _spritePool.Count)
            {
                return null;
            }
            return this._spritePool[idx];
        }
    }
}
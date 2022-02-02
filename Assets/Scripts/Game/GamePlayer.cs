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
        public readonly PlayerData PlayerData;
        private List<Sprite> spritePool;

        public GamePlayer(PlayerData playerData)
        {
            this.PlayerData = playerData;
            this.spritePool = new List<Sprite>(playerData.SpriteFrameCount);
        }

        public void Load()
        {
            for (int i = 0; i < this.PlayerData.SpriteFrameCount; i++)
            {
                LoadSpriteFrame(i);
            }
        }
        
        public void LoadSpriteFrame(int idx)
        {
            if (idx < 0 || idx >= this.PlayerData.SpriteFrameCount)
            {
                return;
            }
            var spriteFrame = PlayerData.SpriteFrames[idx];
            PaletteData palette = null;
            if (!spriteFrame.HasPrivatePalette)
            {
                palette = PlayerData.PublicPalettes[0];
            }

            var sprite = SpriteFrameUtil.GenSprite(spriteFrame, palette);
            sprite.name = this.PlayerData.Name + "_" + idx;
            this.spritePool.Add(sprite);
        }

        public Sprite SpriteAt(int idx)
        {
            if (idx < 0 || idx >= spritePool.Count)
            {
                return null;
            }
            return this.spritePool[idx];
        }
    }
}
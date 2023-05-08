using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.ScriptItem
{
    class AnimationFrameScript : BaseScriptItem
    {
        /// <summary>
        /// 停留时间
        /// </summary>
        public int freezeTime = 0;

        /// <summary>
        /// 图号
        /// </summary>
        public int picIndex = 0;

        /// <summary>
        /// 偏移量
        /// </summary>
        public Vector2 offset = Vector2.zero;

        /// <summary>
        /// 是否X轴反转
        /// </summary>
        public bool flipX = false;

        /// <summary>
        /// 是否Y轴反转
        /// </summary>
        public bool flipY = false;

        /// <summary>
        /// 是否固定朝向
        /// </summary>
        public bool fixDir = false;

        public AnimationFrameScript() : base((int)ScriptItemTypes.AnimationFrame)
        {

        }
    }
}

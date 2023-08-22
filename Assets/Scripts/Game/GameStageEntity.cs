using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Game
{
    class GameStageEntity : MonoBehaviour
    {
        private Transform _transform;
        private GameStage _stage;

        /* 以下为实体初期设置 */
        private bool _horizontalLoop = false;
        private bool _verticalLoop = false;
        private bool _horizontalScroll = false;
        private bool _verticalScroll = false;
        private int _horizontalScrollSize;
        private int _verticalScrollSize;
        
        public List<ScriptItemData> scriptItems;
        private Stack<ScriptRuntimeContext> _scriptCallStack;

    }
}
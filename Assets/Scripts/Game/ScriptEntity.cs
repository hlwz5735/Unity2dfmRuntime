using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game {
    /// <summary>
    /// 脚本实体对象组件，用于所有2DFM脚本实体对象的统一描述
    /// </summary>
    public class ScriptEntity : MonoBehaviour {
        private Transform _transform;

        public List<ScriptData> scripts;
        public List<ScriptItemData> scriptItems;

        private Stack<ScriptRuntimeContext> _scriptCallStack;

        // 起始脚本
        public int startScript = 0;

        private void Start() {
            _transform = GetComponent<Transform>();
            
            // 将初始脚本项推入
            _scriptCallStack.Push(new ScriptRuntimeContext() {
                scriptIdx = startScript,
                itemIdx = 0
            });
        }

        private void Update() {
            var deltaTime = Time.deltaTime;
        }
    }
}

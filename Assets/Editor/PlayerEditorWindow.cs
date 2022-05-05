using System;
using _2dfmFile;
using Data;
using Game;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PlayerEditorWindow : EditorWindow
    {
        private DefaultAsset _playerAsset;
        private DefaultAsset _prevPlayerAsset;

        private PlayerData _playerData;
        private GamePlayer _gamePlayer;

        private GameObject _target;

        public int SpriteIdx = 0;
        
        private void OnGUI()
        {
            _playerAsset = (DefaultAsset) EditorGUILayout.ObjectField("角色文件", _playerAsset, typeof(DefaultAsset), false);

            if (GUILayout.Button("读取"))
            {
                if (_playerAsset)
                {
                    ReadPlayerAsset();
                }
            }

            var maxFrameIdx = 0;
            if (_playerData != null)
            {
                EditorGUILayout.BeginVertical();
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.TextField("角色名", _playerData.Name);
                EditorGUILayout.IntField("脚本总数", _playerData.ScriptCount);
                EditorGUILayout.IntField("脚本项总数", _playerData.ScriptItemCount);
                EditorGUILayout.IntField("动画帧总数", _playerData.SpriteFrameCount);
                EditorGUILayout.IntField("声音总数", _playerData.SoundCount);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndVertical();

                maxFrameIdx = _playerData.SpriteFrameCount - 1;
            }

            _target = (GameObject) EditorGUILayout.ObjectField("目标", _target, typeof(GameObject), true);

            SpriteIdx = EditorGUILayout.IntField("精灵帧序号", SpriteIdx);
            SpriteIdx = Mathf.Clamp(SpriteIdx, 0, maxFrameIdx);

            if (_target && _gamePlayer != null)
            {
                var render = _target.GetComponent<SpriteRenderer>();
                if (render)
                {
                    var sprite = _gamePlayer.SpriteAt(SpriteIdx);
                    if (render.sprite != sprite)
                    {
                        render.sprite = sprite;
                    }
                }
            }
        }

        private void ReadPlayerAsset()
        {
            var playerFilePath = Application.streamingAssetsPath + "/Players/" + _playerAsset.name + ".player";
            _playerData = PlayerFileReader.Read2dfmPlayerFile(playerFilePath);
            _gamePlayer = new GamePlayer(_playerData);
            _gamePlayer.Load();
        }

        [MenuItem("2DFM/角色编辑器")]
        private static void OpenWindow()
        {
            var window = GetWindow(typeof(PlayerEditorWindow), true, "角色编辑器");
            window.Show();
        }
    }
}
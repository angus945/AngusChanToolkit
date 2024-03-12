using UnityEngine;
using UnityEditor;
using System;

namespace DataStorage
{
    [CustomEditor(typeof(StorageTokenBase), true)]
    public class DataStorgeTokenEditor : Editor
    {
        StorageTokenBase dataToken;

        void OnEnable()
        {
            dataToken = (StorageTokenBase)target;

            if(PlayerPrefs.HasKey(dataToken.storgeKey))
            {
                serializedObject.Update();
                dataToken.LoadData();
                serializedObject.ApplyModifiedProperties();
            }
        }
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(Application.isPlaying);

            base.DrawDefaultInspector();

            bool hasData = PlayerPrefs.HasKey(dataToken.storgeKey);

            GUILayout.BeginHorizontal();
            GUIButton("Apply Storge", true, dataToken.ApplyToStorge);
            GUIButton("Revert Storge", hasData, dataToken.LoadData);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUIButton("Delect Storge Data", Color.red, hasData, () =>
            {
                serializedObject.Update();
                dataToken.DelectData();
                serializedObject.ApplyModifiedProperties();
            });
            GUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();
        }

        void GUIButton(string header, bool enable, Action onClick)
        {
            EditorGUI.BeginDisabledGroup(!enable);

            if (GUILayout.Button(header))
            {
                onClick.Invoke();
            }

            EditorGUI.EndDisabledGroup();
        }
        void GUIButton(string header, Color color, bool enable, Action onClick)
        {
            Color baseColor = GUI.color;
            GUI.color = color;

            GUIButton(header, enable, onClick);

            GUI.color = baseColor;
        }
    }

}

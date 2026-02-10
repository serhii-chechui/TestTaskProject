using TMPro.EditorUtilities;
using UnityEditor;

namespace ProductMadness.TestTaskProject.UI.Editor
{
    [CustomEditor(typeof(RollingTextTMP), true)]
    [CanEditMultipleObjects]
    public class RollingTextTMPEditor : TMP_EditorPanelUI
    {
        private SerializedProperty duration;
        private SerializedProperty ease;
        private SerializedProperty template;
        private SerializedProperty currency;
        
        private SerializedProperty doPopScale;
        private SerializedProperty popScale;
        private SerializedProperty popDuration;
        private SerializedProperty checkSum;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            duration = serializedObject.FindProperty("duration");
            ease = serializedObject.FindProperty("ease");
            template = serializedObject.FindProperty("template");
            currency = serializedObject.FindProperty("currency");
            
            doPopScale  = serializedObject.FindProperty("doPopScale");
            popScale = serializedObject.FindProperty("popScale");
            popDuration = serializedObject.FindProperty("popDuration");
            checkSum = serializedObject.FindProperty("checkSum");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Custom properties", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(duration);
            EditorGUILayout.PropertyField(ease);
            EditorGUILayout.PropertyField(template);
            EditorGUILayout.PropertyField(currency);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Pop Scale", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(doPopScale);
            EditorGUILayout.PropertyField(popScale);
            EditorGUILayout.PropertyField(popDuration);
            EditorGUILayout.PropertyField(checkSum);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
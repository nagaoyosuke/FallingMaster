
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DebugManager))] // 拡張するクラスを指定
public class MaterialSettingsEditor : Editor
{ // 継承しているクラスがMonoBehaviourでないところに注意！

    public override void OnInspectorGUI()
    {
        // 元のインスペクター部分を表示
        base.OnInspectorGUI();

        // targetを変換して対象を取得
        DebugManager debugManager = target as DebugManager;

        if (GUILayout.Button("Chenge!"))
        {
            debugManager.Change();
        }

        if (GUILayout.Button("CheckSet!"))
        {
            debugManager.CheckSet();
        }

    }
}
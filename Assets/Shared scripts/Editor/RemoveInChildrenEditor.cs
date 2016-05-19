using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(RemoveInChildren))]
public class RemoveInChildrenEditor : Editor {

    RemoveInChildren removeScript;

    public override void OnInspectorGUI()
    {
        removeScript = (RemoveInChildren)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Remove components of chosen type"))
        {
            removeScript.RemoveComponents();
        }
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        //base.OnInspectorGUI();
    }
}

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace UTPI.SceneViewer
{
    public class PrefabViewerWindow : EditorWindow
    {
        static PrefabViewerWindow prefabViewer;
        Vector2 scrollPosition = Vector2.zero;

        [MenuItem("Window/General/Prefab Viewer", priority = 1)]
        public static void Init()
        {
            prefabViewer = GetWindow<PrefabViewerWindow>("Prefab Viewer");
            prefabViewer.RefreshPrefabs();
        }

        string[] prefabsGUIDs;
        string currentFolderName;
        string[] items;

        GUIStyle titleStyle;
        GUIStyle GetTitleStyle()
        {
            GUIStyle _style = new GUIStyle();

            _style.alignment = TextAnchor.MiddleCenter;
            _style.fontStyle = FontStyle.Bold;
            _style.fontSize = 24;
            _style.normal.textColor = Color.white;

            return _style;
        }

        GUIStyle buttonStyle;
        GUIStyle GetButtonStyle()
        {
            GUIStyle _style = new GUIStyle(GUI.skin.button);

            _style.fontStyle = FontStyle.Bold;
            _style.fontSize = 16;
            _style.normal.textColor = Color.white;

            return _style;
        }

        void RefreshPrefabs()
        {
            prefabsGUIDs = AssetDatabase.FindAssets("t:Prefab");
        }

        private void OnGUI()
        {
            titleStyle = GetTitleStyle();
            buttonStyle = GetButtonStyle();

            if (GUILayout.Button("Refresh project prefabs", buttonStyle))
            {
                RefreshPrefabs();
            }

            if (prefabsGUIDs is null)
                RefreshPrefabs();
            if (prefabsGUIDs.Length == 0)
                return;

            currentFolderName = "";

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            EditorGUILayout.Space(5f);

            foreach (string prefabGUID in prefabsGUIDs)
            {
                items = AssetDatabase.GUIDToAssetPath(prefabGUID).Split('/');

                if (items[1] != "Prefabs")
                    continue;

                if (currentFolderName != items[items.Length - 2])
                {
                    EditorGUILayout.Space(5f);
                    currentFolderName = items[items.Length - 2];
                    GUILayout.Label(currentFolderName, titleStyle);
                }

                string itemName = items[items.Length - 1];

                if (GUILayout.Button(itemName.Remove(itemName.Length - 7), buttonStyle))
                {
                    AssetDatabase.OpenAsset(AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(prefabGUID)));
                }
            }

            EditorGUILayout.EndScrollView();
        }
    }
}
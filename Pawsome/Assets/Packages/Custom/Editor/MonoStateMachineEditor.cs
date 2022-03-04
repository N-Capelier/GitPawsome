using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

[CustomEditor(typeof(MonoStateMachine), true)]
[CanEditMultipleObjects]
public class MonoStateMachineEditor : Editor
{
	string newStateName;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GUILayout.Space(10);

		GUILayout.Label("Create new MonoState");

		if (EditorApplication.isCompiling)
		{
			GUIStyle _errorStyle = new GUIStyle();
			_errorStyle.normal.textColor = Color.red;
			GUILayout.Label("Compiling...", _errorStyle);
			return;
		}

		GUILayout.BeginHorizontal();

		newStateName = GUILayout.TextField(newStateName);

		if (GUILayout.Button("Generate", GUILayout.MaxWidth(80f)))
		{
			CreateStateScriptAsset();
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("");
		if (GUILayout.Button("Instantiate", GUILayout.MaxWidth(90f)))
		{
			CreateStateScriptableObjectAsset();
		}
		GUILayout.EndHorizontal();
	}

	void CreateStateScriptAsset()
	{
		if (newStateName == null || newStateName == "")
			throw new System.ArgumentNullException("MonoState name can not be null.");

		newStateName.Replace(' ', '_');
		newStateName.Replace('-', '_');

		string _path = $"Assets/{newStateName}.cs";

		if (File.Exists(_path))
			throw new System.InvalidOperationException("File name already exists.");

		StringBuilder _stringBuilder = new StringBuilder();
		_stringBuilder.Append("using System.Collections;\n");
		_stringBuilder.Append("using System.Collections.Generic;\n");
		_stringBuilder.Append("using UnityEngine;\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append($"public class {newStateName} : MonoState\n");
		_stringBuilder.Append("{\n");
		_stringBuilder.Append("//public override void OnStateEnter()\n");
		_stringBuilder.Append("//{\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append("//}\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append("//public override void OnStateUpdate()\n");
		_stringBuilder.Append("//{\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append("//}\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append("//public override void OnStateFixedUpdate()\n");
		_stringBuilder.Append("//{\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append("//}\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append("//public override void OnStateLateUpdate()\n");
		_stringBuilder.Append("//{\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append("//}\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append("//public override void OnStateExit()\n");
		_stringBuilder.Append("//{\n");
		_stringBuilder.Append("\n");
		_stringBuilder.Append("//}\n");
		_stringBuilder.Append("}");

		using (StreamWriter _out =
			new StreamWriter(_path))
		{
			_out.Write(_stringBuilder.ToString());
		}

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	public void CreateStateScriptableObjectAsset()
	{
		if (newStateName == null && newStateName == "")
		{
			throw new System.ArgumentNullException("MonoState name can not be null.");
		}

		string _path = $"Assets/{newStateName}.asset";

		if (File.Exists(_path))
			throw new System.InvalidOperationException("File name already exists.");

		MonoState _instance = (MonoState)CreateInstance(newStateName);

		_instance.SetName(newStateName);

		MonoStateMachine _ref = (MonoStateMachine)target;

		_ref.states.Add(_instance);

		AssetDatabase.CreateAsset(_instance, _path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		newStateName = "";

	}
}
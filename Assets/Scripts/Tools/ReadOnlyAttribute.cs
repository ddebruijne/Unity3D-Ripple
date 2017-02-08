#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

/*
 *		ReadOnlyAttribute
 *		Makes value readonly in the Editor. Customized so it doesn't throw errors on builds.
 *		
 *		Ripped from the internet by Danny de Bruijne. Source unknown.
 */

public class ReadOnlyAttribute : PropertyAttribute {

}
#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer {
	public override float GetPropertyHeight(SerializedProperty property,
											GUIContent label) {
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

	public override void OnGUI(Rect position,
							   SerializedProperty property,
							   GUIContent label) {
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}
#endif

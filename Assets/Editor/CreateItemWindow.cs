using UnityEngine;
using UnityEditor;

public class CreateItemWindow : EditorWindow {

    [MenuItem("Window/Create new item")]
	static void OpenWindow()
    {
        CreateItemWindow window = (CreateItemWindow)GetWindow(typeof(CreateItemWindow));
        window.minSize = new Vector2(300, 600);
        window.Show();
    }
}

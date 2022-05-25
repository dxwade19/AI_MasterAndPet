using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ShortcutEditor : Editor
{
    [MenuItem("Shortcuts/PersistentDataPath")]
    static void OpenPersistentDataPath() => Application.OpenURL(Application.persistentDataPath);


    [MenuItem("Shortcuts/DataPath")]
    static void OpenDataPath() => Application.OpenURL(Application.dataPath);
}

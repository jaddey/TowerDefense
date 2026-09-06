using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CardListEditor : EditorWindow {
    
    public List<GameObject> prefabs = new List<GameObject>(); // список выбранных префабов
    public List<int> counts = new List<int>(); // список количества копий каждого префаба
    
    [MenuItem("Tools/Card List Editor")]
    public static void ShowWindow() {
        GetWindow<CardListEditor>("Card List Editor");
    }
    
    void OnGUI() {
        GUILayout.Label("Select prefabs to add to new CardList", EditorStyles.boldLabel);
        
        // отображение выбранных префабов и полей для количества копий
        for (int i = 0; i < prefabs.Count; i++) {
            GUILayout.BeginHorizontal();
            prefabs[i] = (GameObject)EditorGUILayout.ObjectField(prefabs[i], typeof(GameObject), false);
            counts[i] = EditorGUILayout.IntField(counts[i], GUILayout.Width(50));
            GUILayout.EndHorizontal();
        }
        
        GUILayout.Space(10);
        
        // кнопка для добавления нового префаба в список
        if (GUILayout.Button("Add Prefab")) {
            prefabs.Add(null);
            counts.Add(1);
        }
        
        GUILayout.Space(10);
        
        // кнопка для генерации нового CardList на основе выбранных префабов
        if (GUILayout.Button("Generate CardList")) {
            CardsList newList = ScriptableObject.CreateInstance<CardsList>();
            for (int i = 0; i < prefabs.Count; i++) {
                for (int j = 0; j < counts[i]; j++) {
                    Card newCard = prefabs[i].GetComponent<Card>();
                    if (newCard != null) {
                        newList.cardList.Add(newCard);
                    }
                }
            }
            AssetDatabase.CreateAsset(newList, "Assets/CardLists/NewCardList.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newList;
            Debug.Log("New CardList generated.");
            Close();
        }
    }
}


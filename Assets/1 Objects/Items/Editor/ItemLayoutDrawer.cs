using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ItemLayout))]
public class ItemLayoutDrawer : PropertyDrawer
{
    const int CELL_SIZE = 18;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 6f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(property.propertyType != SerializedPropertyType.Generic)
        {
            EditorGUI.LabelField(position, label.text, "Use ItemLayout with List<Vector2>.");
            return;
        }

        Rect labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(labelRect, label.text);

        var layout = property.FindPropertyRelative("positions");
        List<SerializedProperty> layoutPropertyList = new();
        List<Vector2> layoutPositions = new();

        for (int i = 0; i < layout.arraySize; ++i)
        {
            layoutPropertyList.Add(layout.GetArrayElementAtIndex(i));
            layoutPositions.Add(layoutPropertyList[i].vector2Value);
        }

        Vector2 cellPos;
        Rect togglePosition = new Rect(position.position, Vector2.one * CELL_SIZE);
        for (int y = 2; y > -3; --y)
        {
            togglePosition.y = position.y - CELL_SIZE * y + 2 * CELL_SIZE + 10;
            for (int x = -2; x < 3; ++x)
            {
                cellPos = new Vector2(x, y);
                togglePosition.x = position.x + CELL_SIZE * x + 2 * CELL_SIZE + 90;

                bool isCenter = x == 0 && x == y;

                if(!isCenter) EditorGUI.BeginChangeCheck();

                bool toggled = EditorGUI.Toggle(togglePosition, isCenter || layoutPositions.Contains(cellPos));

                if(!isCenter && EditorGUI.EndChangeCheck())
                {
                    if (toggled)
                        AddArrayElement(layout, cellPos);
                    else
                        RemoveArrayElement(layout, cellPos);
                }
            }
        }
    }

    private void AddArrayElement(SerializedProperty property, Vector2 value)
    {
        property.InsertArrayElementAtIndex(property.arraySize);
        var newElement = property.GetArrayElementAtIndex(property.arraySize - 1);
        newElement.vector2Value = value;
    }

    private void RemoveArrayElement(SerializedProperty property, Vector2 value)
    {
        for (int i = 0; i < property.arraySize; ++i)
        {
            var element = property.GetArrayElementAtIndex(i);
            if (element.vector2Value != value)
                continue;

            property.DeleteArrayElementAtIndex(i);
            return;
        }
    }
}

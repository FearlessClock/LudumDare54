using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ItemLayout))]
public class ItemLayoutDrawer : PropertyDrawer
{
    const int CELL_SIZE = 18;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(property.propertyType != SerializedPropertyType.Generic)
        {
            EditorGUI.LabelField(position, label.text, "Use ItemLayout with List<Vector2>.");
            return;
        }

        EditorGUILayout.LabelField(label.text);
        var layout = property.FindPropertyRelative("positions");
        List<SerializedProperty> layoutPropertyList = new();
        List<Vector2> layoutPositions = new();

        for (int i = 0; i < layout.arraySize; ++i)
        {
            layoutPropertyList.Add(layout.GetArrayElementAtIndex(i));
            layoutPositions.Add(layoutPropertyList[i].vector2Value);
        }

        Rect togglePosition = position;
        for (int y = 2; y > -3; --y)
        {
            togglePosition.y = position.y + CELL_SIZE * y + 2 * CELL_SIZE;
            for (int x = -2; x < 3; ++x)
            {
                togglePosition.x = position.x + CELL_SIZE * x + 2 * CELL_SIZE;

                bool isCenter = (x == 0 && x == y) ? true : false;

                if(!isCenter)
                    EditorGUI.BeginChangeCheck();
                bool toggled = EditorGUI.Toggle(togglePosition, isCenter);

                if(!isCenter && EditorGUI.EndChangeCheck())
                {
                    if (toggled)
                        AddArrayElement(layout, new Vector2(x, y));
                    else
                        RemoveArrayElement(layout, new Vector2(x, y));
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

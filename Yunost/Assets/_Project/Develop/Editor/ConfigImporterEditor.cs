#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;

[CustomEditor(typeof(InitSystem))]
public class ConfigImporterEditor : Editor
{
    private SerializedProperty configProp;
    private SerializedObject configSerialized;
    private InitSystem initSystem;

    private void OnEnable()
    {
        initSystem = (InitSystem)target;
        configProp = serializedObject.FindProperty("config");
        UpdateConfigSerialized();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Сохраняем предыдущее значение конфига
        var prevConfig = configProp.objectReferenceValue;

        // Рисуем поле для ScriptableObject
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(configProp);
        if (EditorGUI.EndChangeCheck())
        {
            UpdateConfigSerialized();

            // Вызываем OnValidate если изменилась ссылка на конфиг
            if (prevConfig != configProp.objectReferenceValue)
            {
                CallOnValidate();
            }
        }

        // Если конфиг существует
        if (configSerialized != null)
        {
            configSerialized.Update();

            // Получаем итератор по всем видимым свойствам
            SerializedProperty iterator = configSerialized.GetIterator();
            bool enterChildren = true;
            bool anyPropertyChanged = false;

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;

                // Пропускаем служебные поля
                if (iterator.name == "m_Script") continue;

                // Проверяем изменения свойств
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(iterator, true);
                if (EditorGUI.EndChangeCheck())
                {
                    anyPropertyChanged = true;
                }
            }

            configSerialized.ApplyModifiedProperties();

            // Вызываем OnValidate если изменились свойства конфига
            if (anyPropertyChanged)
            {
                CallOnValidate();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void UpdateConfigSerialized()
    {
        if (configProp != null && configProp.objectReferenceValue != null)
        {
            configSerialized = new SerializedObject(configProp.objectReferenceValue);
        }
        else
        {
            configSerialized = null;
        }
    }

    private void CallOnValidate()
    {
        // Получаем метод OnValidate через рефлексию
        MethodInfo onValidate = typeof(InitSystem).GetMethod("OnValidate",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (onValidate != null)
        {
            onValidate.Invoke(initSystem, null);
        }
        else
        {
            Debug.LogWarning("OnValidate method not found in InitSystem");
        }
    }
}
#endif
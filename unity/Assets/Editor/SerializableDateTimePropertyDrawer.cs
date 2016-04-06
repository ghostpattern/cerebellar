
using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializableDateTime))]
class SerializableDateTimePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float buffer = 5.0f;
        float yearWidth = 50.0f;
        float monthWidth = 40.0f;
        float dayWidth = 40.0f;
        float hourWidth = 40.0f;
        float minuteWidth = 40.0f;

        float currOffset = position.x;
        // Calculate rects
        Rect yearRect = new Rect(currOffset, position.y, yearWidth, position.height);
        currOffset += yearWidth + buffer;
        Rect monthRect = new Rect(currOffset, position.y, monthWidth, position.height);
        currOffset += monthWidth + buffer;
        Rect dayRect = new Rect(currOffset, position.y, dayWidth, position.height);
        currOffset += dayWidth + buffer;
        Rect hourRect = new Rect(currOffset, position.y, hourWidth, position.height);
        currOffset += hourWidth + buffer;
        Rect minuteRect = new Rect(currOffset, position.y, minuteWidth, position.height);
        currOffset += minuteWidth + buffer;

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        SerializedProperty serializedProperty = property.FindPropertyRelative("_serializedDateTime");

        string dateTimeString = serializedProperty.stringValue;
        DateTime parsedDateTime;
        if(DateTime.TryParse(dateTimeString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out parsedDateTime) == false)
        {
            parsedDateTime = new DateTime(1979, 11, 27, 8, 42, 0);
        }

        int year = parsedDateTime.Year;
        int month = parsedDateTime.Month;
        int day = parsedDateTime.Day;
        int hour = parsedDateTime.Hour;
        int minute = parsedDateTime.Minute;

        // EditorGUI.LabelField(position, year.ToString());
        year = EditorGUI.IntPopup(yearRect, year, new[] {"2095" }, new[] { 2095 });
        month = EditorGUI.IntPopup(monthRect, month,
            new[] {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"},
            new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12});
        day = EditorGUI.IntPopup(dayRect, day,
            new[]
            {
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                "21", "22", "23", "24", "25", "26", "27", "28", "29", "30",
                "31"
            },
            new[] {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                31 });
        hour = EditorGUI.IntPopup(hourRect, hour,
            new[]
            {
                "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                "21", "22", "23"
            },
            new[] {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23});
        minute = EditorGUI.IntPopup(minuteRect, minute,
            new[]
            {
                "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                "21", "22", "23", "24", "25", "26", "27", "28", "29", "30",
                "31", "32", "33", "34", "35", "36", "37", "38", "39", "40",
                "41", "42", "43", "44", "45", "46", "47", "48", "49", "50",
                "51", "52", "53", "54", "55", "56", "57", "58", "59"
            },
            new[] {
                00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
                51, 52, 53, 54, 55, 56, 57, 58, 59});

        parsedDateTime = parsedDateTime.AddYears(year - parsedDateTime.Year);
        parsedDateTime = parsedDateTime.AddMonths(month - parsedDateTime.Month);
        parsedDateTime = parsedDateTime.AddDays(day - parsedDateTime.Day);
        parsedDateTime = parsedDateTime.AddHours(hour - parsedDateTime.Hour);
        parsedDateTime = parsedDateTime.AddMinutes(minute - parsedDateTime.Minute);

        serializedProperty.stringValue = parsedDateTime.ToString("O");

        // EditorGUI.PropertyField(amountRect, serializedProperty, GUIContent.none);

        //EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("DateTime.Day"), GUIContent.none);
        //EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("_serializedDateTime.Hour"), GUIContent.none);
        //EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("_serializedDateTime.Minute"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}

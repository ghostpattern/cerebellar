using System;
using System.Globalization;
using UnityEngine;

[Serializable]
public class SerializableDateTime : ISerializationCallbackReceiver
{
    public DateTime DateTime { get; set; }

    public SerializableDateTime()
    {
        
    }

    public SerializableDateTime(int year, int month, int day, int hour, int minute, int second)
    {
        DateTime = new DateTime(year, month, day, hour, minute, second);
    }

    [SerializeField]
    private string _serializedDateTime;

    public void OnBeforeSerialize()
    {
        _serializedDateTime = DateTime.ToString("O");
    }

    public void OnAfterDeserialize()
    {
        if(string.IsNullOrEmpty(_serializedDateTime) == false)
        {
            DateTime = DateTime.Parse(_serializedDateTime, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        }
    }
}

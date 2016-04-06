public enum SceneTrigger
{
    Time,
    CharacterEnterLocation
}

public enum CharacterKey
{
    Rigger,
    Architect
}

public class SceneData : StoryElementData
{
    public CharacterKey Character;

    public string Location;
    public SerializableDateTime Time;

    public int SceneIndex;
}
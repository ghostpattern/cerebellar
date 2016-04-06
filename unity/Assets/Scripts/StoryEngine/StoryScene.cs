using System;

public class StoryScene
{
    public SceneData Data { get; private set; }

    public DateTime Time { get { return Data.Time.DateTime; } }

    public StoryScene(SceneData data)
    {
        Data = data;
    }
}
public class DebugSettings : MonoBehaviourSingleton<DebugSettings>
{
    public string LoadIntoScene;

    protected override bool DestroyOnLoad { get { return false; } }
    protected override void InitSingletonInstance()
    {

    }

    protected override void DestroySingletonInstance()
    {

    }
}

using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class RecordingLists : RecordingBaseLists
    {
        public RecordingLists()
            : base("recordings", new RecordingList())
        { }
    }
}

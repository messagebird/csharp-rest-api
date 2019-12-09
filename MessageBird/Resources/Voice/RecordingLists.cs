using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class RecordingLists : VoiceBaseLists<Recording>
    {
        public RecordingLists()
            : base("recordings", new RecordingList())
        { }
    }
}

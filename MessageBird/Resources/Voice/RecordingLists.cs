using MessageBird.Objects.Voice;
using System;

namespace MessageBird.Resources.Voice
{
    public class RecordingLists : VoiceBaseLists<Recording>
    {
        public RecordingLists(string callId, string legId)
            : base("recordings", new RecordingList { CallId = callId, LegId = legId })
        { }

        public RecordingLists(Objects.Voice.RecordingList list) : base("recordings", list) { }

        public override string Uri
        {
            get
            {
                return String.Format("calls/{0}/legs/{1}/{2}", ((RecordingList)Object).CallId, ((RecordingList)Object).LegId, Name);
            }
        }
    }
}

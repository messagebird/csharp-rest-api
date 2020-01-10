using MessageBird.Objects.Voice;
using System;

namespace MessageBird.Resources.Voice
{
    public class TranscriptionsLists : VoiceBaseLists<Transcription>
    {
        public TranscriptionsLists(string callId, string legId, string recordingId)
            : base("transcriptions", new TranscriptionList { CallId = callId, LegId = legId, RecordingId = recordingId })
        { }

        public TranscriptionsLists(Objects.Voice.TranscriptionList list) : base("transcriptions", list) { }

        public override string Uri
        {
            get
            {
                return String.Format("calls/{0}/legs/{1}/recordings/{2}/{3}", ((TranscriptionList)Object).CallId, ((TranscriptionList)Object).LegId, ((TranscriptionList)Object).RecordingId, Name);
            }
        }
    }
}

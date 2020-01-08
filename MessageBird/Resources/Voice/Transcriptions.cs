using System;
using MessageBird.Exceptions;
using MessageBird.Objects.Voice;
using Newtonsoft.Json;

namespace MessageBird.Resources.Voice
{
    public class Transcriptions : VoiceBaseResource<Transcription>
    {
        public Transcriptions(Transcription transcription)
            : base("calls", transcription)
        {
        }

        public override string Uri
        {
            get
            {
                return String.Format("{0}/{1}/legs/{2}/recordings/{3}/transcriptions/{4}", Name, ((Transcription)Object).CallId, ((Transcription)Object).LegId, ((Transcription)Object).RecordingId, Object.Id);
            }
        }

        public string DownloadUri
        {
            get
            {
                return String.Format("{0}/{1}/legs/{2}/recordings/{3}/transcriptions/{4}.txt", Name, ((Transcription)Object).CallId, ((Transcription)Object).LegId, ((Transcription)Object).RecordingId, Object.Id);
            }
        }

        public override string Serialize()
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(new { ((Transcription)Object).Language }, settings);
        }

        public override void Deserialize(string resource)
        {
            try
            {
                Object = JsonConvert.DeserializeObject<VoiceResponse<Transcription>>(resource);
            }
            catch (JsonSerializationException e)
            {
                throw new ErrorException("Received response in an unexpected format!", e);
            }
        }
    }
}

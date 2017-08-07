using Microsoft.Bing.Speech;
using System.IO;
using System.Threading.Tasks;

namespace Common.Interface.IService
{
    public interface ISpeechService
    {
        Task<RecognitionPhrase> ReconizeAudioStreamAsync(Stream audioStream);
    }
}

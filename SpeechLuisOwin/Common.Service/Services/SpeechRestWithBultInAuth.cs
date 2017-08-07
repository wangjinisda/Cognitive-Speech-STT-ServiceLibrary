using System;
using System.IO;
using Common.Interface.IService;
using Common.Service.AuthorizationProvider;
using SpeechLuisOwin.Src.Services;

namespace Common.Service.Services
{
    public class SpeechRestWithBultInAuth : ISpeechRestService
    {
        private Authentication _authentication;

        private ISpeechRestService _restService;

        public SpeechRestWithBultInAuth(string subKey)
        {
            _authentication = new Authentication(subKey);
            _restService = new SpeechRestService(_authentication);
        }

        dynamic ISpeechRestService.SendAudio(Stream stream)
        {
            return _restService.SendAudio(stream);
        }

        dynamic ISpeechRestService.SendAudio(byte[] audioArray, int length)
        {
            return _restService.SendAudio(audioArray, length);
        }

        ISpeechRestService ISpeechRestService.UseLocale(string locale)
        {
            return _restService.UseLocale(locale);
        }
    }

}

using Common.Interface.IService;
using Common.Service.Exceptions;
using Silk2WavCommon.Silk2WavConverter;
using SpeechWithLuis.Src.Model;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SpeechLuisOwin.Controllers
{  
    [Authorize]
    [Route("api/[controller]")]
    public class SilkAuthController : Controller
    {
        private ISpeechServiceWithRabdom _speechServiceWithRabdom;

        private ILuisService _luisService;

        private ISpeechService _speechService;


        public SilkAuthController(ISpeechServiceWithRabdom speechServiceWithRabdom, ILuisService luisService, ISpeechService speechService)
        {
            _speechServiceWithRabdom = speechServiceWithRabdom;
            _luisService = luisService;
            _speechService = speechService;
        }


        [HttpPost]
        public async Task<dynamic> Post([FromBody]byte[] audioSource, [FromQuery]string locale = "zh-cn", [FromQuery]bool withIntent = true)
        {
            long tsWhenGetAudioText = 0;
            long tsWhenGetAudioIntention = 0;
            var arrivalTime = DateTime.UtcNow;

            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var silk2Wav = new Silk2Wav(audioSource, audioSource.Count<byte>());
                var outs = _speechServiceWithRabdom.WithRandom(service => {
                    var outss = service
                    .UseLocale(locale)
                    .SendAudio(silk2Wav.WavBytes, silk2Wav.WavBytesLen);
                    return outss;
                });
                var result = outs.results[0];
                string lexical = result.name;
                string content = result.lexical;
                /*
                var outs = await speechService.ReconizeAudioStreamAsync(new MemoryStream(audioSource));
                string lexical = outs.DisplayText;
                */
                stopWatch.Stop();
                tsWhenGetAudioText = stopWatch.ElapsedMilliseconds;
                dynamic intentions = null;

                if (withIntent)
                {
                    stopWatch.Restart();
                    intentions = await _luisService.GetIntention(lexical);
                    stopWatch.Stop();
                    tsWhenGetAudioIntention = stopWatch.ElapsedMilliseconds;
                }

                return new ResponeModel
                {
                    Text = content,
                    intentions = intentions,
                    GetAudioTextLantency = tsWhenGetAudioText,
                    GetAudioIntentionLantency = tsWhenGetAudioIntention,
                    StatusCode = 0,
                    StatusMessage = "Status OK.",
                    ArrivalTime = arrivalTime,
                    EndTime = DateTime.UtcNow
                };
            }
            catch (BaseException e)
            {
                return new ResponeModel
                {
                    Text = "",
                    intentions = null,
                    GetAudioTextLantency = 0,
                    GetAudioIntentionLantency = 0,
                    StatusCode = e.ErrorCode,
                    StatusMessage = e.Message,
                    ArrivalTime = arrivalTime,
                    EndTime = DateTime.UtcNow
                };
            }
        }

        [HttpGet]
        public async Task<ResponeModel> Get(string id)
        {
            await Task.Delay(100);
            return new ResponeModel
            {
                Text = "test",
                intentions = "test01"
            };
        }
    }
}
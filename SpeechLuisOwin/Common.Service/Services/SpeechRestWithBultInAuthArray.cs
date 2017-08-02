using Common.Interface.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common.Service.Services
{
    public class SpeechRestWithBultInAuthArray : ISpeechServiceWithRabdom
    {
        private ISpeechRestService[] _speechRestWithBultInAuthArray;

        private int order = 0;

        private int count = 0;

        public SpeechRestWithBultInAuthArray(string[] subKeys)
        {
            _speechRestWithBultInAuthArray = subKeys.Select(key => new SpeechRestWithBultInAuth(key)).ToArray();
            count = subKeys.Count();
        }

        dynamic ISpeechServiceWithRabdom.WithRandom(Func<ISpeechRestService, dynamic> func)
        {
            var currentOrder = order;

            this.order = (currentOrder + 1) % count;
            return func(_speechRestWithBultInAuthArray[currentOrder]);
        }
    }
}

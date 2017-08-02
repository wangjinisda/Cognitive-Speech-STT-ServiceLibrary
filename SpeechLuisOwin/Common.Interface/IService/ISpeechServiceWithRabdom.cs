

using System;

namespace Common.Interface.IService
{
    public interface ISpeechServiceWithRabdom
    {
        dynamic WithRandom(Func<ISpeechRestService, dynamic> func);
    }
}

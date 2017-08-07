using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechLuisCore.Src.Formatters
{
    public class BinaryFormatter : InputFormatter
    {
        private static Type _supportedType = typeof(byte[]);

        public BinaryFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/octet-stream"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/wav"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("audio/wav"));


        }

        protected override bool CanReadType(Type type)
        {
            return type == _supportedType;
        }


        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var request = context.HttpContext.Request;

            using (var reader = new StreamReader(request.Body))
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        request.Body.CopyTo(memoryStream);
                        memoryStream.Position = 0;
                        return await InputFormatterResult.SuccessAsync(memoryStream.ToArray());
                    }
                }
                catch
                {
                    return await InputFormatterResult.FailureAsync();
                }
            }

        }
    }
}

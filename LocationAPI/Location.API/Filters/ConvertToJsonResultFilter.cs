using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Buffers;

namespace Location.API.Filters;

public class ConvertToJsonResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value != null)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            var jsonOutputFormatter = new NewtonsoftJsonOutputFormatter(serializerSettings,
                ArrayPool<char>.Shared, new MvcOptions(), null);

            objectResult.Formatters.Add(jsonOutputFormatter);

            objectResult.ContentTypes.Add("application/json");
        }

        await next();
    }
}

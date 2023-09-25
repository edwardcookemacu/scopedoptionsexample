using Microsoft.AspNetCore.Mvc;
using WebApplication1.OptionsStuff;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScopedController
    {
        private readonly ScopedOptions _options;
        private readonly GuidGenerator _guidGenerator;

        public ScopedController(ScopedOptions options, GuidGenerator guidGenerator)
        {
            _options = options;
            _guidGenerator = guidGenerator;
        }

        [HttpGet]
        public string Get() => _options.Guid;

        [HttpGet("long")]
        public async Task<object> LongGet()
        {
            var oldGuid = _options.Guid;
            var expected = _guidGenerator.Guid;
            _options.Guid = expected;
            //allow another request to come in and change it after we just did
            await Task.Delay(5000);
            return new
            {
                Original = oldGuid,
                Expected = expected,
                New = _options.Guid
            };
        }

    }
}

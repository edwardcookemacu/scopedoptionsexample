using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication1.OptionsStuff;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StandardOptionsController
    {
        private readonly IOptions<StandardOptions> _options;
        private readonly GuidGenerator _guidGenerator;

        public StandardOptionsController(IOptions<StandardOptions> options, GuidGenerator guidGenerator)
        {
            _options = options;
            _guidGenerator = guidGenerator;
        }

        [HttpGet]
        public string Get() => _options.Value.Guid;

        [HttpGet("long")]
        public async Task<object> LongGet()
        {
            var oldGuid = _options.Value.Guid;
            var expected = _guidGenerator.Guid;
            _options.Value.Guid = expected;
            //allow another request to come in and change it after we just did
            await Task.Delay(5000);
            return new
            {
                Original = oldGuid,
                Expected = expected,
                New = _options.Value.Guid
            };
        }
    }
}

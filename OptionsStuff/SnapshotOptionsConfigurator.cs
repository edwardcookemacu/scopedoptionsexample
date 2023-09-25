using Microsoft.Extensions.Options;

namespace WebApplication1.OptionsStuff
{
    public class SnapshotOptionsConfigurator : IConfigureOptions<SnapshotOptions>
    {
        public readonly GuidGenerator _guidGenerator;

        public SnapshotOptionsConfigurator(GuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }

        public void Configure(SnapshotOptions options)
        {
            options.Guid = _guidGenerator.Guid;
        }
    }
}

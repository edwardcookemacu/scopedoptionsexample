namespace WebApplication1.OptionsStuff
{
    public class ScopedOptions
    {
        public string Guid { get; set; }

        public ScopedOptions(GuidGenerator guidGenerator)
        {
            Guid = guidGenerator.Guid;
        }
    }
}

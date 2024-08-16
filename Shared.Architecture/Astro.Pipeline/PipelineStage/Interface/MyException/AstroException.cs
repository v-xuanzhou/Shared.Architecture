
namespace Shared.Architecture.Astro.Pipeline.PipelineStage.Interface.MyException
{
    public class AstroException:Exception
    {
        public AstroException(string messageFormat, params object[] args) : base(string.Format(messageFormat, args)) { }
    }
}

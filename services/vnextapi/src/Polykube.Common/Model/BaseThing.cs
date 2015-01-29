namespace Polykube.Common.Model
{
    [Bond.Schema]
    public class BaseThing
    {
        [Bond.Id(0)]
        public string Name { get; set; }
    }
}
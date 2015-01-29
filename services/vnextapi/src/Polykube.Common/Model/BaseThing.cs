using System;

using Bond;
using Bond.Protocols;
using Bond.IO.Safe;

namespace Polykube.Common.Model
{
    [Bond.Schema]
    public class BaseThing
    {
        [Bond.Id(0)]
        public string Name { get; set; }
    }
}
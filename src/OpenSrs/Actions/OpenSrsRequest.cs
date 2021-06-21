using System.Collections.Generic;
using System.Xml.Linq;

namespace OpenSrs
{
    public abstract class OpenSrsRequest
    {
        private readonly string action;
        private readonly string @object;

        public OpenSrsRequest(string action, string @object)
        {
            this.action = action;
            this.@object = @object;
        }

        public virtual XElement ToXml()
        {
            return new XElement("OPS_envelope",
                new XElement("header",
                    new XElement("version", "0.9")
                ),
                new XElement("body",
                    new XElement("data_block",
                        new XElement("dt_assoc",
                            new XElement("item", new XAttribute("key", "protocol"), "XCP"),
                            new XElement("item", new XAttribute("key", "action"), this.action),
                            new XElement("item", new XAttribute("key", "object"), this.@object),

                            GetAttributes()
                        )
                    )
                )
            );
        }

        public virtual Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object>();
        }

        public virtual XElement GetAttributes() => new ("item", new XAttribute("key", "attributes"), Util.ToDtAssoc(GetParameters()));
    }
}
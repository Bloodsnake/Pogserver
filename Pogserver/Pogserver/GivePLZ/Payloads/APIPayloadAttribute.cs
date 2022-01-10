using System;

namespace Pogserver.GivePLZ.Payloads
{
    [AttributeUsage(AttributeTargets.Class)]
    public class APIPayloadAttribute : Attribute
    {
        public string Name { get; set; }
        public string GivenPath { get; set; }
        public double Version { get; set; }
        public APIPayloadAttribute(string name, string path, double version)
        {
            this.Name = name;
            this.Version = version;
            this.GivenPath = path;
        }
        public APIPayloadAttribute(string name, double version)
        {
            this.Name = name;
            this.Version = version;
        }
    }
}

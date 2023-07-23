using System;

namespace BeymenGroupCase.Configuration
{
    public class ConfigurationModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string ApplicationName { get; set; }

        public new Type GetType()
        {
            return Type switch
            {
                "String" => typeof(string),
                "Boolean" => typeof(bool),
                "Int" => typeof(int),
                "Decimal" => typeof(decimal),
                _ => typeof(string),
            };
        }
    }
}

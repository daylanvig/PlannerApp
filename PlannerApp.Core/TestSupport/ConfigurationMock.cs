using AutoFixture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace Shared.TestSupport
{
    public class ConfigurationMock : IConfiguration
    {
        private Dictionary<string, string> values;
        private Fixture fixture;
        public ConfigurationMock()
        {
            fixture = TestFixture.Create();
            values = new Dictionary<string, string>
            {
                { "JwtSecurityKey", "iN5SFN2PGc0JI2yEbuqzcl1GEJ0JCMrAMt6nZzIo" },
                {"JwtExpiryInDays", "30" }
            };
            
        }

        public string this[string key] {

            get {
                if (values.ContainsKey(key)){
                    return values[key];
                }
                return fixture.Create<string>(); 
            } 
            set => throw new NotImplementedException(); }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}

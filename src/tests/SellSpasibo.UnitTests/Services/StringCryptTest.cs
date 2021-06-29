using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Options;
using SellSpasibo.Core.Services;
using Xunit;

namespace SellSpasibo.UnitTests.Services
{
    public class StringCryptTest
    {
        private readonly IStringCrypt _stringCrypt;
        public StringCryptTest()
        {
            var options = Options.Create(new StringCryptOptions
            {
                Key = "my_key"
            });
            var mockLogger = new Mock<ILogger<StringCrypt>>();
                
            _stringCrypt = new StringCrypt(options);
        }

        [Fact]
        public void Input_Fasol_Output_Fasol()
        {
            const string expected = "fasol";
            var cipher =  _stringCrypt.Encrypt(expected);
            var actual =  _stringCrypt.Decrypt(cipher);
            
            Assert.Equal(expected, actual);
        }
    }
}
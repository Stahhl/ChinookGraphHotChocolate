using Chinook.Common;
using System;
using Xunit;

namespace Chinook.Test
{
    public class AppSettingsTests
    {
        [Fact]
        public void ConnectionStringTest_01()
        {
            var result = AppSettingsManager.Settings["ConnectionStrings:Chinook"];

            Assert.False(string.IsNullOrEmpty(result));
        }
    }
}

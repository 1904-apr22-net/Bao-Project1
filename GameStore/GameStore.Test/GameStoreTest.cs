using System;
using Xunit;

namespace GameStore.Test
{
    public class GameStoreTest
    {
        [Fact]
        public void Test1()
        {
            string x = "hello";
            Assert.Equal("hello", x);
        }
    }
}

using MemoryGame.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class RandomServiceUnitTests
    {
        private Random a = new Random();

        IRandomService _randomService = new RandomService();
        [TestMethod]
        public void GetRandom_Exclude_IsNotIncluded_Test()
        {
            var res = _randomService.GetRandoms(null, 0, 15, 3, new[] { 2, 4, 8 });

            Assert.IsFalse(res.Contains(2));
            Assert.IsFalse(res.Contains(4));
            Assert.IsFalse(res.Contains(8));
        }

        [TestMethod]
        public void GetRandom_Count_Is_3_Test()
        {
            var count = 3;
            var res = _randomService.GetRandoms(null, 0, 15, count, new[] { 2, 4, 8 });

            Assert.IsTrue(res.Count == count);

        }
    }
}

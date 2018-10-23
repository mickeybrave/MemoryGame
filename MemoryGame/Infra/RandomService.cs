using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.Infra
{
    public interface IRandomService
    {
        int GetRandom(int min, int max);
        List<int> GetRandoms(List<int> randomList, int min, int max, int count, int[] exclude);
    }
    public class RandomService : IRandomService
    {
        private Random _random = new Random();



        public List<int> GetRandoms(List<int> randomList, int min, int max, int count, int[] exclude)
        {
            if (randomList == null) randomList = new List<int>();
            var number = _random.Next(min, max);

            if (!randomList.Contains(number) && !exclude.Contains(number))
                randomList.Add(number);

            if (randomList.Count < count)
                return GetRandoms(randomList, min, max, count, exclude);
            else
                return randomList;
        }

        public int GetRandom(int min, int max)
        {
            return GetRandoms(null, min, max, 1, new int[] { }).FirstOrDefault();
        }
    }
}

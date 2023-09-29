using System;

namespace HelperScripts
{
    public class WeightedRandom
    {
        private int totalNumberOfWeights = 0;
        private Weight[] weights = null;

        private Random random = new Random();

        public WeightedRandom(Weight[] objects)
        {
            UpdateWeights(objects);
        }

        public void UpdateWeights(Weight[] objects)
        {
            weights = objects;
            for (int i = 0; i < objects.Length; i++)
            {
                totalNumberOfWeights += objects[i].weight;
            }
        }

        public T GetRandomObject<T>()
        {
            int rand = random.Next(totalNumberOfWeights);
            for (int i = 0; i < weights.Length; i++)
            {
                rand -= weights[i].weight;
                if(rand <= 0)
                {
                    return (T)weights[i].obj;
                }
            }
            return (T)weights[weights.Length - 1].obj;
        }
    }

    public class Weight
    {
        public Weight(object obj, int weight)
        {
            this.obj = obj;
            this.weight = weight;
        }
        public object obj;
        public int weight;
    }
}
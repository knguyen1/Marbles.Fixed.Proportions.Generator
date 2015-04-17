using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace QuickTest
{
    public class Marbles
    {
        static void Main(string[] args)
        {
            int red, green, blue, orange;

            if (args.Length > 0)
            {
                red = int.Parse(args[0]);
                blue = int.Parse(args[1]);
                green = int.Parse(args[2]);
                orange = int.Parse(args[3]);
            }
            else
            {
                red = PromptInt("Ratio red marbles?");
                blue = PromptInt("Ratio blue marbles?");
                green = PromptInt("Ratio green marbles?");
                orange = PromptInt("Ratio orange marbles?");
            }


            ////debug
            //int red, green, blue, orange;
            //red = 10;
            //green = 5;
            //blue = 5;
            //orange = 1;

            int[] results = Solve(red, green, blue, orange, 1000);

            WriteOutStats(results);
            Console.ReadLine();
        }

        private static readonly Random mRandom = new Random(); // use to generate random color marble

        //public const int RED_MARBLE = 1;
        //public const int BLUE_MARBLE = 2;
        //public const int GREEN_MARBLE = 3;
        //public const int ORANGE_MARBLE = 4;

        public enum MyMarbles
        {
            Red,
            Green,
            Blue,
            Orange
        }

        /// <summary>
        /// Takes in the proportion of marbles and randomly generates an array of marbles, must
        /// hold the proportion in a fixed manner.  We place proportion of marbles in a "master bucket".
        /// Then we uniformly pick from the master bucket at a random manner.
        /// </summary>
        /// <param name="red">Proportion of red marbles.</param>
        /// <param name="green">Proportion of green marbles.</param>
        /// <param name="blue">Proportion of blue marbles.</param>
        /// <param name="orange">Proportion of orange marbles.</param>
        /// <param name="count">Total count of marbles to be returned.</param>
        /// <returns>An array of randomly generated marbles.</returns>
        public static int[] Solve(int red, int green, int blue, int orange, int count)
        {
            //INSTRUCTIONS:
            // You should then *randomly* generate [count] number of marbles based on that probability                                   
            // (i.e. if you were passed the values 10, 5, 5, 1 for the red, green, blue and orange parameters respectively
            // you should have approximately 10 red marbles for every 5 green and for every five blue marbles, and
            // there should 10 red marbles for approximately every orange marble you get).  Note: if you do this right
            // you should *not* get the same exact results when you run, even if you enter the same input parameters.

            //get the greatest common factor.  the reason we need this is because (20, 10, 5, 5) would
            //behave the exact same as (4, 2, 1, 1) but the smaller list is faster
            int gcf = GreatestCommonFactor(new[] { red, green, blue, orange });

            //get the corrected distribution
            red /= gcf;
            green /= gcf;
            blue /= gcf;
            orange /= gcf;

            //create tuples from the colour and their (corrected) distribution
            var marbles = Enum.GetValues(typeof(MyMarbles)).Cast<MyMarbles>().ToList();
            int[] distributions = { red, green, blue, orange };
            var marblesWithDistribution = marbles.Zip(distributions, (m, d) => new { Marble = m, Distribution = d });

            //add marbles to the master bucket, we'll to pick from this bucket at uniform probability
            List<MyMarbles> masterBucket = new List<MyMarbles>();

            foreach(var md in marblesWithDistribution)
            {
                for (int i = 0; i < md.Distribution; i++)
                    masterBucket.Add(md.Marble);
            }

            //now, we randomly (uniformly) pick from this bucket for [count] times
            int[] resultBucket = new int[count];

            //add it to the result bucket
            for (int i = 0; i < count; i++)
            {
                int randomIndex = mRandom.Next(masterBucket.Count);
                int pickedMarble = (int)masterBucket[randomIndex];
                resultBucket[i] = pickedMarble;
            }

            return resultBucket;
        }

        public static void WriteOutStats(int[] results)
        {
            // TODO: output the total number of red, green, blue and orange marbles based on the array of results passed into you.
            // This array is the same array you generated in the Solve function above.            

            //get a list of all colors grouped by count
            var groupedResult = from marbles in results
                                group marbles by marbles into g
                                orderby g.Count() descending, g.Key
                                select new { Color = g.Key, Count = g.Count() };

            Console.WriteLine("Here are your results:\n");

            //display the colours and the counts
            foreach (var color in groupedResult)
                Console.WriteLine("Color: {0}, Count: {1}", (MyMarbles)color.Color, color.Count);

            // Also, separately output the number of red marbles in the first 100 marbles in your array.
            var redsInTopHundred = Array.FindAll(results.Take(100).ToArray(), s => s.Equals((int)MyMarbles.Red));

            if (redsInTopHundred == null || redsInTopHundred.Length == 0)
                Console.WriteLine("No reds in top 100 found.");
            else
                Console.WriteLine("\nCount of {0} marbles in top 100: {1}", (MyMarbles)redsInTopHundred[0], redsInTopHundred.Count());

        }

        public static int PromptInt(string message)
        {
            int ret = -1;
            while (true)
            {
                Console.WriteLine(message);
                string str = Console.ReadLine();
                if (Int32.TryParse(str, out ret))
                    break;
                else
                    Console.WriteLine("'{0}' is invalid", str);
            }
            return ret;
        }

        /// <summary>
        /// Returns the greatest common factor from an array of N numbers.
        /// </summary>
        /// <param name="numbers">The array of N numbers.</param>
        /// <returns>The greatest common factor.</returns>
        public static int GreatestCommonFactor(int[] numbers)
        {
            return numbers.Aggregate(GreatestCommonFactor);
        }

        public static int GreatestCommonFactor(int a, int b)
        {
            return b == 0 ? a : GreatestCommonFactor(b, a % b);
        }
    }
}
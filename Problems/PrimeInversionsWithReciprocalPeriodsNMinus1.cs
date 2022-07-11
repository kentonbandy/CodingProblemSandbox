using System.Text;

namespace CodingProblemSandbox.Problems
{
    internal static class PrimeInversionsWithReciprocalPeriodsNMinus1
    {
        public static void Run()
        {
            List<int> primes = PrimeSieve(1000);
            Console.WriteLine(CyclicMinus1Counter(primes));
        }

        /*
        Problem

        Prime numbers greater than 2 all have repeating decimals when inverted.

        Example: 7 is a prime number. 1/7 in decimal is 0.142857 repeating.

        The reciprocal (the repeating part) is 6 figures long.

        7 is an example of this where the decimal repeats 6 times. Not every prime number has this property.

        n-1 where n is the prime number is also the maximum number a digit can repeat in this way. The next
        example would be 17 with 16 repeating digits: 0.0588235294117647

        Write a public static method called FindCyclicPrimes that returns an integer count of the number of
        prime numbers under 1,000 that their reciprocal periods are of length one less than the prime number.
        Read more here: https://en.wikipedia.org/wiki/Repeating_decimal#Fractions_with_prime_denominators 

        Video that inspired this problem: https://youtu.be/DmfxIhmGPP4
        */

        /// <summary>
        /// Returns a list of all prime numbers up to and including the passed int.
        /// </summary>
        private static List<int> PrimeSieve(int max)
        {
            List<int> primes = new();
            if (max < 2) return primes;
            primes.Add(2);

            for (int i = 3; i <= max; i += 2)
            {
                if (primes.TrueForAll(n => i % n != 0)) primes.Add(i);
            }

            return primes;
        }

        /// <summary>
        /// Counts the number of ints in the given list whose inversion has a reciprocal period of n - 1
        /// </summary>
        private static int CyclicMinus1Counter(List<int> ints)
        {
            return ints.Aggregate(0, (a, b) => IsCyclicNMinus1(b) ? a + 1 : a);
        }

        private static bool IsCyclicNMinus1(int i, int min = 3)
        {
            if (i < min) return false;
            int divisor = 1;

            // we don't need to save the quotient in memory; we simply need to find the first recurrence of divisor 1 and see if this happens in i steps
            for (int j = 1; j <= i; j++)
            {
                if (divisor / i > 0)
                {
                    divisor %= i;
                    if (divisor == 1) return j == i;
                }
                divisor *= 10;
            }
            return false;
        }
    }
}

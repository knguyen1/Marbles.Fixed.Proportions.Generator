using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    //extension for the enum class, we need this to create the Zip() tuple function
    public static class Enumerable
    {
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> tuple)
        {
            var ie1 = first.GetEnumerator();
            var ie2 = second.GetEnumerator();

            while (ie1.MoveNext() && ie2.MoveNext())
                yield return tuple(ie1.Current, ie2.Current);
        }
    }
}

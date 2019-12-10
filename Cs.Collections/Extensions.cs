using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs.Collections
{
    public class IEnumberableExtensionException : Exception
    {
        public IEnumberableExtensionException(Exception inner)
            : base("IEnumerable Extension Error", inner)
        {
        }

        public IEnumberableExtensionException(string message)
            : base(message)
        {
        }

        public IEnumberableExtensionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public static class IEnumerable
    {
        /// <summary>
        /// Method converts the IEnumerable from the current type to the type specified
        /// </summary>
        /// <typeparam name="TOut">type - return type</typeparam>
        /// <typeparam name="TIn">type - current type</typeparam>
        /// <param name="enumerable">enumerable to convert</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TOut> CovertTo<TOut, TIn>(this IEnumerable<TIn> enumerable)
        {
            try
            {
                return enumerable.Cast<TOut>();
            }
            catch (Exception ex)
            {
                throw new IEnumberableExtensionException(ex);
            }
        }

        /// <summary>
        /// Method performs the specified method on each value of the IEnumerable and return the modified IEnumerable
        /// Example:
        ///   double[] myArray = floatArray.ForEach(val => (double)val)
        /// </summary>
        /// <typeparam name="TOut">type - return type</typeparam>
        /// <typeparam name="TIn">type - current type</typeparam>
        /// <param name="enumerable">enumerable to convert</param>
        /// <param name="operation">operation to run</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TOut> ForEach<TOut, TIn>(this IEnumerable<TIn> enumerable, Func<TIn, TOut> operation)
        {
            try
            {
                List<TOut> list = new List<TOut>();
                foreach (TIn value in enumerable)
                    list.Add(operation(value));

                return list;
            }
            catch (Exception ex)
            {
                throw new IEnumberableExtensionException(ex);
            }
        }

        /// <summary>
        /// Method get the subarray specified
        /// </summary>
        /// <typeparam name="T">type - Ienumerable</typeparam>
        /// <param name="enumerable">main IEnumerable</param>
        /// <param name="index">start index</param>
        /// <param name="length">length of sub IEnumerable</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> SubArray<T>(this IEnumerable<T> enumerable, int index, int length)
        {
            try
            {
                T[] array = enumerable.ToArray();
                T[] subArray = new T[length];
                Array.Copy(array, index, subArray, 0, length);
                return subArray;
            }
            catch (Exception ex)
            {
                throw new IEnumberableExtensionException(ex);
            }
        }


        public static IEnumerable<string> RemoveEmpty(this IEnumerable<string> values)
        {
            try
            {
                List<string> list = values.ToList();
                list.RemoveAll(s => s.Equals(""));
                return list;
            }
            catch (Exception ex)
            {
                throw new IEnumberableExtensionException(ex);
            }
        }
    }
}

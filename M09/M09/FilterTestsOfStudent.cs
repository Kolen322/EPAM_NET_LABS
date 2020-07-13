using CommandLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M09
{
    /// <summary>
    /// Represent the class which filter collection of TestOfStudent class
    /// </summary>
    public static class FilterTestsOfStudent
    {
        /// <summary>
        /// Filter the collection with the specified flags
        /// </summary>
        /// <param name="args">The args of console</param>
        /// <param name="collectionsOfTests">The collection to filter</param>
        /// <returns>The filtered collection</returns>
        public static IEnumerable<TestOfStudent> Filter(IEnumerable<TestOfStudent> collectionOfTests, string[] args)
        {
            if (collectionOfTests == null)
                throw new ArgumentNullException(nameof(collectionOfTests));
            IEnumerable<TestOfStudent> filterCollection = collectionOfTests;
            Parser.Default.ParseArguments<Flags>(args)
                 .WithParsed<Flags>(o =>
                 {
                     if (!string.IsNullOrEmpty(o.Name))
                     {
                         filterCollection = filterCollection.Where((test) => test.NameOfStudent == o.Name);
                     }
                     if (o.MinMark != 0)
                     {
                         filterCollection = filterCollection.Where((test) => test.Assessment >= o.MinMark);
                     }
                     if (o.MaxMark != 0)
                     {
                         filterCollection = filterCollection.Where((test) => test.Assessment <= o.MaxMark);
                     }
                     if (!string.IsNullOrEmpty(o.DateFrom))
                     {
                         filterCollection = filterCollection.Where((test) => test.Date >= DateTime.Parse(o.DateFrom));
                     }
                     if (!string.IsNullOrEmpty(o.DateTo))
                     {
                         filterCollection = filterCollection.Where((test) => test.Date < DateTime.Parse(o.DateTo));
                     }
                     if (!string.IsNullOrEmpty(o.Test))
                     {
                         filterCollection = filterCollection.Where((test) => test.Name == o.Test);
                     }
                     if (o.Sort.Count != 0)
                     {
                         filterCollection = Sort(filterCollection, o.Sort);
                     }
                 });
            return filterCollection;
        }

        private static IEnumerable<TestOfStudent> Sort(IEnumerable<TestOfStudent> collectionOfTests, IList<string> flags)
        {
            if (collectionOfTests == null)
                throw new ArgumentNullException(nameof(collectionOfTests));
            IEnumerable<TestOfStudent> filterCollection = collectionOfTests;
            switch (flags[0])
            {
                case "name":
                    filterCollection = flags[1] == "asc" ? filterCollection.OrderBy(test => test.NameOfStudent) : filterCollection.OrderByDescending(test => test.NameOfStudent);
                    break;
                case "date":
                    filterCollection = flags[1] == "asc" ? filterCollection.OrderBy(test => test.Date) : filterCollection.OrderByDescending(test => test.Date);
                    break;
                case "mark":
                    filterCollection = flags[1] == "asc" ? filterCollection.OrderBy(test => test.Assessment) : filterCollection.OrderByDescending(test => test.Assessment);
                    break;
                case "test":
                    filterCollection = flags[1] == "asc" ? filterCollection.OrderBy(test => test.Name) : filterCollection.OrderByDescending(test => test.Name);
                    break;
                default: throw new ArgumentException("Unexpected value, available order by (name, date, mark, test)");
            }
            return filterCollection;
        }
    }
}

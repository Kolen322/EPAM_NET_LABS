using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace M09
{
    /// <summary>
    /// Represent the class which contains flags
    /// </summary>
    class Flags
    {
        /// <summary>
        /// The name flag
        /// </summary>
        [Option("name", Required = false, HelpText = "Set the name flag.")]
        public string Name { get; set; }
        /// <summary>
        /// The min mark flag
        /// </summary>
        [Option("minmark", Required = false, HelpText = "Set the minmark flag.")]
        public int MinMark { get; set; }
        /// <summary>
        /// The max mark flag
        /// </summary>
        [Option("maxmark", Required = false, HelpText = "Set the maxmark flag.")]
        public int MaxMark { get; set; }
        /// <summary>
        /// The date from flag
        /// </summary>
        [Option("datefrom", Required = false, HelpText = "Set the datefrom flag.")]
        public string DateFrom { get; set; }
        /// <summary>
        /// The date to flag
        /// </summary>
        [Option("dateto", Required = false, HelpText = "Set the dateto flag.")]
        public string DateTo { get; set; }
        /// <summary>
        /// The name of test flag
        /// </summary>
        [Option("test", Required = false, HelpText = "Set the name's subject of test")]
        public string Test { get; set; }
        /// <summary>
        /// The sort flag
        /// </summary>
        [Option("sort", Required = false, HelpText = "Sort the collection ex. --sort name asc")]
        public IList<string> Sort { get; set; }
    }
}

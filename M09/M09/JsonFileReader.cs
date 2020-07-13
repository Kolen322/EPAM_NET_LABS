using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M09
{
    /// <summary>
    /// Represent the class which read information from json file
    /// </summary>
    public class JsonFileReader : IDisposable
    {
        private readonly TextReader _textReader;
        /// <summary>
        /// Initialize a new instance of JsonFileReader class with the specified TextReader
        /// </summary>
        /// <param name="textReader">The instance of class TextReader</param>
        public JsonFileReader(TextReader textReader)
        {
            _textReader = textReader;
        }
        /// <summary>
        /// Read from .json file and return information into collection
        /// </summary>
        /// <returns>The collection of TestOfStudent class</returns>
        public ICollection<TestOfStudent> ReadFromJson()
        {
            ICollection<TestOfStudent> collection = null;
            try
            {
                string json = _textReader.ReadToEnd();
                collection = JsonConvert.DeserializeObject<List<TestOfStudent>>(json);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _textReader.Dispose();
            }
            return collection;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _textReader.Dispose();
        }

    }
}

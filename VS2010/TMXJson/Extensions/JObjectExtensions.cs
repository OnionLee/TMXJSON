using System.IO;
using System;

namespace Newtonsoft.Json.Linq
{
    public static class JObjectExtensions
    {
        /// <summary>
        /// Parses a JObject from a filename
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static JObject FromFile(this JObject jsonObject, string filename)
        {
            //Santiy Check
            if (!File.Exists(filename)) throw new FileNotFoundException(String.Format("The file: {0} was not found. Check the path and try again!", filename));

            string fileText = File.ReadAllText(filename);

            JObject result = JObject.Parse(fileText);

            return result;
        }
    }
}

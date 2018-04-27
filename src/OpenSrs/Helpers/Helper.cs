using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace OpenSrs
{
    public static class Util
    {
        public static T ParseEnum<T>(string text, bool ignoreCase = false)
        {
            return (T)Enum.Parse(typeof(T), text, ignoreCase);
        }

        public static string ComputeMD5Hash(string text)
        {
            var data = Encoding.UTF8.GetBytes(text);

            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);

                var sb = new StringBuilder(32);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string ReadString(this Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static IDictionary<string, object> ObjectToDictionary(object instance)
        {
            return instance
                .GetType()
                .GetTypeInfo()
                .GetProperties()
                .ToDictionary(
                    /*keySelector*/ p => p.Name,
                    /*elementSelector*/ p => p.GetValue(instance, null)
                );
        }

        public static XElement ToDtAssoc(object parameters)
        {
            return ToDtAssoc(ObjectToDictionary(parameters));
        }

        public static XElement ToDtAssoc(IDictionary<string, object> parameters)
        {
            var rootEl = new XElement("dt_assoc");

            foreach (var item in parameters)
            {
                if (item.Value != null)
                {
                    rootEl.Add(new XElement("item", new XAttribute("key", item.Key), item.Value));
                }
            }

            return rootEl;
        }

        public static XElement ToDtArray(string[] items)
        {
            var element = new XElement("dt_array");

            int i = 0;

            foreach (var item in items)
            {
                element.Add(new XElement("item", new XAttribute("key", i.ToString()), item));

                i++;
            }

            return element;
        }

        public static XElement ToDtArray(IEnumerable<IDtEl> items)
        {
            var arrayEl = new XElement("dt_array");

            int i = 0;

            foreach (var item in items)
            {
                arrayEl.Add(new XElement("item", new XAttribute("key", i.ToString()), item.ToDtAssoc()));

                i++;
            }

            return arrayEl;
        }
    }

    public interface IDtEl
    {
        XElement ToDtAssoc();
    }
}
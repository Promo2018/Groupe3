using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BoVoyages.Model
{
    public class Properties
    {
        private static Properties props = null;

        // XML Property keys
        public static readonly string HOST = "Host";
        public static readonly string DATABASE = "Database";

        private string propertyFile = "../../Properties/BoVoyages.xml";
        private List<string> keys = new List<string>();
        private List<string> values = new List<string>();

        private Properties()
        {
            using (XmlReader reader = XmlReader.Create(propertyFile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if(reader.Name == DATABASE)
                        {
                            if (reader.Read())
                            {
                                keys.Add(DATABASE);
                                values.Add(reader.Value.Trim());
                            }
                        } else if (reader.Name == HOST)
                        {
                            if (reader.Read())
                            {
                                keys.Add(HOST);
                                values.Add(reader.Value.Trim());
                            }
                        }
                    }
                }
            }

        }

        public static Properties getInstance()
        {
            if(props == null)
            {
                props = new Properties();
            }
            return props;
        }

        public string getProperty(string key)
        {
            string value = "";
            for(int i = 0; i < keys.Count; i++)
            {
                if(keys[i] == key)
                {
                    value = values[i];
                }
            }
            return value;
        }
    }
}

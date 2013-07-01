using System;

namespace XmlTablify
{
	class MainClass
	{
		public static void Main(string[] args)
		{
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: XmlTablify <XmlFile> <ConfigFile>");
                return;
            }
            string xmlPath = args[0];
            string configPath = args[1];

            using (Config.Configuration config = new Config.Configuration(configPath))
            {
                Parser.Run(xmlPath, config.Jobs, config.PrintStatusNodeCount);
            }
		}
	}
}

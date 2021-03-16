using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // read one by one
            var appSettingsDict = new Dictionary<string, string>();
            appSettingsDict.Add("TempFilePath", ConfigurationManager.AppSettings["TempFilePath"]);
            appSettingsDict.Add("UserName", ConfigurationManager.AppSettings["UserName"]);
            appSettingsDict.Add("SystemEmailAddress", ConfigurationManager.AppSettings["SystemEmailAddress"]);
            appSettingsDict.Add("ServerIPAddress", ConfigurationManager.AppSettings["ServerIPAddress"]);
            Console.WriteLine("appSettings one by one:");
            foreach (var item in appSettingsDict)
            {
                Console.WriteLine($"{item.Key} {item.Value}");
            }

            var connectionStringsDict = new Dictionary<string, string>();
            connectionStringsDict.Add("Default", ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
            connectionStringsDict.Add("CustomerDB", ConfigurationManager.ConnectionStrings["CustomerDB"].ConnectionString);
            connectionStringsDict.Add("AuthDB", ConfigurationManager.ConnectionStrings["AuthDB"].ConnectionString);
            Console.WriteLine("connectionStrings one by one:");
            foreach (var item in connectionStringsDict)
            {
                Console.WriteLine($"{item.Key} {item.Value}");
            }
            Console.WriteLine("");

            // iterate through
            OutputAllAppConfigValues();
            Console.WriteLine("");

            // save new values then iterate through
            var config = ConfigurationManager.OpenExeConfiguration(GetExecutingAssemblyLocation());
            foreach (var key in config.AppSettings.Settings.AllKeys)
            {
                config.AppSettings.Settings[key].Value = $"new {config.AppSettings.Settings[key].Value}";
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            OutputAllAppConfigValues();

            Console.ReadLine();
        }

        private static void OutputAllAppConfigValues()
        {
            Console.WriteLine("all appSettings:");
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                Console.WriteLine($"{key} {ConfigurationManager.AppSettings[key]}");
            }
            Console.WriteLine("all connectionStrings:");
            foreach (var cnn in ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>())
            {
                Console.WriteLine($"{cnn.Name} {cnn.ConnectionString}");
            }
        }

        private static string GetExecutingAssemblyLocation()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            return Uri.UnescapeDataString(uri.Path);
        }
    }
}
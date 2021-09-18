using System;
using System.IO;
using Avalonia;
using Avalonia.ReactiveUI;
using Newtonsoft.Json;

namespace Todo
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {

            ReadConfig();
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
            WriteConfig();
        }
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        public static void ReadConfig()
        {
            var HomePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var ConfigPath = HomePath + "/.TodoCrossPlatform";
            if (Directory.Exists(ConfigPath) == false)
            {
                Directory.CreateDirectory(ConfigPath);
            }
            var ConfigFile = ConfigPath + "/.Cache.ini";
            if(!File.Exists(ConfigFile))
            {
                var stream = File.Create(ConfigFile);
                stream.Close();
            }
            StreamReader sr = new StreamReader(ConfigFile);
            var text = sr.ReadToEnd();
            sr.Close();
            try
            {
              GlobalValue.Instance = JsonConvert.DeserializeObject<GlobalValue>(text);
            }
            catch {}
            if (GlobalValue.Instance == null)
            {
                GlobalValue.Instance = new GlobalValue();
            }
        }

        public static void WriteConfig()
        {
            var HomePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var ConfigPath = HomePath + "/.TodoCrossPlatform";
            if (Directory.Exists(ConfigPath) == false)
            {
                Directory.CreateDirectory(ConfigPath);
            }
            var ConfigFile = ConfigPath + "/.Cache.ini";
            if(!File.Exists(ConfigFile))
            {
                File.Create(ConfigFile);
            }
            StreamWriter sw = new StreamWriter(ConfigFile);
            var text = JsonConvert.SerializeObject(GlobalValue.Instance);
            sw.Write(text);
            sw.Close();
        }
    }
}
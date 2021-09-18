using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Todo
{
    public class GlobalValue
    {
        public static GlobalValue Instance = new GlobalValue { };
        public AuthResponse? AuthInfo { get; set; }
    }

    public class LaunchArg
    {
        public static string? Code { get; set; }
    }


    public class AuthResponse
    {
        public string? AccessToken { get; set; }

        public string? RefreshTokn { get; set; }

        public string? Scope { get; set; }
        public int ExpiresIn { get; set; }
    }

    public class Util {
        public static void OpenBrowser(string url)
        {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                }
        }
    }
}
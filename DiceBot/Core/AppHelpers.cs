using System.Reflection;

namespace DiceBot
{


    public class AppHelpers
    {

        public const string AppName = "DICEBot by WinMachine";

        public static string AppVersion
        {
            get
            {
                return string.Format("{0}", Assembly.GetEntryAssembly().GetName().Version);
            }
        }

        public const string DateTimeCounterFormat = @"d'D, 'hh\:mm\:ss";

        public static string AppFullName
        {
            get
            {
                return AppName + " [" + AppVersion + "] PREVIEW";
            }
        }

    }

}

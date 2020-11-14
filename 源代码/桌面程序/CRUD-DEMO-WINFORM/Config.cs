using System;

namespace HelpUtil
{
    public class Config
    {
        //private static String connectionStr = "SERVER = 127.0.0.1;DATABASE = SCHOOL;USER = sa; PASSWORD = 123456;";
        private static String connectionStr = "SERVER=.;DATABASE=SCHOOL;USER=sa;PASSWORD=123456;";

        public static String ConnectionStr
        {
            get { return Config.connectionStr; }
            set { Config.connectionStr = value; }
        }
    }
}

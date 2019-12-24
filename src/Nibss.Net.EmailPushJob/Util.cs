using Parkway.Scheduler.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailPushJob
{
    public class Util : JobDataProvider
    {
        public Util(JobExecutionContext context) : base(context)
        {

        }



        public bool IsLiveMode
        {
            get
            {
                return Convert.ToBoolean(GetConfig("IsLiveMode"));
            }
        }
      


        public string EmailServiceUrl
        {
            get
            {
                return GetConfig("EmailServiceUrl");
            }
        }

        public string InfoBeepServiceUrl
        {
            get
            {
                return GetConfig("InfoBeepServiceUrl");
            }
        }

        public string InfoBeepUserName
        {
            get
            {
                return GetConfig("InfoBeepUserName");
            }
        }
        public string InfoBeepPassword
        {
            get
            {
                return GetConfig("InfoBeepPassword");
            }
        }


        public string ChunkSize
        {
            get
            {
                return GetConfig("chunksize");
            }
        }

        public int Flag
        {
            get
            {
                return Convert.ToInt32(GetConfig("Flag"));
            }
        }




        public Boolean UseProxy
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(GetConfig("UseProxy"));
                }
                catch
                {
                    return false;
                }
            }
        }
        public Boolean RequireProxyAuthentication
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(GetConfig("RequireProxyAuthentication"));
                }
                catch
                {
                    return true;
                }
            }
        }

        public string ProxyIP
        {
            get
            {
                return GetConfig("ProxyIP");
            }
        }

        public string ProxyPort
        {
            get
            {
                return GetConfig("ProxyPort");
            }
        }

        public string DbConnectionString
        {
            get
            {
                return GetConfig("DbConnectionString");
            }
        }

       
    }
}


using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCSE.Framework.Services.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SCSE.Costs.Test
{
    public class HelperConnectionContext
    {
        private static HelperConnectionContext ConnectionContext = null;
        private ServicesConnectionContext ServicesConnectionContext;
        public IOptions<ServicesConnectionContext> ServicesConnection;
        public static HelperConnectionContext ConnectionContextInstance
        {
            get
            {
                if (ConnectionContext == null)
                {
                    ConnectionContext = new HelperConnectionContext();
                }

                return ConnectionContext;
            }
        }

        public HelperConnectionContext()
        {
            ServicesConnectionContext = new ServicesConnectionContext();
            try
            {
                using (StreamReader ServicesSettingsReader = new StreamReader(@"c:\servinte\ServicesSettings.json"))
                {
                    try
                    {
                        string ServicesSettingsString = ServicesSettingsReader.ReadToEnd();
                        var ServicesSettingsDeserialize = (JObject)JsonConvert.DeserializeObject(ServicesSettingsString);
                        string ServicesSettingsSession = ServicesSettingsDeserialize["ServicesConnectionContext"].Value<object>().ToString();
                        JsonTextReader ServicesSettingsJson = new JsonTextReader(new StringReader(ServicesSettingsSession));
                        JsonSerializer serializer = new JsonSerializer();
                        ServicesConnectionContext = serializer.Deserialize<ServicesConnectionContext>(ServicesSettingsJson);
                        this.ServicesConnection = Options.Create(this.ServicesConnectionContext);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
     
        }


        public void OptionsServicesConnectionContext2()
        {
            using (StreamReader ServicesSettingsReader = new StreamReader(@"c:\servinte\ServicesSettings.json"))
            {
                string ServicesSettingsString = ServicesSettingsReader.ReadToEnd();
                var ServicesSettingsDeserialize = (JObject)JsonConvert.DeserializeObject(ServicesSettingsString);
                string ServicesSettingsSession = ServicesSettingsDeserialize["ServicesConnectionContext"].Value<object>().ToString();
                JsonTextReader ServicesSettingsJson = new JsonTextReader(new StringReader(ServicesSettingsSession));
                JsonSerializer serializer = new JsonSerializer();
                this.ServicesConnectionContext = serializer.Deserialize<ServicesConnectionContext>(ServicesSettingsJson);
                this.ServicesConnection = Options.Create(this.ServicesConnectionContext);
            }
        }
    }
}

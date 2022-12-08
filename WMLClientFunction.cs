using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net.Http;
using System.Net.Security;
using ADSLoanDecision;

namespace WMLClientFunction
{
    
    public class WMLClientFunction
    {
        private static RestClient client = new RestClient("http://159.122.183.223:32086");
                                                                   
        public static JObject Main(JObject args)
        {
            //Process input schema

           
            //Create WML RestRequest

            RestRequest WMLClientFunctionRequest = new RestRequest("/api/titanicml", Method.POST);
            WMLClientFunctionRequest.RequestFormat = DataFormat.Json;
            WMLClientFunctionRequest.AddHeader("Content-Type", "application/json");
            WMLClientFunctionRequest.AddHeader("Accept", "*/*");
            WMLClientFunctionRequest.AddHeader("Connection", "keep-alive");
            //WMLClientFunctionRequest.AddHeader("Authorization", "Basic " + "ZHJzOmVDcWRGWUtmZFlId1JXR0w=");

            //Create WML Request object and populate data
                       

            //Populate values


            WMLRequest msgWMLRequest = new WMLRequest();

            msgWMLRequest.pcclass = 2;
            msgWMLRequest.sex = "female";
            msgWMLRequest.age = 24;
            msgWMLRequest.parch = 1;
            msgWMLRequest.sibsp = 2;
            msgWMLRequest.fare = 27;


            //Add Body to RestRequest


            string wmlrequestbody;
            wmlrequestbody = JsonConvert.SerializeObject(msgWMLRequest);
            WMLClientFunctionRequest.AddJsonBody(wmlrequestbody);

            //Call WML API Post Operation

            //Execute POST request

            RestResponse msgResponse;

            string content = "";

            try
            {

                System.Net.ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
                delegate
                {
                    return true;
                });

                var msgWMLResponse = client.Execute(WMLClientFunctionRequest);
                //WMLResponse 
                
                // JObject objResp = (JObject)client.Execute(LoanRiskrequest);
                msgResponse = (RestResponse)msgWMLResponse;
                content = msgResponse.Content.ToString();

                //Optional - Get values from Response

                var myJObject = JObject.Parse(content);

                return myJObject;


            }
            catch (Exception ex)
            {
                
                WMLResponse errorResponse = new WMLResponse();
                errorResponse.probabilty = 0;
                errorResponse.prediction = 0;
                errorResponse.description = ex.Message;

                content = JsonConvert.SerializeObject(errorResponse);
                var myJObject = JObject.Parse(content);

                return myJObject;
            }

        }
    }
}

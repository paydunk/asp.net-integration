using System;
using System.IO;
using System.Net;

public partial class WebApiClass : System.Web.UI.Page
    {
    protected void Page_Load(object sender, EventArgs e)
        {
            //update the following 2 lines with your ID and Secret!
            string client_id = "fwGq5O7jTCMcW26fzzJ9DnsjgnXps5C8HioF5hz0";
            string client_secret = "tN9h0GRAaKEd6wKeos87Gq2TagdyezsS7PubYI2X";
			
			string uuid = "12345";
            uuid = Request["transaction_uuid"];
            WebApiClass obj = new WebApiClass();
            obj.CallAPI("https://api.paydunk.com/api/v1/transactions", uuid, client_id, client_secret);
        }
        //Returns the JSON string of the webresponse
        public string CallAPI(string URL, string uuid, string ClientKey, string SecretKey)
        {
            HttpWebRequest myHttpWebRequest = null;   
            HttpWebResponse myHttpWebResponse = null;
            try
            {
                string status = "error";
                if (uuid != "")
                    status = "success";
                URL = String.Format("{0}/{1}?client_id={2}&client_secret={3}&status={4}", URL, uuid, ClientKey, SecretKey, status);
                //Create Request
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                myHttpWebRequest.Method = "PUT";
                //Get Response
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                return JSONResponse(myHttpWebResponse);
            }
            catch (WebException myException)
            {
                return JSONResponse((HttpWebResponse)myException.Response);
            }
            finally
            {
                myHttpWebRequest = null;
                myHttpWebResponse = null;
            }
        }
        string JSONResponse(HttpWebResponse webResponse)
        {
            StreamReader rdr = new StreamReader(webResponse.GetResponseStream());
            string strResponse = rdr.ReadToEnd();
            rdr.Close();
            return strResponse;
        }
    }

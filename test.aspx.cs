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
			/* process your payment here, using the following information POSTED from paydunk:
					
			-"expiration_date": "01/20", // string - expiration date on card. Format: MM/YY
			-"card_number": "1111111111111111", //- string - credit card number
			-"cvv": "123", // string - 3-4 digit code found on back of card
			-"shipping_first_name": "Rolo", // string - first name
			-"shipping_last_name": "Tomassi", // string - last name
			-"shipping_email": "rolo.tomassi@test.com", // string - email
			-"shipping_address_1": "Some Address", // string - address line one
			-"shipping_address_2": "A", // string (optional) - address line two
			-"shipping_city": "New York", // string - city name
			-"shipping_state": "NY", // string - state abbreviation
			-"shipping_zip": "10006" // string - 5 digit postal code
			-"billing_first_name": "Kaiser", // string - first name
			-"billing_last_name": "Sose", // string - last name
			-"billing_email": "kaiser.sose@test.com", // string - email
			-"billing_phone": "1-111-1111", // string - phone
			-"billing_address_1": "Some Addres", // string - address line one
			-"billing_address_2": "B", // string (optional) - address line two
			-"billing_city": "New York", // string - city name
			-"billing_state": "NY", // string - state abbreviation
			-"billing_zip": "10006" // string - 5 digit postal code
			-"email": "test@test.com", // string - email address of paydunk user associated with transaction
			-"transaction_uuid": "2e6bde48-ad8d-11e4-9b26-b8e856352ede" // string - 36 digit uuid number of the transaction
			-"order_number": "14324" // string - order number created by merchant site in order to perform transaction
			*/
					
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

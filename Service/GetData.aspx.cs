using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Service_GetData : System.Web.UI.Page
{

    public class InquiryParam
    {
        public string TYPE { get; set; }
        public string ID { get; set; }
        public string KEY { get; set; }
    }

    public class DataResponse
    {
        public string CODE { get; set; }
        public string STATUS { get; set; }
        public string MESSAGE { get; set; }
        public Object DATA { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string sCallerIP;
        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
        {
            sCallerIP = Request.ServerVariables["REMOTE_ADDR"];
        }
        else
        {
            sCallerIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }

        string auth = Request.Headers["KEY"];
        //string param = Request.QueryString[""];

        sendData(null, sCallerIP, auth);
    }

    private void sendData(InquiryParam param, string sCallerIP, string auth)
    {
        Response.ContentType = "application/json";
        DataResponse response = new DataResponse();
        response.CODE = "00";
        response.STATUS = "OK";
        response.MESSAGE = "OK";
        //response.DATA = new DataTable();

        string token = "";

        if (auth != null)
        {
            try
            {
                //token = CUtility.ConvertSHa1("REQUEST" + param.ID + auth);
                //if (token.ToUpper() != param.KEY.ToUpper())
                //{
                //    response.CODE = "01";
                //    response.STATUS = "Error";
                //    response.MESSAGE = "KEY not valid";
                //    //CUtility.ErrorLog("Hario", "KEY tidak sesuai", param.EMAIL, "AccountRegister");
                //}
                //else
                //{
                    string dtResult = "";
                    dtResult = CUtility.Get_DataDummy();
                    response.DATA = JsonConvert.DeserializeObject(dtResult);
                //}
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        else
        {
            response.CODE = "01";
            response.STATUS = "Error";
            response.MESSAGE = "Incorrect Authorization";
        }

        Response.Write(JsonConvert.SerializeObject(response));
    }
}
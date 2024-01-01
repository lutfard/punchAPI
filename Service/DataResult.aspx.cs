using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Collections;

public partial class Service_DataResult : System.Web.UI.Page
{
    public class Param
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
        public DataTable DATA { get; set; }
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

        string auth = Request.Headers["Authorization"];

        Param param = new Param();
        param.TYPE = string.IsNullOrEmpty(Request.Form["TYPE"]) ? "" : Request.Form["TYPE"];
        param.ID = string.IsNullOrEmpty(Request.Form["ID"]) ? "" : Request.Form["ID"];
        sendData(param, sCallerIP, "req");


    }

    private Param getParam()
    {
        Param param = new Param();
        param.TYPE = string.IsNullOrEmpty(Request.Form["TYPE"]) ? "" : Request.Form["TYPE"];
        param.ID = string.IsNullOrEmpty(Request.Form["ID"]) ? "" : Request.Form["ID"];

        return param;
    }

    private void sendData(Param param, string sCallerIP, string auth)
    {
        Response.ContentType = "application/json";
        DataResponse response = new DataResponse();
        response.CODE = "00";
        response.STATUS = "OK";
        response.MESSAGE = "OK";
        response.DATA = new DataTable();

        string token = "";

        if (auth != null)
        {
            try
            {
                token = CUtility.ConvertSHa1("REQUEST" + param.ID + auth);
                if (token.ToUpper() != param.KEY.ToUpper())
                {
                    response.CODE = "01";
                    response.STATUS = "Error";
                    response.MESSAGE = "KEY not valid";
                    //CUtility.ErrorLog("Hario", "KEY tidak sesuai", param.EMAIL, "AccountRegister");
                }
                else
                { 
                    DataTable dtResult = new DataTable();
                    dtResult = CUtility.Get_UserProfile(param.TYPE, param.ID);
                    response.DATA = dtResult;
                }
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
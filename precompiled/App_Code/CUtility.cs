using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Linq;
using Newtonsoft.Json;

public class CUtility
{
    public CUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string ConvertSHa1(string str)
    {
        System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
        byte[] buffer = encoder.GetBytes(str);
        SHA1CryptoServiceProvider cryptoTransformSHA1 =
        new SHA1CryptoServiceProvider();
        string hash = BitConverter.ToString(
        cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

        return hash.ToLower();
    }

    public static string Get_UserProfile(string type, string id)
    {
        string data = "";
        Object dtOutput = new Object();
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("NAME", typeof(string));
        dt.Columns.Add("GENDER", typeof(string));
        dt.Columns.Add("AGE", typeof(int));
        dt.Columns.Add("ADDRESS", typeof(string));

        dt.Rows.Add("01", "Lutfi", "L", 25, "Jaksel");
        dt.Rows.Add("02", "Ardi", "L", 20, "Bekasi");
        dt.Rows.Add("03", "Fiar", "L", 29, "Depok");
        dt.Rows.Add("01", "Maya", "P", 25, "Bogor");
        dt.Rows.Add("02", "Ayuning", "P", 20, "Tangerang");
        dt.Rows.Add("03", "Tyas", "P", 29, "Serang");

        var user = dt.AsEnumerable()
            .Where(row => row.Field<string>("ID") == id && row.Field<string>("GENDER") == type)
            .FirstOrDefault();

        dtOutput = user.ItemArray;
        //data = JsonConvert.SerializeObject(dtOutput, Formatting.Indented);
        data = JsonConvert.SerializeObject(dtOutput);

        return data;

    }

    public static string Get_DataDummy()
    {
        string data = "";
        Object dtOutput = new Object();
        DataTable dt = new DataTable();
        dt.Columns.Add("AXIS_X", typeof(double));
        dt.Columns.Add("AXIS_Y", typeof(double));
        dt.Columns.Add("AXIS_Z", typeof(double));
        dt.Columns.Add("SPEED", typeof(double));

        dt.Rows.Add(4870, 0, 0.28, 25);
        dt.Rows.Add(4679, 0, 0.28, 27);
        dt.Rows.Add(4987, 0, 0.28, 23);
        dt.Rows.Add(4800, 0, 0.28, 21);

        //var user = dt.AsEnumerable()
        //    .Where(row => row.Field<string>("ID") == id && row.Field<string>("GENDER") == type)
        //    .FirstOrDefault();

        //dtOutput = user.ItemArray;

        data = JsonConvert.SerializeObject(dt);

        return data;

    }
}
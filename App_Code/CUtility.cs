using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Linq;

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

    public static DataTable Get_UserProfile(string type, string id)
    {
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

        dt.Rows.Clear();
        dt.ImportRow(user);

        return dt;

    }
}
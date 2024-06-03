using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CoreApi.Common.Models
{
    public class CommonFunction
    {

        #region "Common Functions"
        public string[] GetJobColumnData(DataTable dtColumn)
        {
            //string[] columnNames = dtColumn.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

            string[] arr1 = new string[dtColumn.Columns.Count];


            string strWidth = string.Empty;

            for (int i = 0; i < dtColumn.Columns.Count; i++)
            {
                //arr2[i] += "$";
                arr1[i] += "{";
                arr1[i] += "'text':'" + dtColumn.Columns[i] + "',";
                arr1[i] += "'datafield':'" + dtColumn.Columns[i] + "',";
                strWidth = GetColumnWidth(dtColumn.Columns[i].DataType.ToString());

                if (Convert.ToString(dtColumn.Columns[i]).Substring(0, 1) == "0")
                {
                    //arr1[i] += "'width':'0',";
                    arr1[i] += "'hidden' : 'true'";
                }
                else
                {
                    arr1[i] += "'width':'" + strWidth + "'";
                }

                //{ name: "0nJobtypeid", width: 0, css: "hide" },

                arr1[i] += "}";
            }

            return arr1;

            //string JSONColumn = string.Empty;
            //JSONColumn = JsonConvert.SerializeObject(arr1) ;
            //return JSONColumn; 
        }

        public string GetJobColumnDataType(DataTable dtColumn)
        {
            var JSONString = new StringBuilder();
            string strType = string.Empty;

            JSONString.Append(" },{'datatype\' :[");
            for (int i = 0; i < dtColumn.Columns.Count; i++)
            {

                JSONString.Append("{");

                if (i < dtColumn.Columns.Count)
                {
                    strType = GetColumnType(dtColumn.Columns[i].DataType.ToString());
                    JSONString.Append("'name':" + "\'" + dtColumn.Columns[i] + "\', 'type':" + "\'" + strType + "\'");
                }


                if (i == dtColumn.Columns.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("},");
                }
            }
            JSONString.Append("]");

            return JSONString.ToString();
        }
        // function to return row data
        public string GetJsonFromDT(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("}, {'rows\' :[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\'" + table.Columns[j].ColumnName.ToString() + "\':" + "\'" + table.Rows[i][j].ToString() + "\',");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\'" + table.Columns[j].ColumnName.ToString() + "\':" + "\'" + table.Rows[i][j].ToString() + "\'");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }


        public DataTable GetDynamicDT(DataTable dt, string strHideColumns)
        {

            //********** hide columns
            //hide columns
            if (strHideColumns.ToString() != "")
            {
                var myarray = strHideColumns.Split(',');

                for (var i = 0; i < myarray.Length; i++)
                {
                    var a = myarray[i];
                    var b = Convert.ToInt32(a);
                    dt.Columns.RemoveAt(b);
                }
            }
            //********** hide column

            return dt;
        }

        /// <summary>
        /// Encrypt password using AES Symmetric key (Same key) algorithm
        /// </summary>
        /// <param name="clearText">password</param>
        /// <returns> string</returns>
        public DataTable Encrypt(string clearText, string EncryptionKey)
        {
            string salt = "";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                #pragma warning disable
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] buffer = new byte[1024];
                rng.GetBytes(buffer);
                salt = BitConverter.ToString(buffer);

                //Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, Encoding.ASCII.GetBytes(salt));
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add(clearText);
            dt.Columns.Add(salt);
            return dt;
        }
        /// <summary>
        /// using for validate password encryption.
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="EncryptionKey"></param>
        /// <param name="saltText"></param>
        /// <returns></returns>
        public string validateEncryption(string clearText, string EncryptionKey, string saltText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                //Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, Encoding.ASCII.GetBytes(saltText));
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Decrypt password using AES Symmetric key (Same key) algorithm
        /// </summary>
        /// <param name="clearText">Encrypted password</param>
        /// <returns> string</returns>
        public string Decrypt(string cipherText, string EncryptionKey, string saltText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, Encoding.ASCII.GetBytes(saltText));
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        /// Utility Decrypt password using AES Symmetric key (Same key) algorithm
        /// </summary>
        /// <param name="clearText">Encrypted password</param>
        /// <returns> string</returns>
        public string DecryptionforUtility(string cipherText, string EncryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


        #endregion

        #region "Private Functions"
        public string GetColumnWidth(string strDataWidth)
        {
            string strWidth = string.Empty;

            switch (strDataWidth)
            {
                case "System.Int64":
                    strWidth = "200";

                    break;
                case "System.Int32":
                    strWidth = "100";

                    break;
                case "System.bigint":
                    strWidth = "200";

                    break;

                case "System.text":
                    strWidth = "250";

                    break;
                case "System.DateTime":
                    strWidth = "200";

                    break;
                case "System.String":
                    strWidth = "250";

                    break;
                case "System.Decimal":
                    strWidth = "100";

                    break;
                default:
                    break;
            }
            return strWidth;
        }

        private string GetColumnType(string strDataType)
        {

            string strType = string.Empty;
            switch (strDataType)
            {
                case "System.Int32":
                    strType = "int";
                    break;

                case "System.text":
                    strType = "string";
                    break;

                case "System.bigint":
                    strType = "int";
                    break;

                case "System.Int64":
                    strType = "int";
                    break;

                case "System.DateTime":
                    strType = "DateTime";
                    break;

                case "System.String":
                    strType = "string";
                    break;
                case "System.Decimal":
                    strType = "int";
                    break;

                default:
                    break;
            }
            return strType;
        }


        #endregion

        /// <summary>
        /// Common function to convert null int value to zero.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 ConvertInt32(object value)
        {
            if (value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

    }

	#region "Enum Extension Methods"
    public static class EnumExtensionMethods
    {
        /// <summary>
        /// Gets the Description of Enum
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns>String value of Enum Description</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(enumValue));
        }
        /// <summary>
        /// Get Integer value of Enum of Specific Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns>Integer value of respexctive Enum</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int GetEnumValueByDescription<T>(this string description) where T : Enum
        {
            foreach (Enum enumItem in Enum.GetValues(typeof(T)))
            {
                if (enumItem.GetEnumDescription() == description)
                {
                    return Convert.ToInt32(enumItem);
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }

        /// <summary>
        /// Get Enum of Specific Type by description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns>Integer value of respexctive Enum</returns>
        /// <exception cref="ArgumentException"></exception>
        public static T GetEnumByDescription<T>(this string description) where T : Enum
        {
            foreach (Enum enumItem in Enum.GetValues(typeof(T)))
            {
                if (enumItem.GetEnumDescription() == description)
                {
                    return (T)enumItem;
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }
        #endregion
    }
    public class objectResponse
    {
        public int rId { get; set; } // requestId
        public object? data { get; set; } // return Object as a API response data
    }

}

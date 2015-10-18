using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;

namespace Pet2Share_Web.BL
{
    public sealed class FormatText
    {
        private static SymmetricAlgorithm encryption;
        private const string password = "this@is_@retWrsd!!3232";
        private const string Mkey = "@SepdtR01212$2!#";


        public const string CommonThumbnail = "?width=100";
        public const string Thumbnail400px = "?width=400";

        private static void Init()
        {
            encryption = new RijndaelManaged();
            var key = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes(Mkey));

            encryption.Key = key.GetBytes(encryption.KeySize / 8);
            encryption.IV = key.GetBytes(encryption.BlockSize / 8);
            encryption.Padding = PaddingMode.PKCS7;
        }

        public static void Encrypt(Stream inStream, Stream OutStream)
        {
            Init();
            var encryptor = encryption.CreateEncryptor();
            inStream.Position = 0;
            var encryptStream = new CryptoStream(OutStream, encryptor, CryptoStreamMode.Write);
            inStream.CopyTo(encryptStream);
            encryptStream.FlushFinalBlock();
        }


        public static void Decrypt(Stream inStream, Stream OutStream)
        {
            Init();
            var encryptor = encryption.CreateDecryptor();
            inStream.Position = 0;
            var encryptStream = new CryptoStream(inStream, encryptor, CryptoStreamMode.Read);
            encryptStream.CopyTo(OutStream);
            OutStream.Position = 0;
        }


        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                //using (var fileStream = File.OpenWrite(fileName))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize(stream, serializableObject);
                        stream.Position = 0;

                        //MemoryStream encryptedStream = new MemoryStream();

                        //Encrypt(stream, encryptedStream);

                        //var bytes = new byte[encryptedStream.Length];

                        //stream.Read(bytes, 0, (int)encryptedStream.Length);

                        //fileStream.Write(bytes, 0, bytes.Length);

                        xmlDocument.Load(stream);
                        xmlDocument.Save(fileName);
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }


        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

        private static readonly FormatText instance = new FormatText();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static FormatText()
        {
        }

        private FormatText()
        {
        }

        public static FormatText Instance
        {
            get
            {
                return instance;
            }
        }

        //public class FormatText
        //{
        public string AppPath()
        {
            //get
            //{
            string path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
            return path.EndsWith("/") ? path : path + "/";
            //}
        }

        public TimeSpan? GetLocalTime(TimeSpan? ts)
        {
            if (ts != null)
            {
                TimeSpan tsDiff = DateTime.Now.Subtract(DateTime.UtcNow);

                ((TimeSpan)ts).Add(tsDiff);
            }

            return null;
        }

        public TimeSpan? GetUniversalTime(TimeSpan? ts)
        {
            if (ts != null)
            {
                TimeSpan tsDiff = DateTime.UtcNow.Subtract(DateTime.Now);

                ((TimeSpan)ts).Add(tsDiff);
            }

            return null;
        }

        public string ClientIP()
        {
            string clientIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(clientIp))
            {
                string[] forwardedIps = clientIp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                clientIp = forwardedIps[forwardedIps.Length - 1];
            }
            else
            {
                clientIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return clientIp;
        }

        public string FormatDate(object Date)
        {
            if (Date != null)
            {
                DateTime temp;

                if (DateTime.TryParse(Date.ToString(), out temp))
                {
                    return temp.ToString("dd MMM yy");
                }
                else
                {
                    return "Invalid Date";
                }
            }
            else
            {
                return "N/A";
            }

        }

        public string FormatGridText(object Column)
        {
            if (Column != null)
            {
                return Column.ToString();
            }
            else
            {
                return "N/A";
            }

        }

        public string FormatGridNumber(object Column)
        {
            if (Column != null)
            {
                return Column.ToString();
            }
            else
            {
                return "0.00";
            }

        }

        public int ParseInt(object value)
        {
            if (value == null)
            {
                return 0;
            }

            int val = 0;

            int.TryParse(value.ToString(), out val);

            return val;
        }



        public string FormatNotes(string notes, int? Length)
        {

            int length = 30;

            if (Length != null)
            {
                length = Length ?? 0;
            }

            if (notes.Length < length)
            {
                return notes;
            }
            else
            {
                return notes.Substring(0, length - 3) + "...";
            }
        }

        public string GetStars(int stars)
        {
            string starsSend = "";

            if (stars == 0)
            {
                return "<img src='../images/gray_star.png' width='15' height='14' /><img src='../images/gray_star.png' width='15' height='14' /><img src='../images/gray_star.png' width='15' height='14' /><img src='../images/gray_star.png' width='15' height='14' /><img src='../images/gray_star.png' width='15' height='14' />";
            }

            else
            {

                for (int i = 0; i < stars; i++)
                {
                    starsSend += "<img src='../images/green_star.png' width='15' height='14' />";
                }
                for (int i = 0; i < 5 - stars; i++)
                {
                    starsSend += "<img src='../images/gray_star.png' width='15' height='14' />";
                }
                return starsSend;
            }

        }

        //public void RegisterScript(Control page, string message, ResponseType resp)
        //{
        //    ScriptManager.RegisterStartupScript(page, typeof(Page), Guid.NewGuid().ToString(), "$.prompt('" + message.Replace("'", "").Replace("\r\n", "<Br/>") + "');", true);
        //}

        //public void RegisterBaseScript(Control page, string message)
        //{
        //    ScriptManager.RegisterStartupScript(page, typeof(Page), Guid.NewGuid().ToString(), "alert('" + message.Replace("'", "").Replace("\r\n", "<Br/>") + "');", true);
        //}

        //public void RegisterScript(string guid, Control page, string message)
        //{
        //    ScriptManager.RegisterStartupScript(page, typeof(Page), guid, "$.prompt('" + message.Replace("'", "").Replace("\r\n", "<Br/>") + "');", true);
        //}

        //public void RegisterScript(Control page, string Script)
        //{
        //    ScriptManager.RegisterStartupScript(page, typeof(Page), Guid.NewGuid().ToString(), Script, true);
        //}

        //public void RegisterScript(Control page, string message, string RedirectToPage)
        //{
        //    string script = "$.prompt('" + message.Replace("'", "").Replace("\r\n", "<Br/>") + "', {callback:function(){ window.location='" + AppPath() + RedirectToPage.TrimStart('/') + "'}});";

        //    ScriptManager.RegisterStartupScript(page, typeof(Page), Guid.NewGuid().ToString(), script, true);
        //}


        public string AddOrdinal(int num)
        {
            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num.ToString() + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num.ToString() + "st";
                case 2:
                    return num.ToString() + "nd";
                case 3:
                    return num.ToString() + "rd";
                default:
                    return num.ToString() + "th";
            }

        }

        public string FormatTime(int hours)
        {
            if (hours < 24)
            {
                return hours.ToString() + " hr"; ;
            }
            else
            {
                return (Math.Floor(Convert.ToDouble(hours / 24))).ToString() + "<b>d</b> " + (hours % 24).ToString() + " hr";
            }

        }

        public string FormatCurrency(decimal? value)
        {

            value = Math.Abs(value ?? 0);

            if (value == null)
            {
                return "0";
            }
            else
            {
                return (Math.Truncate(Convert.ToDouble(value) * 100) / 100).ToString();
            }
        }

        public string GetValidImage(object virtualFilePath, bool UseLabeled_NoUser_AsFallbackImage = false, string thumbOpt = "")
        {
            string fallbackImage = "/Images/NoPet.jpg";
            try
            {

                if (UseLabeled_NoUser_AsFallbackImage)
                {
                    fallbackImage = "/Images/NoPet.jpg";
                }

                if (virtualFilePath == null)
                {
                    return fallbackImage;
                }
                else
                {
                    //Creating the HttpWebRequest
                    HttpWebRequest request = WebRequest.Create(virtualFilePath.ToString()) as HttpWebRequest;
                    //Setting the Request method HEAD, you can also use GET too.
                    request.Method = "HEAD";
                    //Getting the Web Response.
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    //Returns TRUE if the Status code == 200



                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return virtualFilePath + thumbOpt;
                    }
                    else
                    {
                        return fallbackImage;
                    }
                }
            }
            catch
            {
                return fallbackImage;
            }
        }

        public string LinkHtml(string Link)
        {
            return @"<p style=""margin-bottom:15px;"">
            	<a href=""" + Link + @""" style=""color:#2f82de;font-weight:bold;text-decoration:none;"">" + Link.Substring(0, Link.Length > 50 ? 50 : Link.Length) + @"</a></p>
            ";
        }

        public string ParagraphHtml(string Paragraph)
        {
            return @"<p style=""margin-bottom:15px;"">
            	" + Paragraph + @"</p>
            ";
        }


    }

}
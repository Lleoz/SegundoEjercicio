using System;
using System.Security.Cryptography;
using System.Text;

namespace Ejercicio2.Api.Transversal.Common.Tools
{
    public static class EncryptedTool
    {
        private static string Key = @"/#!#K3y73zT*@$@_@$@2o20@$@_@$!#/";

        public static string EncryptToMD5(string toEncrypt)
        {
            var result = string.Empty;

            if (String.IsNullOrWhiteSpace(toEncrypt))
            {
                throw new ArgumentException("toEncrypt cannot be empty");
            }

            try
            {
                using (var hashmd5 = new MD5CryptoServiceProvider())
                {
                    byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
                    byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                    hashmd5.Clear();

                    using (var triplesDES = TripleDES.Create())
                    {
                        triplesDES.Key = keyArray;
                        triplesDES.Mode = CipherMode.ECB;
                        triplesDES.Padding = PaddingMode.PKCS7;

                        using (ICryptoTransform cryptoTransform = triplesDES.CreateEncryptor())
                        {
                            byte[] resultArray = cryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            triplesDES.Clear();
                            result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static string DecryptFromMD5(string toDecrypt)
        {
            var result = string.Empty;

            if (String.IsNullOrWhiteSpace(toDecrypt))
            {
                throw new ArgumentException("toDecrypt cannot be empty");
            }

            try
            {
                using (var hashmd5 = new MD5CryptoServiceProvider())
                {
                    byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
                    byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                    hashmd5.Clear();

                    using (var triplesDES = TripleDES.Create())
                    {
                        triplesDES.Key = keyArray;
                        triplesDES.Mode = CipherMode.ECB;
                        triplesDES.Padding = PaddingMode.PKCS7;

                        using (ICryptoTransform cryptoTransform = triplesDES.CreateDecryptor())
                        {
                            byte[] resultArray = cryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            triplesDES.Clear();
                            result = Encoding.UTF8.GetString(resultArray);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}

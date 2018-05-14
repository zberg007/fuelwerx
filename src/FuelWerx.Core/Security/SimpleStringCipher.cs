using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FuelWerx.Security
{
	public static class SimpleStringCipher
	{
		private readonly static byte[] InitVectorBytes;

		private const int Keysize = 256;

		public const string DefaultPassPhrase = "gsKnGZ041HLL4IM8";

		static SimpleStringCipher()
		{
			SimpleStringCipher.InitVectorBytes = Encoding.ASCII.GetBytes("jkE49230Tf093b42");
		}

		public static string Decrypt(string cipherText, string passPhrase = "gsKnGZ041HLL4IM8")
		{
			string str;
			byte[] numArray = Convert.FromBase64String(cipherText);
			using (PasswordDeriveBytes passwordDeriveByte = new PasswordDeriveBytes(passPhrase, null))
			{
				byte[] bytes = passwordDeriveByte.GetBytes(32);
				using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
				{
					rijndaelManaged.Mode = CipherMode.CBC;
					using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(bytes, SimpleStringCipher.InitVectorBytes))
					{
						using (MemoryStream memoryStream = new MemoryStream(numArray))
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
							{
								byte[] numArray1 = new byte[(int)numArray.Length];
								int num = cryptoStream.Read(numArray1, 0, (int)numArray1.Length);
								str = Encoding.UTF8.GetString(numArray1, 0, num);
							}
						}
					}
				}
			}
			return str;
		}

		public static string Encrypt(string plainText, string passPhrase = "gsKnGZ041HLL4IM8")
		{
			string base64String;
			byte[] bytes = Encoding.UTF8.GetBytes(plainText);
			using (PasswordDeriveBytes passwordDeriveByte = new PasswordDeriveBytes(passPhrase, null))
			{
				byte[] numArray = passwordDeriveByte.GetBytes(32);
				using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
				{
					rijndaelManaged.Mode = CipherMode.CBC;
					using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor(numArray, SimpleStringCipher.InitVectorBytes))
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
							{
								cryptoStream.Write(bytes, 0, (int)bytes.Length);
								cryptoStream.FlushFinalBlock();
								base64String = Convert.ToBase64String(memoryStream.ToArray());
							}
						}
					}
				}
			}
			return base64String;
		}
	}
}
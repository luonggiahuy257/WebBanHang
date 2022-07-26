using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace BanHangOnline.Common
{
    public class func
    {
		// Hàm lấy id product
		public static int GetIdProduct()
		{
            try
            {
				string chars = "0123456789";
				char[] stringChars = new char[9];
				Random random = new Random();

				for (int i = 0; i < stringChars.Length; i++)
				{
					stringChars[i] = chars[random.Next(chars.Length)];
				}

				string finalString = new String(stringChars);
				return Int32.Parse(finalString);
			}
            catch (Exception)
            {
                return 0;
            }
		}
		public static string GetUrlName(string fileName)
		{
			try
			{
				for (int i = 1; i < VietnameseSigns.Length; i++)
				{
					for (int j = 0; j < VietnameseSigns[i].Length; j++)
						fileName = fileName.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
				}

				string StringResult = fileName.ToLower().Trim().Replace(" ","-");

				return StringResult;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static string saveImage(IWebHostEnvironment Environment, List<IFormFile> postedFiles, string pathSaveStr, string pathStr)
		{
			var str = "";

			char[] charsToTrim = { ' ' };
			char[] charsToTrimg = { '-' };
			string wwwPath = Environment.WebRootPath;
			string contentPath = Environment.ContentRootPath;
			string path = Path.Combine(Environment.WebRootPath, pathSaveStr);

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			List<string> uploadedFiles = new List<string>();
			foreach (IFormFile postedFile in postedFiles)
			{
				if (postedFile.Length > 2097152)
				{
					return WebConst.FileSave.MaxLengImg;
				}

				string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
				string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
				using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
				{
					postedFile.CopyTo(stream);
					uploadedFiles.Add(flname);
					str = pathStr + flname;
				}
			}

			return str;
		}

		private static readonly string[] VietnameseSigns = new string[]
		{
			"aAeEoOuUiIdDyY",
			"áàạảãâấầậẩẫăắằặẳẵ",
			"ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
			"éèẹẻẽêếềệểễ",
			"ÉÈẸẺẼÊẾỀỆỂỄ",
			"óòọỏõôốồộổỗơớờợởỡ",
			"ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
			"úùụủũưứừựửữ",
			"ÚÙỤỦŨƯỨỪỰỬỮ",
			"íìịỉĩ",
			"ÍÌỊỈĨ",
			"đ",
			"Đ",
			"ýỳỵỷỹ",
			"ÝỲỴỶỸ"
		};
	}
}




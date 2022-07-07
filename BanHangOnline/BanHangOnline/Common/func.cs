using System;

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

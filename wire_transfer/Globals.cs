/*
 * Created by SharpDevelop.
 * User: IrynaM
 * Date: 06/09/2020
 * Time: 15:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;


namespace wire_transfer
{
	/// <summary>
	/// Class containing global variables neccessary to be accessed by all files in namespace. 
	/// </summary>
	public static class Globals{
		public static int WIRE_ID;
		public static int SENDER_ID;
		public static int RECIPIENT_ID;
        public static int IBANK_ID;
        public static int RBANK_ID;
		public static int INTERNAL_USE_ID;
		
		public static string PDF_PATH_REMITTANCE = @"PDF_FILES\remittance.pdf";
		public static string PDF_PATH_NONREMITTANCE = @"PDF_FILES\nonremittance.pdf";	
		
		public static string COMPUTER_NAME = Environment.MachineName.ToString();
		public static string IP_ADDRESS = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
		public static string EMPLOYEE_USER = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
		
		
		public static string getCurrentDateAndTime(){
			DateTime dt = DateTime.Now;
			string date_time = dt.ToString("MM/dd/yyyy HH:mm:ss");
			return date_time;
		}
		
		public static string getCurrentDate(){
			DateTime dt = DateTime.Now;
			string date = dt.ToString("MM/dd/yyyy");
			return date;
		}
		
		public static string getCurrentTime(){
			DateTime dt = DateTime.Now;
			string time = dt.ToString("HH:mm:ss");
			return time;
		}
		
		public static string getFundsAvailableByDate(){
			DateTime dt = DateTime.Now;
			dt = dt.AddDays(14);
			string date = dt.ToString("MM/dd/yyyy");
			return date;
		}
		
		public static string getFundsAvailableByDateManual(string date){
			DateTime dt = Convert.ToDateTime(date);
			dt = dt.AddDays(14);
			date = dt.ToString("MM/dd/yyyy");
			return date;
		}
	}
}

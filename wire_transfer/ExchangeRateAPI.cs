/*
 * Created by SharpDevelop.
 * User: iryna
 * Date: 05/18/2020
 * Time: 13:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Windows;


namespace wire_transfer
{
	/// <summary>
	/// Description of ExchangeRateAPI.
	/// </summary>
	/// 
	
	class Rates
        {
        public static API_Obj Import()
            {
            try
                {
                String URLString = "https://open.exchangerate-api.com/v6/latest";
                
                using (WebClient wc = new WebClient())
				{
   					var json = wc.DownloadString(URLString);
   					API_Obj json_contents = JsonConvert.DeserializeObject<API_Obj>(json);
   					
 
   					return json_contents;
				}
				
                }
            catch (Exception e)
                {
            	  Debug.WriteLine(e);
//            	  MessageBox.Show("Can't connect to Exchange Rate API! "+e.ToString());
            	  return null;
            	  
            	}
        	}
        }

    public class API_Obj
        {
        public string result { get; set; }
        public string documentation { get; set; }
        public string terms_of_use { get; set; }
        public string time_zone { get; set; }
        public string time_last_update_utc { get; set; }
        public string time_next_update_utc { get; set; }
        public ConversionRate rates { get; set; }
        }

    public class ConversionRate
        {
        public double AED { get; set; }
        public double ARS { get; set; }
        public double AUD { get; set; }
        public double BGN { get; set; }
        public double BRL { get; set; }
        public double BSD { get; set; }
        public double CAD { get; set; }
        public double CHF { get; set; }
        public double CLP { get; set; }
        public double CNY { get; set; }
        public double COP { get; set; }
        public double CZK { get; set; }
        public double DKK { get; set; }
        public double DOP { get; set; }
        public double EGP { get; set; }
        public double EUR { get; set; }
        public double FJD { get; set; }
        public double GBP { get; set; }
        public double GTQ { get; set; }
        public double HKD { get; set; }
        public double HRK { get; set; }
        public double HUF { get; set; }
        public double IDR { get; set; }
        public double ILS { get; set; }
        public double INR { get; set; }
        public double ISK { get; set; }
        public double JPY { get; set; }
        public double KRW { get; set; }
        public double KZT { get; set; }
        public double MXN { get; set; }
        public double MYR { get; set; }
        public double NOK { get; set; }
        public double NZD { get; set; }
        public double PAB { get; set; }
        public double PEN { get; set; }
        public double PHP { get; set; }
        public double PKR { get; set; }
        public double PLN { get; set; }
        public double PYG { get; set; }
        public double RON { get; set; }
        public double RUB { get; set; }
        public double SAR { get; set; }
        public double SEK { get; set; }
        public double SGD { get; set; }
        public double THB { get; set; }
        public double TRY { get; set; }
        public double TWD { get; set; }
        public double UAH { get; set; }
        public double USD { get; set; }
        public double UYU { get; set; }
        public double ZAR { get; set; }
        
        public double getRate(string currency){
        	if (currency == "AED"){
        		return this.AED;
        	}
        	if (currency == "ARS"){
        		return this.ARS;
        	}
        	if (currency == "AUD"){
        		return this.AUD;
        	}
        	if (currency == "BGN"){
        		return this.BGN;
        	}
        	if (currency == "BRL"){
        		return this.BRL;
        	}
        	if (currency == "BSD"){
        		return this.BSD;
        	}
        	if (currency == "CAD"){
        		return this.CAD;
        	}
        	if (currency == "CHF"){
        		return this.CHF;
        	}
        	if (currency == "CLP"){
        		return this.CLP;
        	}
        	if (currency == "CNY"){
        		return this.CNY;
        	}
        	if (currency == "COP"){
        		return this.COP;
        	}
        	if (currency == "CZK"){
        		return this.CZK;
        	}
        	if (currency == "DKK"){
        		return this.DKK;
        	}
        	if (currency == "DOP"){
        		return this.DOP;
        	}
        	if (currency == "EGP"){
        		return this.EGP;
        	}
        	if (currency == "EUR"){
        		return this.EUR;
        	}
        	if (currency == "FJD"){
        		return this.FJD;
        	}
        	if (currency == "GBP"){
        		return this.GBP;
        	}
        	if (currency == "GTQ"){
        		return this.GTQ;
        	}
        	if (currency == "HKD"){
        		return this.HKD;
        	}
        	if (currency == "HRK"){
        		return this.HRK;
        	}
        	if (currency == "HUF"){
        		return this.HUF;
        	}
        	if (currency == "IDR"){
        		return this.IDR;
        	}
        	if (currency == "ILS"){
        		return this.ILS;
        	}
        	if (currency == "INR"){
        		return this.INR;
        	}
        	if (currency == "ISK"){
        		return this.ISK;
        	}
        	if (currency == "JPY"){
        		return this.JPY;
        	}
        	if (currency == "KRW"){
        		return this.KRW;
        	}
        	if (currency == "KZT"){
        		return this.KZT;
        	}
        	if (currency == "MXN"){
        		return this.MXN;
        	}
        	if (currency == "MYR"){
        		return this.MYR;
        	}
        	if (currency == "NOK"){
        		return this.NOK;
        	}
        	if (currency == "NZD"){
        		return this.NZD;
        	}
        	if (currency == "PAB"){
        		return this.PAB;
        	}
        	if (currency == "PEN"){
        		return this.PEN;
        	}
        	if (currency == "PHP"){
        		return this.PHP;
        	}
        	if (currency == "PKR"){
        		return this.PKR;
        	}
        	if (currency == "PLN"){
        		return this.PLN;
        	}
        	if (currency == "PYG"){
        		return this.PYG;
        	}
        	if (currency == "RON"){
        		return this.RON;
        	}
        	if (currency == "RUB"){
        		return this.RUB;
        	}
        	if (currency == "SAR"){
        		return this.SAR;
        	}
        	if (currency == "SEK"){
        		return this.SEK;
        	}
        	if (currency == "SGD"){
        		return this.SGD;
        	}
        	if (currency == "THB"){
        		return this.THB;
        	}
        	if (currency == "TRY"){
        		return this.TRY;
        	}
        	if (currency == "TWD"){
        		return this.TWD;
        	}
        	if (currency == "UAH"){
        		return this.UAH;
        	}
        	if (currency == "USD"){
        		return this.USD;
        	}
        	if (currency == "UYU"){
        		return this.UYU;
        	}
        	if (currency == "ZAR"){
        		return this.ZAR;
        	}
        	else{
        		return 0;
        	}
        }
        
    }

	}
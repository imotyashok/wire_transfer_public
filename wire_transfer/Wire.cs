/*
 * Created by SharpDevelop.
 * User: iryna
 * Date: 05/14/2020
 * Time: 10:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//using System.Data.SQLite;
using System.Data.SqlClient;
using System.Windows;

namespace wire_transfer
{
	/// <summary>
	/// Description of Wire.
	/// </summary>
	public class Wire
	{		
		public string TransferDate {get; set;}
		public string TransferTime {get; set;}
		public string FundsAvailableBy {get; set;}
		
		public string CurrencyInfo {get; set;}
		public string TransAmountUSD {get; set;}
		public string TransFeesUSD {get; set;}
		public string TotalUSD {get; set;}
		public string CurrencyType {get; set;}
		public string ExchangeRate {get; set;}
		public string TransAmountForeign {get; set;}
		public string TotalForeign {get; set;}
		public string PaymentInstr {get; set;}
		public int SenderID {get; set;}
		public int RecipientID {get; set;}
		public int BankID {get; set;}
		
		public string WireType {get; set;} // "R" for Remittance, "NR" for NonRemittance
		public int Status {get; set;}
		public string PickupName {get; set;}
		public string PickupAddress {get; set;}
		public string PickupCityStateZip {get; set;}
		public string PickupPhone {get; set;}
		public int FutureDateBox {get; set;} // 0 for Unchecked, 1 for Checked 
		public string FutureDate1 {get; set;}
		public string FutureDate2 {get; set;}
		public int OneTime {get; set;} // 0 for No, 1 for Yes 
		public int SubjectToTransAgreement {get; set;} // 0 for No, 1 for Yes
		public int InternalUseID {get; set;}
		public string CountryOfReceipt {get; set;}
		public string WireBranch {get; set;}
	
		public string ComputerName {get; set;}
		public string IPAddress {get; set;}
		public string EmployeeUser {get; set;}
		
		public DatabaseHandler db = new DatabaseHandler();
		public SqlCommand cmd;
		
		public int insertInfo(){
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				
				cmd.CommandText = "INSERT INTO wire (transfer_date, transfer_time, funds_available_by, wire_type, status, currency_info, transfer_amount_USD, transfer_fees_USD, total_USD, currency_type, exchange_rate, " +
					"transfer_amount_foreign, total_foreign, payment_instructions, sender_id, recipient_id, bank_id, internal_use_id, country_of_receipt, wire_branch, pickup_name, pickup_address, pickup_city_state_zip, " +
					"pickup_phone, future_date_box, future_date_1, future_date_2, one_time, subject_to_transfer_agreement, computer_name, IP_address, employee_user)" +
					"VALUES (@transfer_date, @transfer_time, @funds_available_by, @wire_type, @status, @currency_info, @transfer_amount_USD, @transfer_fees_USD, @total_USD, @currency_type, " +
					"@exchange_rate, @transfer_amount_foreign, @total_foreign, @payment_instructions, @sender_id, @recipient_id, @bank_id, @internal_use_id, @country_of_receipt, @wire_branch, " +
					"@pickup_name, @pickup_address, @pickup_city_state_zip, @pickup_phone, @future_date_box, @future_date_1, @future_date_2, @one_time, @subject_to_transfer_agreement, @computer_name, @IP_address, @employee_user);" +
					"SELECT SCOPE_IDENTITY();";
				cmd.Parameters.Add(new SqlParameter("@transfer_date", this.TransferDate));
				cmd.Parameters.Add(new SqlParameter("@transfer_time", this.TransferTime));
				cmd.Parameters.Add(new SqlParameter("@funds_available_by", this.FundsAvailableBy));
				cmd.Parameters.Add(new SqlParameter("@wire_type", this.WireType));
				cmd.Parameters.Add(new SqlParameter("@status", this.Status));
				cmd.Parameters.Add(new SqlParameter("@currency_info", this.CurrencyInfo));
				cmd.Parameters.Add(new SqlParameter("@transfer_amount_USD", this.TransAmountUSD));
				cmd.Parameters.Add(new SqlParameter("@transfer_fees_USD", this.TransFeesUSD));
				cmd.Parameters.Add(new SqlParameter("@total_USD", this.TotalUSD));
				cmd.Parameters.Add(new SqlParameter("@currency_type", this.CurrencyType));
				cmd.Parameters.Add(new SqlParameter("@exchange_rate", this.ExchangeRate));
				cmd.Parameters.Add(new SqlParameter("@transfer_amount_foreign", this.TransAmountForeign));
				cmd.Parameters.Add(new SqlParameter("@total_foreign", this.TotalForeign));
				cmd.Parameters.Add(new SqlParameter("@payment_instructions", this.PaymentInstr));
				cmd.Parameters.Add(new SqlParameter("@sender_id", this.SenderID));
				cmd.Parameters.Add(new SqlParameter("@recipient_id", this.RecipientID));
				cmd.Parameters.Add(new SqlParameter("@bank_id", this.BankID));
				cmd.Parameters.Add(new SqlParameter("@internal_use_id", this.InternalUseID));
				cmd.Parameters.Add(new SqlParameter("@country_of_receipt", this.CountryOfReceipt));
				cmd.Parameters.Add(new SqlParameter("@wire_branch", this.WireBranch));
				cmd.Parameters.Add(new SqlParameter("@pickup_name", this.PickupName));
				cmd.Parameters.Add(new SqlParameter("@pickup_address", this.PickupAddress));
				cmd.Parameters.Add(new SqlParameter("@pickup_city_state_zip", this.PickupCityStateZip));
				cmd.Parameters.Add(new SqlParameter("@pickup_phone", this.PickupPhone));
				cmd.Parameters.Add(new SqlParameter("@future_date_box", this.FutureDateBox));
				cmd.Parameters.Add(new SqlParameter("@future_date_1", this.FutureDate1));
				cmd.Parameters.Add(new SqlParameter("@future_date_2", this.FutureDate2));
				cmd.Parameters.Add(new SqlParameter("@one_time", this.OneTime));
				cmd.Parameters.Add(new SqlParameter("@subject_to_transfer_agreement", this.SubjectToTransAgreement));
				cmd.Parameters.Add(new SqlParameter("@computer_name", this.ComputerName));
				cmd.Parameters.Add(new SqlParameter("@IP_address", this.IPAddress));
				cmd.Parameters.Add(new SqlParameter("@employee_user", this.EmployeeUser));
				//cmd.ExecuteNonQuery();
				Int32 id = Convert.ToInt32(cmd.ExecuteScalar());
				return id;
			} catch (Exception e){
				MessageBox.Show(e.ToString());
				return -1;
					
			}
		}
			

		public void updateStatus(string status, string wire_id){
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				cmd.CommandText = "UPDATE wire SET status="+status+" WHERE id="+wire_id+";";
				cmd.ExecuteNonQuery();
			} catch (Exception e){
				MessageBox.Show(e.ToString());
			}
		}
		
		public void updateWire(int wire_id){
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				cmd.CommandText = "UPDATE wire SET transfer_date=@transfer_date, transfer_time=@transfer_time, funds_available_by=@funds_available_by, wire_type=@wire_type, " +
					"status=@status, currency_info=@currency_info, transfer_amount_USD=@transfer_amount_USD, transfer_fees_USD=@transfer_fees_USD, total_USD=@total_USD, currency_type=@currency_type, exchange_rate=@exchange_rate, " +
					"transfer_amount_foreign=@transfer_amount_foreign, total_foreign=@total_foreign, payment_instructions=@payment_instructions, sender_id=@sender_id, recipient_id=@recipient_id, " +
					"bank_id=@bank_id, internal_use_id=@internal_use_id, country_of_receipt=@country_of_receipt, wire_branch=@wire_branch, pickup_name=@pickup_name, pickup_address=@pickup_address, " +
					"pickup_city_state_zip=@pickup_city_state_zip, pickup_phone=@pickup_phone, future_date_box=@future_date_box, future_date_1=@future_date_1, future_date_2=@future_date_2, " +
					"one_time=@one_time, subject_to_transfer_agreement=@subject_to_transfer_agreement, computer_name=@computer_name, IP_address=@IP_address, employee_user=@employee_user " +
					"WHERE id="+wire_id+";";
				cmd.Parameters.Add(new SqlParameter("@transfer_date", this.TransferDate));
				cmd.Parameters.Add(new SqlParameter("@transfer_time", this.TransferTime));
				cmd.Parameters.Add(new SqlParameter("@funds_available_by", this.FundsAvailableBy));
				cmd.Parameters.Add(new SqlParameter("@wire_type", this.WireType));
				cmd.Parameters.Add(new SqlParameter("@status", this.Status));
				cmd.Parameters.Add(new SqlParameter("@currency_info", this.CurrencyInfo));
				cmd.Parameters.Add(new SqlParameter("@transfer_amount_USD", this.TransAmountUSD));
				cmd.Parameters.Add(new SqlParameter("@transfer_fees_USD", this.TransFeesUSD));
				cmd.Parameters.Add(new SqlParameter("@total_USD", this.TotalUSD));
				cmd.Parameters.Add(new SqlParameter("@currency_type", this.CurrencyType));
				cmd.Parameters.Add(new SqlParameter("@exchange_rate", this.ExchangeRate));
				cmd.Parameters.Add(new SqlParameter("@transfer_amount_foreign", this.TransAmountForeign));
				cmd.Parameters.Add(new SqlParameter("@total_foreign", this.TotalForeign));
				cmd.Parameters.Add(new SqlParameter("@payment_instructions", this.PaymentInstr));
				cmd.Parameters.Add(new SqlParameter("@sender_id", this.SenderID));
				cmd.Parameters.Add(new SqlParameter("@recipient_id", this.RecipientID));
				cmd.Parameters.Add(new SqlParameter("@bank_id", this.BankID));
				cmd.Parameters.Add(new SqlParameter("@internal_use_id", this.InternalUseID));
				cmd.Parameters.Add(new SqlParameter("@country_of_receipt", this.CountryOfReceipt));
				cmd.Parameters.Add(new SqlParameter("@wire_branch", this.WireBranch));
				cmd.Parameters.Add(new SqlParameter("@pickup_name", this.PickupName));
				cmd.Parameters.Add(new SqlParameter("@pickup_address", this.PickupAddress));
				cmd.Parameters.Add(new SqlParameter("@pickup_city_state_zip", this.PickupCityStateZip));
				cmd.Parameters.Add(new SqlParameter("@pickup_phone", this.PickupPhone));
				cmd.Parameters.Add(new SqlParameter("@future_date_box", this.FutureDateBox));
				cmd.Parameters.Add(new SqlParameter("@future_date_1", this.FutureDate1));
				cmd.Parameters.Add(new SqlParameter("@future_date_2", this.FutureDate2));
				cmd.Parameters.Add(new SqlParameter("@one_time", this.OneTime));
				cmd.Parameters.Add(new SqlParameter("@subject_to_transfer_agreement", this.SubjectToTransAgreement));
				cmd.Parameters.Add(new SqlParameter("@computer_name", this.ComputerName));
				cmd.Parameters.Add(new SqlParameter("@IP_address", this.IPAddress));
				cmd.Parameters.Add(new SqlParameter("@employee_user", this.EmployeeUser));		
				cmd.ExecuteNonQuery();
			} catch (Exception e){
				MessageBox.Show(e.ToString());
			}
		}
	}
}

/*
 * Created by SharpDevelop.
 * User: iryna
 * Date: 5/15/2020
 * Time: 4:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//using System.Data.SQLite;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;

namespace wire_transfer
{
	/// <summary>
	/// Description of WireHistory.
	/// </summary>
	/// 

	
	public class WireHistory
	{

		public int WireID {get; set;}		
		public string sName {get; set;}
		public string sAddress {get; set;}
		public string sCity {get; set;}
		public string sState {get; set;}
		public string sZip  {get; set;}
		public string sPhoneNum {get; set;}
		public string sMemberNum {get; set;}
		public string sSubAccount {get; set;}
		public string rName {get; set;}
  		public string rAddress {get; set;}
  		public string rCity {get; set;}
  		public string rState {get; set;}
  		public string rZip  {get; set;}
  		public string rCountry {get; set;}
  		public string rPhone {get; set;}
  		public string rAccountNumIBAN  {get; set;}
  		public string rSSN {get; set;}
  		public string rTIN  {get; set;}
  		public string rDLNum {get; set;}
  		public string ibName  {get; set;}
  		public string ibAddress  {get; set;}
  		public string ibCity {get; set;}
  		public string ibState {get; set;}
 	 	public string ibZip {get; set;}
  		public string ibRoutingNum  {get; set;}
  		public string ibSwiftSortCode {get; set;}
  		public string ibBranch {get; set;}
  		public string ibRoutingInstr {get; set;}
  		public string rbName  {get; set;}
  		public string rbAddress  {get; set;}
  		public string rbCity {get; set;}
  		public string rbState {get; set;}
 	 	public string rbZip {get; set;}
  		public string rbRoutingNum  {get; set;}
  		public string rbSwiftSortCode {get; set;}
  		public string rbBranch {get; set;}
  		public string rbRoutingInstr {get; set;}
  		public string DateTime {get; set;}
  		public string TransferDate {get; set;}
  		public string TransferTime {get; set;}
  		public string FundsAvailableBy {get; set;}
  		public string WireType {get; set;}
  		public int Status  {get; set;}
  		public string CountryOfReceipt {get; set;}
  		public string WireBranch {get; set;}
  		public string CurrencyInfo {get; set;}
  		public string TransAmountUSD  {get; set;}
  		public string TransFeesUSD {get; set;}
  		public string TotalUSD {get; set;}
  		public string CurrencyType  {get; set;}
  		public string ExchangeRate  {get; set;}
  		public string TransAmountForeign {get; set;}
  		public string TotalForeign {get; set;}
  		public string PaymentInstr {get; set;}
  		public string PickupName {get; set;}
  		public string PickupAddress {get; set;}
  		public string PickupCityStateZip {get; set;}
  		public string PickupPhone {get; set;}
  		public int FutureDateBox {get; set;}
  		public string FutureDate1 {get; set;}
  		public string FutureDate2 {get; set;}
  		public int OneTime {get; set;}
  		public int SubjectToTransAgreement {get; set;} 		
  		public string MemberConfirmingFunds {get; set;} 
  		public string IDUsed {get; set;} 
  		public string MethodOfTransfer{get; set;} 
  		public string TransactionControlNum {get; set;} 
  		public string WireProcessedBy {get; set;} 
  		public string OFACVerificationBy {get; set;} 
  		public string OFACDateTime {get; set;} 
  		public string InternalSpecialInstructions {get; set;} 
  		public string SecurityMethodUsed {get; set;} 
  		public string SecurityProcessedBy {get; set;} 
  		public string SecurityDateTime {get; set;} 
  		public string EmployeeCallback {get; set;} 
  		public string PhoneNumForCallback {get; set;} 
  		public string SourcePhoneVerification {get; set;} 
  		public string MemberCancellingRequest {get; set;} 
  		public string CancelDate {get; set;} 
  		public string CancelProcessedBy {get; set;} 
  		
  		public string ComputerName {get; set;}
		public string IPAddress {get; set;}
		public string EmployeeUser {get; set;}
		
		public DatabaseHandler db = new DatabaseHandler();
		public SqlCommand cmd;
  		
  		public void insertInfo(){
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				
	  			cmd.CommandText = "INSERT INTO wire_history (wire_id, s_name, s_address, s_city, s_state, s_zip, s_phone_num, " +
	  				"s_member_num, s_sub_account, r_name, r_address, r_city, r_state, r_zip, r_country, r_phone, r_account_num_IBAN, r_SSN, r_TIN, " +
	  				"r_DLnum, ib_name, ib_address, ib_city, ib_state, ib_zip, ib_ABA_routing_num, " +
	  				"ib_swift_sort_code, ib_branch, ib_routing_instructions, rb_name, rb_address, rb_city, rb_state, rb_zip, rb_ABA_routing_num," +
					"rb_swift_sort_code, rb_branch, rb_routing_instructions, datetime, transfer_date, transfer_time, funds_available_by, wire_type, status, country_of_receipt, wire_branch, currency_info, transfer_amount_USD, transfer_fees_USD," +
					"total_USD, currency_type, exchange_rate, trans_amount_foreign, total_foreign, payment_instructions, " +
	  				"pickup_name, pickup_address, pickup_city_state_zip, pickup_phone, future_date_box, future_date_1, future_date_2, one_time, subject_to_transfer_agreement, member_confirming_funds, " +
	  			    "id_used, method_of_transfer, transaction_control_num, wire_processed_by, OFAC_verification_by, OFAC_date_time, internal_special_instructions, security_method_used, security_processed_by, security_date_time, " +
					"employee_callback, phone_num_for_callback, source_phone_verification, member_cancelling_request, cancel_date, cancel_processed_by, computer_name, IP_address, employee_user) " +
	  				"VALUES " +
	  				"(@wire_id, @s_name, @s_address, @s_city, @s_state, @s_zip, @s_phone_num, @s_member_num, @s_sub_account, @r_name, @r_address, @r_city, @r_state, @r_zip, @r_country, @r_phone, " +
	  				"@r_account_num_IBAN, @r_SSN, @r_TIN, @r_DLnum, @ib_name, @ib_address, @ib_city, @ib_state, @ib_zip, " +
	  				"@ib_ABA_routing_num, @ib_swift_sort_code, @ib_branch, @ib_routing_instructions, @rb_name, @rb_address, @rb_city, @rb_state, @rb_zip, " +
	  				"@rb_ABA_routing_num, @rb_swift_sort_code, @rb_branch, @rb_routing_instructions, @datetime, @transfer_date, @transfer_time, @funds_available_by, @wire_type, @status, @country_of_receipt, @wire_branch, " +
	  				"@currency_info, @transfer_amount_USD, @transfer_fees_USD, @total_USD, @currency_type, @exchange_rate, @trans_amount_foreign, @total_foreign, " +
	  				"@payment_instructions, @pickup_name, @pickup_address, @pickup_city_state_zip, @pickup_phone, @future_date_box, @future_date_1, @future_date_2, @one_time, @subject_to_transfer_agreement, @member_confirming_funds, " +
	  				"@id_used, @method_of_transfer, @transaction_control_num, @wire_processed_by, @OFAC_verification_by, @OFAC_date_time, @internal_special_instructions, @security_method_used, @security_processed_by, @security_date_time, " +
					"@employee_callback, @phone_num_for_callback, @source_phone_verification, @member_cancelling_request, @cancel_date, @cancel_processed_by, @computer_name, @IP_address, @employee_user);";
				cmd.Parameters.Add(new SqlParameter("@wire_id", this.WireID));
				cmd.Parameters.Add(new SqlParameter("@s_name", this.sName));
				cmd.Parameters.Add(new SqlParameter("@s_address", this.sAddress));
				cmd.Parameters.Add(new SqlParameter("@s_city", this.sCity));
				cmd.Parameters.Add(new SqlParameter("@s_state", this.sState));
				cmd.Parameters.Add(new SqlParameter("@s_zip", this.sZip));
				cmd.Parameters.Add(new SqlParameter("@s_phone_num", this.sPhoneNum));
				cmd.Parameters.Add(new SqlParameter("@s_member_num", this.sMemberNum));
				cmd.Parameters.Add(new SqlParameter("@s_sub_account", this.sSubAccount));
				cmd.Parameters.Add(new SqlParameter("@r_name", this.rName));
				cmd.Parameters.Add(new SqlParameter("@r_address", this.rAddress));
				cmd.Parameters.Add(new SqlParameter("@r_city", this.rCity));
				cmd.Parameters.Add(new SqlParameter("@r_state", this.rState));
				cmd.Parameters.Add(new SqlParameter("@r_zip", this.rZip));
				cmd.Parameters.Add(new SqlParameter("@r_country", this.rCountry));
				cmd.Parameters.Add(new SqlParameter("@r_phone", this.rPhone));
				cmd.Parameters.Add(new SqlParameter("@r_account_num_IBAN", this.rAccountNumIBAN));
				cmd.Parameters.Add(new SqlParameter("@r_SSN", this.rSSN));
				cmd.Parameters.Add(new SqlParameter("@r_TIN", this.rTIN));
				cmd.Parameters.Add(new SqlParameter("@r_DLnum", this.rDLNum));
				cmd.Parameters.Add(new SqlParameter("@ib_name", this.ibName));
				cmd.Parameters.Add(new SqlParameter("@ib_address", this.ibAddress));
				cmd.Parameters.Add(new SqlParameter("@ib_city", this.ibCity));
				cmd.Parameters.Add(new SqlParameter("@ib_state", this.ibState));
				cmd.Parameters.Add(new SqlParameter("@ib_zip", this.ibZip));
				cmd.Parameters.Add(new SqlParameter("@ib_ABA_routing_num", this.ibRoutingNum));
				cmd.Parameters.Add(new SqlParameter("@ib_swift_sort_code", this.ibSwiftSortCode));
				cmd.Parameters.Add(new SqlParameter("@ib_branch", this.ibBranch));
				cmd.Parameters.Add(new SqlParameter("@ib_routing_instructions", this.ibRoutingInstr));
				cmd.Parameters.Add(new SqlParameter("@rb_name", this.rbName));
				cmd.Parameters.Add(new SqlParameter("@rb_address", this.rbAddress));
				cmd.Parameters.Add(new SqlParameter("@rb_city", this.rbCity));
				cmd.Parameters.Add(new SqlParameter("@rb_state", this.rbState));
				cmd.Parameters.Add(new SqlParameter("@rb_zip", this.rbZip));
				cmd.Parameters.Add(new SqlParameter("@rb_ABA_routing_num", this.rbRoutingNum));
				cmd.Parameters.Add(new SqlParameter("@rb_swift_sort_code", this.rbSwiftSortCode));
				cmd.Parameters.Add(new SqlParameter("@rb_branch", this.rbBranch));
				cmd.Parameters.Add(new SqlParameter("@rb_routing_instructions", this.rbRoutingInstr));
				cmd.Parameters.Add(new SqlParameter("@datetime", this.DateTime));
				cmd.Parameters.Add(new SqlParameter("@transfer_date", this.TransferDate));
				cmd.Parameters.Add(new SqlParameter("@transfer_time", this.TransferTime));
				cmd.Parameters.Add(new SqlParameter("@funds_available_by", this.FundsAvailableBy));
				cmd.Parameters.Add(new SqlParameter("@wire_type", this.WireType));
				cmd.Parameters.Add(new SqlParameter("@status", this.Status));
				cmd.Parameters.Add(new SqlParameter("@country_of_receipt", this.CountryOfReceipt));
				cmd.Parameters.Add(new SqlParameter("@wire_branch", this.WireBranch));
				cmd.Parameters.Add(new SqlParameter("@currency_info", this.CurrencyInfo));
				cmd.Parameters.Add(new SqlParameter("@transfer_amount_USD", this.TransAmountUSD));
				cmd.Parameters.Add(new SqlParameter("@transfer_fees_USD", this.TransFeesUSD));
				cmd.Parameters.Add(new SqlParameter("@total_USD", this.TotalUSD));
				cmd.Parameters.Add(new SqlParameter("@currency_type", this.CurrencyType));
				cmd.Parameters.Add(new SqlParameter("@exchange_rate", this.ExchangeRate));
				cmd.Parameters.Add(new SqlParameter("@trans_amount_foreign", this.TransAmountForeign));
				cmd.Parameters.Add(new SqlParameter("@total_foreign", this.TotalForeign));
				cmd.Parameters.Add(new SqlParameter("@payment_instructions", this.PaymentInstr));
				cmd.Parameters.Add(new SqlParameter("@pickup_name", this.PickupName));
				cmd.Parameters.Add(new SqlParameter("@pickup_address", this.PickupAddress));
			    cmd.Parameters.Add(new SqlParameter("@pickup_city_state_zip", this.PickupCityStateZip));
			    cmd.Parameters.Add(new SqlParameter("@pickup_phone", this.PickupPhone));
			    cmd.Parameters.Add(new SqlParameter("@future_date_box", this.FutureDateBox));
			    cmd.Parameters.Add(new SqlParameter("@future_date_1", this.FutureDate1));
			    cmd.Parameters.Add(new SqlParameter("@future_date_2", this.FutureDate2));
				cmd.Parameters.Add(new SqlParameter("@one_time", this.OneTime));
				cmd.Parameters.Add(new SqlParameter("@subject_to_transfer_agreement", this.SubjectToTransAgreement));
				cmd.Parameters.Add(new SqlParameter("@member_confirming_funds", this.MemberConfirmingFunds));
				cmd.Parameters.Add(new SqlParameter("@id_used", this.IDUsed));
				cmd.Parameters.Add(new SqlParameter("@method_of_transfer", this.MethodOfTransfer));
				cmd.Parameters.Add(new SqlParameter("@transaction_control_num", this.TransactionControlNum));
				cmd.Parameters.Add(new SqlParameter("@wire_processed_by", this.WireProcessedBy));
				cmd.Parameters.Add(new SqlParameter("@OFAC_verification_by", this.OFACVerificationBy));
				cmd.Parameters.Add(new SqlParameter("@OFAC_date_time", this.OFACDateTime));
				cmd.Parameters.Add(new SqlParameter("@internal_special_instructions", this.InternalSpecialInstructions));
				cmd.Parameters.Add(new SqlParameter("@security_method_used", this.SecurityMethodUsed));
				cmd.Parameters.Add(new SqlParameter("@security_processed_by", this.SecurityProcessedBy));
				cmd.Parameters.Add(new SqlParameter("@security_date_time", this.SecurityDateTime));
				cmd.Parameters.Add(new SqlParameter("@employee_callback", this.EmployeeCallback));
				cmd.Parameters.Add(new SqlParameter("@phone_num_for_callback", this.PhoneNumForCallback));
				cmd.Parameters.Add(new SqlParameter("@source_phone_verification", this.SourcePhoneVerification));
				cmd.Parameters.Add(new SqlParameter("@member_cancelling_request", this.MemberCancellingRequest));
				cmd.Parameters.Add(new SqlParameter("@cancel_date", this.CancelDate));
				cmd.Parameters.Add(new SqlParameter("@cancel_processed_by", this.CancelProcessedBy));
				cmd.Parameters.Add(new SqlParameter("@computer_name", this.ComputerName));
				cmd.Parameters.Add(new SqlParameter("@IP_address", this.IPAddress));
				cmd.Parameters.Add(new SqlParameter("@employee_user", this.EmployeeUser));
	  			cmd.ExecuteNonQuery();
			} catch (Exception e){
				MessageBox.Show(e.ToString());	
			}
  		}
  		
  		public WireHistory returnOldWire(int wire_id){
  			db.initializeConnection();
			cmd = db.getCommand();
  			SqlDataReader reader;
  			WireHistory old_wire = new WireHistory();
  			
  			// If there's 2+ wire histories for the same wire, we need to retrieve the latest wire from the wire history
  			cmd.CommandText = "SELECT TOP 1 * FROM wire_history WHERE wire_id="+wire_id.ToString()+" ORDER BY \"datetime\" DESC;";
  			try{
	  			using (reader = cmd.ExecuteReader()){
	  				
					while (reader.Read()){
  						// TODO: fix the numbering in GetString(..) after all new fields have been added
	  					old_wire.WireID = reader.GetInt32(1);
						old_wire.sName = reader.GetString(2);
						old_wire.sAddress = reader.GetString(3);
						old_wire.sCity = reader.GetString(4);
						old_wire.sState = reader.GetString(5);
						old_wire.sZip = reader.GetString(6);
						old_wire.sPhoneNum = reader.GetString(7);
						old_wire.sMemberNum = reader.GetString(8);
						old_wire.sSubAccount = reader.GetString(9);
						old_wire.rName = reader.GetString(10);
						old_wire.rAddress = reader.GetString(11);
						old_wire.rCity = reader.GetString(12);
						old_wire.rState = reader.GetString(13);
						old_wire.rZip = reader.GetString(14);
						old_wire.rCountry = reader.GetString(15);
						old_wire.rPhone = reader.GetString(16);
						old_wire.rAccountNumIBAN = reader.GetString(17);
						old_wire.rSSN = reader.GetString(18);
						old_wire.rTIN = reader.GetString(19);
						old_wire.rDLNum = reader.GetString(20);
						old_wire.ibName = reader.GetString(21);
						old_wire.ibAddress = reader.GetString(22);
						old_wire.ibCity = reader.GetString(23);
						old_wire.ibState = reader.GetString(24);
						old_wire.ibZip = reader.GetString(25);
						old_wire.ibRoutingNum = reader.GetString(26);
						old_wire.ibSwiftSortCode = reader.GetString(27);
						old_wire.ibBranch = reader.GetString(28);
						old_wire.ibRoutingInstr = reader.GetString(29);
						old_wire.rbName = reader.GetString(30);
						old_wire.rbAddress = reader.GetString(31);
						old_wire.rbCity = reader.GetString(32);
						old_wire.rbState = reader.GetString(33);
						old_wire.rbZip = reader.GetString(34);
						old_wire.rbRoutingNum = reader.GetString(35);
						old_wire.rbSwiftSortCode = reader.GetString(36);
						old_wire.rbBranch = reader.GetString(37);
						old_wire.rbRoutingInstr = reader.GetString(38);
						old_wire.DateTime = reader.GetString(39);
						old_wire.TransferDate = reader.GetString(40);
						old_wire.TransferTime = reader.GetString(41);
						old_wire.FundsAvailableBy = reader.GetString(42);
						old_wire.WireType = reader.GetString(43);
						old_wire.CurrencyInfo = reader.GetString(44);
						old_wire.TransAmountUSD = reader.GetString(45);
						old_wire.TransFeesUSD  = reader.GetString(46);
						old_wire.TotalUSD = reader.GetString(47);
						old_wire.CurrencyType = reader.GetString(48);
						old_wire.ExchangeRate = reader.GetString(49);
						old_wire.TransAmountForeign = reader.GetString(50);
						old_wire.TotalForeign = reader.GetString(51);
					    old_wire.Status = reader.GetInt32(52);
					    old_wire.CountryOfReceipt = reader.GetString(53);
					    old_wire.WireBranch = reader.GetString(54);
						old_wire.PaymentInstr = reader.GetString(55);
						old_wire.PickupName = reader.GetString(56);
						old_wire.PickupAddress = reader.GetString(57);
						old_wire.PickupCityStateZip = reader.GetString(58);
						old_wire.PickupPhone = reader.GetString(59);
						old_wire.FutureDateBox = reader.GetInt32(60);
						old_wire.FutureDate1 = reader.GetString(61);
						old_wire.FutureDate2 = reader.GetString(62);
						old_wire.OneTime = reader.GetInt32(63);
						old_wire.SubjectToTransAgreement = reader.GetInt32(64);
						old_wire.MemberConfirmingFunds = reader.GetString(65);
						old_wire.IDUsed = reader.GetString(66);
						old_wire.MethodOfTransfer = reader.GetString(67);
						old_wire.TransactionControlNum = reader.GetString(68);
						old_wire.WireProcessedBy = reader.GetString(69);
						old_wire.OFACVerificationBy = reader.GetString(70);
						old_wire.OFACDateTime = reader.GetString(71);
						old_wire.InternalSpecialInstructions = reader.GetString(72);
						old_wire.SecurityMethodUsed = reader.GetString(73);
						old_wire.SecurityProcessedBy = reader.GetString(74);
						old_wire.SecurityDateTime = reader.GetString(75);
						old_wire.EmployeeCallback = reader.GetString(76);
						old_wire.PhoneNumForCallback = reader.GetString(77);
						old_wire.SourcePhoneVerification = reader.GetString(78);
						old_wire.MemberCancellingRequest = reader.GetString(79);
						old_wire.CancelDate = reader.GetString(80);
						old_wire.CancelProcessedBy = reader.GetString(81);
						old_wire.ComputerName = reader.GetString(82);
						old_wire.IPAddress = reader.GetString(83);
						old_wire.EmployeeUser = reader.GetString(84);
	  				}
  				}
			} catch (Exception e){
				MessageBox.Show(e.ToString());
				return old_wire;
			}
  			return old_wire;
  		}	
	}
}

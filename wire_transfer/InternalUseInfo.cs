/*
 * Created by SharpDevelop.
 * User: IrynaM
 * Date: 06/12/2020
 * Time: 10:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.SqlClient;
//using System.Data.SQLite;
using System.Windows;




namespace wire_transfer
{
	/// <summary>
	/// Description of InternalUseInfo.
	/// </summary>
	public class InternalUseInfo
	{
		public string MemberConfirmingFunds {get; set;}
		public string DateAndTime {get; set;}
		public string FeeAmount {get; set;}
		public string IDUsed {get; set;}
		public string MethodOfTransfer {get; set;}
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
		
		public DatabaseHandler db = new DatabaseHandler();
		public SqlCommand cmd;

		public int insertInfo(){
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				
				cmd.CommandText = "INSERT INTO internal_use_info (member_confirming_funds, date_and_time, fee_amount, id_used, method_of_transfer, transaction_control_num, " +
					"wire_processed_by, OFAC_verification_by, OFAC_date_time, internal_special_instructions, security_method_used, security_processed_by, security_date_time, employee_callback, phone_num_for_callback," +
					"source_phone_verification, member_cancelling_request, cancel_date, cancel_processed_by) " +
					"VALUES " +
					"(@member_confirming_funds, @date_and_time, @fee_amount, @id_used, @method_of_transfer, @transaction_control_num, @wire_processed_by, @OFAC_verification_by, @OFAC_date_time, " +
					"@internal_special_instructions, @security_method_used, @security_processed_by, @security_date_time, @employee_callback, @phone_num_for_callback, @source_phone_verification, " +
					"@member_cancelling_request, @cancel_date, @cancel_processed_by); " +
					"SELECT SCOPE_IDENTITY();";

				// NOTE: The "parameterized query expects x value which was not supplied" is because this.TransactionControlNum 
				cmd.Parameters.Add(new SqlParameter("@member_confirming_funds", this.MemberConfirmingFunds));
				cmd.Parameters.Add(new SqlParameter("@date_and_time", this.DateAndTime));
				cmd.Parameters.Add(new SqlParameter("@fee_amount", this.FeeAmount));
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
				//cmd.ExecuteNonQuery();
				Int32 id = Convert.ToInt32(cmd.ExecuteScalar());
				return id;
			} catch (Exception e){
				MessageBox.Show(e.ToString());
				return -1;
			}
		}
		
		public void updateTransactionControlNum(string internal_use_id){
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				
				cmd.CommandText = "UPDATE internal_use_info SET transaction_control_num=@trans_control_num WHERE id="+internal_use_id+";";
				cmd.Parameters.Add(new SqlParameter("@trans_control_num", this.TransactionControlNum));
				cmd.ExecuteNonQuery();
			} catch (Exception e){
				MessageBox.Show(e.ToString());
			}
		}
		
		public void updateInternalUseInfo(int internal_use_id){
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				
				cmd.CommandText = "UPDATE internal_use_info SET member_confirming_funds=@member_confirming_funds, date_and_time=@date_and_time, fee_amount=@fee_amount, " +
					"id_used=@id_used, method_of_transfer=@method_of_transfer, transaction_control_num=@transaction_control_num, wire_processed_by=@wire_processed_by, " +
					"OFAC_verification_by=@OFAC_verification_by, OFAC_date_time=@OFAC_date_time, internal_special_instructions=@internal_special_instructions, " +
					"security_method_used=@security_method_used, security_processed_by=@security_processed_by, security_date_time=@security_date_time, " +
					"employee_callback=@employee_callback, phone_num_for_callback=@phone_num_for_callback, source_phone_verification=@source_phone_verification, " +
					"member_cancelling_request=@member_cancelling_request, cancel_date=@cancel_date, cancel_processed_by=@cancel_processed_by WHERE id="+internal_use_id+";";
				cmd.Parameters.Add(new SqlParameter("@member_confirming_funds", this.MemberConfirmingFunds));
				cmd.Parameters.Add(new SqlParameter("@date_and_time", this.DateAndTime));
				cmd.Parameters.Add(new SqlParameter("@fee_amount", this.FeeAmount));
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
				cmd.ExecuteNonQuery();
			} catch (Exception e){
				MessageBox.Show(e.ToString());
			}
		}
	
	}
}

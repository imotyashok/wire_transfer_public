/*
 * Created by SharpDevelop.
 * User: IrynaM
 * Date: 7/23/2020
 * Time: 9:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace wire_transfer
{
	public class FormValidation : IDataErrorInfo
	{
		
		public string Error{
			get {return null;}
		}
		
		public string sName {get; set;}
		public string sMemberNum {get; set;}
		public string sSubAccount {get; set;}
		public string sAddress {get; set;}
		public string sCity {get; set;}
		public string sState {get; set;}
		public string sZip {get; set;}
		public string sPhone {get; set;}
		public string sPaymentInstr {get; set;}
		
		public string rName {get; set;}
  		public string rAddress {get; set;}
  		public string rCity {get; set;}
  		public string rZip  {get; set;}
  		public string rPhone {get; set;}
  		public string rCountry  {get; set;}
  		public string rAccountNumIBAN  {get; set;}
  		public string rSSN {get; set;}
  		public string rTIN  {get; set;}
  		public string rDLnum {get; set;}
  		
  		public string ibName  {get; set;}
  		public string ibAddress  {get; set;}
  		public string ibCity {get; set;}
  		public string ibState {get; set;}
 	 	public string ibZip {get; set;}
  		public string ibRoutingNum  {get; set;}
  		public string ibSwiftSortCode {get; set;}
  		public string ibBranchInfo {get; set;}
  		public string ibRoutingInstr {get; set;}
  		
  		public string rbName  {get; set;}
  		public string rbAddress  {get; set;}
  		public string rbCity {get; set;}
  		public string rbState {get; set;}
 	 	public string rbZip {get; set;}
  		public string rbRoutingNum  {get; set;}
  		public string rbSwiftSortCode {get; set;}
  		public string rbBranchInfo {get; set;}
  		public string rbRoutingInstr {get; set;}
  		
  		
  		public string transferDate {get; set;}
  		public string fundsAvailableBy {get; set;}
  		
  		public string employeeName {get; set;}
		public string ofacVerificationBy {get; set;}
		public string ofacDateTime {get; set;}
		public string internalSpecialInstr {get; set;}
		public string securityMethodUsed {get; set;}
		public string securityProcessedBy {get; set;}
		public string securityDateTime {get; set;}
		public string employeeCallback {get; set;}
		public string phoneNumCallback {get; set;}
		public string sourcePhoneVerificationCallback {get; set;}
		public string memberCancellingRequest {get; set;}
		public string memberConfirmingFunds {get; set;}
		public string cancelDate {get; set;}
		public string cancelProcessedBy {get; set;}
		
		public string pickupName {get; set;}
		public string pickupAddress {get; set;}
		public string pickupCityStateZip {get; set;}
		public string pickupPhone {get; set;}
		public string futureDate1 {get; set;}
		public string futureDate2 {get; set;}
		
		
		public string transAmount {get; set;}
		public string USDTotal {get; set;}
		public string transFees {get; set;}
		public string exchangeRate {get; set;}
		public string foreignTransAmount {get; set;}
		public string foreignTotal {get; set;}
		
		
		
		public string Required_Field_Validation(string field_name){
			if (String.IsNullOrEmpty(field_name)){
				return "This is a required field.";
			}
			return null;
		}
		
		public string Number_Field_Validation(string field_name){
			string pattern = @"^(?:[0-9\-\s]+)?$";
			Regex regex = new Regex(pattern);
			Debug.WriteLine(regex.IsMatch(field_name).ToString());
		
			bool passed = regex.IsMatch(field_name);
			if (passed == false){
				return "This field cannot contain special characters.";
			}
			
			return null;
		}
		
		public string Name_Field_Validation(string field_name){
			string pattern = @"^(?:[a-zA-Z0-9\-\'\.\/\,\s]+)?$";
			Regex regex = new Regex(pattern);
			Debug.WriteLine(regex.IsMatch(field_name).ToString());
		
			bool passed = regex.IsMatch(field_name);
			if (passed == false){
				return "This field cannot contain special characters.";
			}
			
			return null;
		}
				
		public string No_Special_Characters_Validation(string field_name){
			string pattern = @"^(?:[a-zA-Z0-9\-\.\,\/\'\#\:\s]+)?$";
			Regex regex = new Regex(pattern);
			Debug.WriteLine(regex.IsMatch(field_name).ToString());
			
			bool passed = regex.IsMatch(field_name);
			if (passed == false){
				return "This field cannot contain special characters.";
			}
			return null;
		}
		
		public string Minimal_Validation(string field_name){
			// We won't allow " or ; characters for injection prevention purposes
			
			string pattern = @"^(?:[a-zA-Z0-9\-\.\,\'\:\?\!\%\#\~\/\{\}\[\]\(\)\@\^\=\+\&\$\s]+)?$";
			Regex regex = new Regex(pattern);
			Debug.WriteLine(regex.IsMatch(field_name).ToString());
			
			bool passed = regex.IsMatch(field_name);
			if (passed == false){
				return "This field cannot contain that character.";
			}
			return null;
		}
		
		public string Date_Field_Validation(string field_name){
			string pattern = @"^(?:[0-9\/]+)?$";
			Regex regex = new Regex(pattern);
			Debug.WriteLine(regex.IsMatch(field_name).ToString());
			
			bool passed = regex.IsMatch(field_name);
			int length = field_name.Length;
			
			if (passed == false){
				return "The character you entered is invalid.";
			}
			else{
				if (length == 0){
					return null;
				}
				else if (length != 10){
					return "The date must be in mm/dd/yyyy format.";
				}
			}
			return null;
		}
		
		public string Max_Characters_Validation(string field_name, int max_chars){
			if (field_name.Length > max_chars){
				return "You entered too many characters.";
			}
			else{
				return null;
			}
		}
		
		public string Currency_Field_Validation(string field_name){
			string pattern = @"^(?:[0-9\.]+)?$";
			Regex regex = new Regex(pattern);
			Debug.WriteLine(regex.IsMatch(field_name).ToString());
			
			bool passed = regex.IsMatch(field_name);
			if (passed == false){
				return "This field cannot contain that character.";
			}
			return null;
		}
		
		public string this[string columnName]{
			get{
				string result = null;
				
				#region Sender Page Fields
				if (columnName == "sMemberNum"){
					result = Required_Field_Validation(sMemberNum) ?? No_Special_Characters_Validation(sMemberNum) ?? Max_Characters_Validation(sMemberNum, 10);
				}
				if (columnName == "sSubAccount"){
					result = Required_Field_Validation(sSubAccount) ?? No_Special_Characters_Validation(sSubAccount) ?? Max_Characters_Validation(sSubAccount, 15);
				}
				if (columnName == "sName"){
					result = Required_Field_Validation(sName) ?? Name_Field_Validation(sName) ?? Max_Characters_Validation(sName, 35);
				}
				if (columnName == "sAddress"){
					result = Required_Field_Validation(sAddress) ?? No_Special_Characters_Validation(sAddress) ?? Max_Characters_Validation(sAddress, 35);
				}
				if (columnName == "sCity"){
					result = Required_Field_Validation(sCity) ?? Name_Field_Validation(sCity) ?? Max_Characters_Validation(sCity, 35);
				}
				if (columnName == "sZip"){
					result = Required_Field_Validation(sZip) ?? Number_Field_Validation(sZip) ?? Max_Characters_Validation(sZip, 35);
				}
				if (columnName == "sState"){
					result = Required_Field_Validation(sState) ?? Max_Characters_Validation(sState, 35);
				}
				if (columnName == "sPhone"){
					result = Number_Field_Validation(sPhone) ?? Max_Characters_Validation(sPhone, 35);
				}
				if (columnName == "sPaymentInstr"){
					result = Minimal_Validation(sPaymentInstr);
				}
				#endregion
				
				#region Recipient Page Fields
				if (columnName == "rName"){
					result = Required_Field_Validation(rName) ?? Name_Field_Validation(rName) ?? Max_Characters_Validation(rName, 35);
				}
				if (columnName == "rAddress"){
					result = Required_Field_Validation(rAddress) ?? No_Special_Characters_Validation(rAddress) ?? Max_Characters_Validation(rAddress, 35);
				}
				if (columnName == "rCity"){
					result = Name_Field_Validation(rCity) ?? Max_Characters_Validation(rCity, 35);
				}
				if (columnName == "rZip"){
					result = Number_Field_Validation(rZip) ?? Max_Characters_Validation(rZip, 35);
				}
				if (columnName == "rCountry"){
					result = Required_Field_Validation(rCountry) ?? Max_Characters_Validation(rCountry, 35);
				}
				if (columnName == "rPhone"){
					result = Number_Field_Validation(rPhone) ?? Max_Characters_Validation(rPhone, 35);
				}
				if (columnName == "rAccountNumIBAN"){
					result = Required_Field_Validation(rAccountNumIBAN) ?? No_Special_Characters_Validation(rAccountNumIBAN) ?? Max_Characters_Validation(rAccountNumIBAN, 35);
				}
				if (columnName == "rSSN"){
					result = No_Special_Characters_Validation(rSSN) ?? Max_Characters_Validation(rSSN, 35);
				}
				if (columnName == "rTIN"){
					result = No_Special_Characters_Validation(rTIN) ?? Max_Characters_Validation(rTIN, 35);
				}
				if (columnName == "rDLnum"){
					result = No_Special_Characters_Validation(rDLnum) ?? Max_Characters_Validation(rDLnum, 35);
				}
				#endregion

				#region Intermediary Bank Page Fields
				if (columnName == "ibName"){
					result = Name_Field_Validation(ibName) ?? Max_Characters_Validation(rName, 35);
				}
				if (columnName == "ibAddress"){
					result = No_Special_Characters_Validation(ibAddress) ?? Max_Characters_Validation(rName, 35);
				}
				if (columnName == "ibCity"){
					result = Name_Field_Validation(ibCity) ?? Max_Characters_Validation(rName, 35);
				}
				if (columnName == "ibZip"){
					result = Number_Field_Validation(ibZip) ?? Max_Characters_Validation(rName, 35);
				}
				if (columnName == "ibRoutingNum"){
					result = No_Special_Characters_Validation(ibRoutingNum) ?? Max_Characters_Validation(rName, 35);
				}
				if (columnName == "ibSwiftSortCode"){
					result = No_Special_Characters_Validation(ibSwiftSortCode) ?? Max_Characters_Validation(rName, 35);
				}
				if (columnName == "ibBranchInfo"){
					result = Minimal_Validation(ibBranchInfo) ?? Max_Characters_Validation(rName, 35);
				}
				if (columnName == "ibRoutingInstr"){
					result = Minimal_Validation(ibRoutingInstr);
				}	
				#endregion
				
				#region Recipient Bank Page Fields
				if (columnName == "rbName"){
					result = Required_Field_Validation(rbName) ?? Name_Field_Validation(rbName) ?? Max_Characters_Validation(rbName, 35);
				}
				if (columnName == "rbAddress"){
					result = Required_Field_Validation(rbAddress) ?? No_Special_Characters_Validation(rbAddress) ?? Max_Characters_Validation(rbAddress, 35);
				}
				if (columnName == "rbCity"){
					result = Name_Field_Validation(rbCity) ?? Max_Characters_Validation(rbCity, 35);
				}
				if (columnName == "rbZip"){
					result = Number_Field_Validation(rbZip) ?? Max_Characters_Validation(rbZip, 35);
				}
				if (columnName == "rbRoutingNum"){
					result = No_Special_Characters_Validation(rbRoutingNum) ?? Max_Characters_Validation(rbRoutingNum, 35);
				}
				if (columnName == "rbSwiftSortCode"){
					result = No_Special_Characters_Validation(rbSwiftSortCode) ?? Max_Characters_Validation(rbSwiftSortCode, 35);
				}
				if (columnName == "rbBranchInfo"){
					result = Minimal_Validation(rbBranchInfo) ?? Max_Characters_Validation(rbBranchInfo, 35);
				}
				if (columnName == "rbRoutingInstr"){
					result = Minimal_Validation(rbRoutingInstr);
				}	
				#endregion
				
				#region Internal Use Page Fields
				if (columnName == "memberConfirmingFunds"){
					result = Name_Field_Validation(memberConfirmingFunds) ?? Max_Characters_Validation(memberConfirmingFunds, 35);
				}
				if (columnName == "internalSpecialInstr"){
					result = Minimal_Validation(internalSpecialInstr);
				}
				if (columnName == "securityMethodUsed"){
					result = No_Special_Characters_Validation(securityMethodUsed) ?? Max_Characters_Validation(securityMethodUsed, 35);
				}
				if (columnName == "securityProcessedBy"){
					result = Name_Field_Validation(securityProcessedBy) ?? Max_Characters_Validation(securityProcessedBy, 35);
				}
				if (columnName == "securityDateTime"){
					result = No_Special_Characters_Validation(securityDateTime) ?? Max_Characters_Validation(securityDateTime, 35);
				}
				if (columnName == "employeeName"){
					result = Name_Field_Validation(employeeName) ?? Max_Characters_Validation(employeeName, 35);
				}
				if (columnName == "ofacVerificationBy"){
					result = Name_Field_Validation(ofacVerificationBy) ?? Max_Characters_Validation(ofacVerificationBy, 35);
				}
				if (columnName == "ofacDateTime"){
					result = Date_Field_Validation(ofacDateTime);
				}
				if (columnName == "employeeCallback"){
					result = Name_Field_Validation(employeeCallback) ?? Max_Characters_Validation(employeeCallback, 35);
				}
				if (columnName == "phoneNumCallback"){
					result = Number_Field_Validation(phoneNumCallback) ?? Max_Characters_Validation(phoneNumCallback, 35);
				}
				if (columnName == "sourcePhoneVerificationCallback"){
					result = No_Special_Characters_Validation(sourcePhoneVerificationCallback) ?? Max_Characters_Validation(sourcePhoneVerificationCallback, 35);
				}
				if (columnName == "memberCancellingRequest"){
					result = Name_Field_Validation(memberCancellingRequest) ?? Max_Characters_Validation(memberCancellingRequest, 35);
				}
				if (columnName == "cancelDate"){
					result = Date_Field_Validation(cancelDate);
				}
				if (columnName == "cancelProcessedBy"){
					result = Name_Field_Validation(cancelProcessedBy) ?? Max_Characters_Validation(cancelProcessedBy, 35);
				}
				
			
				#endregion
				
				#region Additional Info
				
				if (columnName == "pickupName"){
					result = Name_Field_Validation(pickupName) ?? Max_Characters_Validation(pickupName, 35);
				}
				if (columnName == "pickupAddress"){
					result = No_Special_Characters_Validation(pickupAddress) ?? Max_Characters_Validation(pickupAddress, 35);
				}
				if (columnName == "pickupCityStateZip"){
					result = No_Special_Characters_Validation(pickupCityStateZip) ?? Max_Characters_Validation(pickupCityStateZip, 35);
				}
				if (columnName == "pickupPhone"){
					result = Number_Field_Validation(pickupPhone) ?? Max_Characters_Validation(pickupPhone, 35);
				}
				if (columnName == "futureDate1"){
					result = Date_Field_Validation(futureDate1) ?? Max_Characters_Validation(futureDate1, 35);
				}
				if (columnName == "futureDate2"){
					result = Date_Field_Validation(futureDate2) ?? Max_Characters_Validation(futureDate2, 35);
				}	
				
				if (columnName == "transferDate"){
					result = Required_Field_Validation(transferDate) ?? Date_Field_Validation(transferDate);
				}
				
				if (columnName == "fundsAvailableBy"){
					result = Date_Field_Validation(fundsAvailableBy);
				}
				
				
				#endregion
				
				#region Currency Exchange 
				
				if (columnName == "transAmount"){
					result = Required_Field_Validation(transAmount) ?? Currency_Field_Validation(transAmount) ?? Max_Characters_Validation(transAmount, 35);
				}
				if (columnName == "USDTotal"){
					result = Required_Field_Validation(USDTotal) ?? Currency_Field_Validation(USDTotal) ?? Max_Characters_Validation(USDTotal, 35);
				}
				if (columnName == "transFees"){
					result = Required_Field_Validation(transFees) ?? Currency_Field_Validation(transFees) ?? Max_Characters_Validation(transFees, 35);
				}
				if (columnName == "exchangeRate"){
					result = Required_Field_Validation(exchangeRate) ?? Currency_Field_Validation(exchangeRate) ?? Max_Characters_Validation(exchangeRate, 35);
				}
				if (columnName == "foreignTransAmount"){
					result = Currency_Field_Validation(foreignTransAmount) ?? Max_Characters_Validation(foreignTransAmount, 35);
				}
				if (columnName == "foreignTotal"){
					result = Required_Field_Validation(foreignTotal) ?? Currency_Field_Validation(foreignTotal) ?? Max_Characters_Validation(foreignTotal, 35);
				}
				
				#endregion
				
				return result;
			}
		}
	}


}

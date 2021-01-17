/*
 * Created by SharpDevelop.
 * User: IrynaM
 * Date: 9/16/2020
 * Time: 11:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using iTextSharp;
using iTextSharp.text;  
using iTextSharp.text.pdf;
using System.Windows;


namespace wire_transfer
{
	/// <summary>
	/// Description of WirePDFWriter.
	/// </summary>
	public class WirePDFWriter
	{
		
		public PdfReader pdfReader;
		public PdfStamper pdfStamper;
		
		
		private string CreateTmpFile()
		{
		    string fileName = string.Empty;
		
		    try
		    {
		        fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
		    }
		    catch (Exception ex)
		    {
		       MessageBox.Show("Unable to create TEMP file: " + ex.Message);
		    }
		
		    return fileName;
		}
		
		private static void DeleteTmpFile(string tmpFile)
		{
		    try
		    { // Delete the temp file (if it exists)
		    	
		        if (File.Exists(tmpFile))
		        {
		            File.Delete(tmpFile);
		        }
		    }
		    catch (Exception ex)
		    {
		        MessageBox.Show("Error deleteing TEMP file: " + ex.Message);
		    }
		}
		
		
		public void fill_nonremittance_PDF(WireHistory wire){
			string pdfTemplate = Globals.PDF_PATH_NONREMITTANCE;  
			// string new_file = CreateTmpFile();  // Globals.PDF_PATH_NONREMITTANCE_OUTPUT;
			string new_file = CreateTmpFile();
		 		
		    try{
			    pdfReader = new PdfReader(pdfTemplate);	
				pdfStamper = new PdfStamper(pdfReader, new FileStream(new_file, FileMode.Create));  
			    
			    AcroFields af = pdfStamper.AcroFields;

			    // af.SetField("mydate", wire.TransferDate);
			    //af.SetField("acctnumber", wire.sMemberNum+"-"+wire.TransferDate+"-"+wire.TransferTime);
			    af.SetField("AP1_ACCT_NMBR", wire.sMemberNum+" - "+wire.DateTime);
			    
			    if (wire.OneTime == 1){
			    	af.SetField("ONE_TIME_X", "X");
			    } 
			    else{
			    	af.SetField("ONE_TIME_X", "");
			    }
			    
			    if (wire.SubjectToTransAgreement == 1){
			    	af.SetField("SUB_FUNDSWIRE_X", "X");
			    }
			    else{
			    	af.SetField("SUB_FUNDSWIRE_X", "");
			    }
			    
			    af.SetField("PAYER_NAME", wire.sName);  af.SetField("PAYER_ADD1", wire.sAddress);  
			    if (wire.sState == "N/A" || String.IsNullOrEmpty(wire.sState)){
			    	af.SetField("PAYER_ADD2", wire.sCity+" "+wire.sZip);
			    }
			    else{
			    	af.SetField("PAYER_ADD2", wire.sCity+", "+wire.sState+" "+wire.sZip);
			    }
			    
			    af.SetField("PAYER_DAY_PH_NMBR", wire.sPhoneNum);  af.SetField("ACCT_TRANS_AMT", wire.TransAmountUSD);  af.SetField("PAYER_INSTRUCT1", "");  af.SetField("PAYER_INSTRUCT2", wire.PaymentInstr);
			    
			    af.SetField("PAYEE_NAME", wire.rName);  af.SetField("PAYEE_ADD1", wire.rAddress);  
			    af.SetField("PAYEE_ADD2", wire.rCity+" "+wire.rState+" "+wire.rZip);
			    if (wire.rState == "N/A" || String.IsNullOrEmpty(wire.rState)){
			    	af.SetField("PAYEE_ADD2", wire.rCity+" "+wire.rZip);
			    }
			    else{
			    	af.SetField("PAYEE_ADD2", wire.rCity+", "+wire.rState+" "+wire.rZip);
			    }
			    
			    af.SetField("PAYEE_COUNTRY", wire.rCountry);  af.SetField("PAYEE_ACCT_NMBR", wire.rAccountNumIBAN);  af.SetField("PAYEE_SSN", wire.rSSN);  af.SetField("PAYEE_TIN",wire.rTIN);  af.SetField("PAYEE_DL_NMBR",wire.rDLNum);
			    
			    af.SetField("FIN_INST_NAME", wire.rbName);  af.SetField("FIN_INST_ADD1",wire.rbAddress);  
			    if (wire.rbState == "N/A" || String.IsNullOrEmpty(wire.rbState)){
			    	af.SetField("FIN_INST_ADD2", wire.rbCity+" "+wire.rbZip);
			    }
			    else{
			    	af.SetField("FIN_INST_ADD2", wire.rbCity+" "+wire.rbState+" "+wire.rbZip);
			    }
			    
			    af.SetField("TRANSIT_NMBR", wire.rbRoutingNum);  af.SetField("SWIFT_CODE", wire.rbSwiftSortCode);  af.SetField("BRANCH_INFO", wire.rbBranch);  af.SetField("ROUTE_INSTRUCT", "");  af.SetField("ROUTE_INSTRUCT1", wire.rbRoutingInstr);
			    
			    af.SetField("FIN_INST_NAME2", wire.ibName);  af.SetField("FIN_INST2_ADD1", wire.ibAddress);  
			    af.SetField("FIN_INST2_ADD2", wire.ibCity+" "+wire.ibState+" "+wire.ibZip);
			    if (wire.ibState == "N/A" || String.IsNullOrEmpty(wire.ibState)){
			    	af.SetField("FIN_INST2_ADD2", wire.ibCity+" "+wire.ibZip);
			    }
			    else{
			    	af.SetField("FIN_INST2_ADD2", wire.ibCity+" "+wire.ibState+" "+wire.ibZip);
			    }
			    
			    af.SetField("TRANSIT_NMBR1", wire.ibRoutingNum);  af.SetField("SWIFT_CODE1", wire.ibSwiftSortCode);  af.SetField("BRANCH_INFO1", wire.ibBranch);  af.SetField("ROUTE_INSTRUCT3", wire.ibRoutingInstr);
			    
			    af.SetField("MBR_CONFIRM_TRANS", wire.MemberConfirmingFunds);   af.SetField("IDENT_USE", wire.IDUsed);   af.SetField("SPECIAL_INSTRUCT", "");  af.SetField("SPECIAL_INSTRUCTION", wire.InternalSpecialInstructions);
			    af.SetField("PROCESSED_BY", wire.WireProcessedBy);  af.SetField("OFAC_VERIFY", wire.OFACVerificationBy);
			    af.SetField("SEC_METHOD", wire.SecurityMethodUsed);  af.SetField("DT_TIME", wire.SecurityDateTime);  af.SetField("PROCESSED2_BY", wire.SecurityProcessedBy);  
			    af.SetField("EMP_CALLBACK", wire.EmployeeCallback);  af.SetField("CALLBACK_PH_NMBR", wire.PhoneNumForCallback);  af.SetField("SOURCE_SECURE_PH_NUMBER", wire.SourcePhoneVerification);
			    af.SetField("MBR_CANCEL_REQUEST", wire.MemberCancellingRequest);  af.SetField("CANCEL_DT", wire.CancelDate);  af.SetField("PROCESSED3_BY", wire.CancelProcessedBy);
			    
			    af.SetField("METHOD_TRANSFER", wire.MethodOfTransfer);
			    
			    af.SetField("CONTROL_NMBR", wire.WireID.ToString());
			    af.SetField("total_box", wire.TotalUSD);
			    af.SetField("AMOUNT", wire.TotalUSD);
			    af.SetField("FEE_AMT", wire.TransFeesUSD);
			    af.SetField("PAYER_ACCT_NMBR", wire.sMemberNum+"-"+wire.sSubAccount);
			    af.SetField("DT_TIME_REQ", wire.DateTime);
			    af.SetField("CURRENCY_TYPE", wire.CurrencyInfo);
			    
			    af.SetField("AP1_ACCT_NAME", wire.sName);
			    af.SetField("AP1_PRES_ADD1", wire.sAddress);
			    af.SetField("AP1_PRES_ADD2", wire.sCity+", "+wire.sState+" "+wire.sZip);
			  
			    af.SetField("DATE", wire.TransferDate);
			    af.SetField("AMT_TRANSFER1", wire.TransAmountUSD);
			    af.SetField("XFER_FEE", wire.TransFeesUSD);
			    af.SetField("acctnumber", wire.sMemberNum);
			    af.SetField("mydate", wire.DateTime);
			    
			    pdfStamper.FormFlattening = false;
			    pdfStamper.Close();
			    
			    pdfReader.Close();
			    
				System.Diagnostics.Process.Start(new_file); // Immediately opens the file for the user to print it
			
			    
		    } catch (Exception err){
		    	MessageBox.Show(err.ToString());
		    }
		}
		
		public void fill_remittance_PDF(WireHistory wire){
			string pdfTemplate = Globals.PDF_PATH_REMITTANCE;  
			string new_file = CreateTmpFile(); // Globals.PDF_PATH_REMITTANCE_OUTPUT;
					 	
			
		    try{
			    pdfReader = new PdfReader(pdfTemplate);	
				pdfStamper = new PdfStamper(pdfReader, new FileStream(new_file, FileMode.Create), '\0', true);  
			    
			    AcroFields af = pdfStamper.AcroFields;
			    
			    af.SetField("AP1_ACCT_NMBR", wire.sMemberNum+"-"+wire.sSubAccount);
			    af.SetField("DATE_TRANSFER", wire.TransferDate);   af.SetField("DATE", wire.TransferDate);
			   // af.SetField("time", wire.TransferTime);
			   // af.SetField("time", "");
			    
			    af.SetField("AMT_TRANSFER1", wire.TransAmountUSD); af.SetField("XFER_FEE", wire.TransFeesUSD);  af.SetField("TOTAL_AMT", wire.TotalUSD);  af.SetField("XCHANGE", wire.ExchangeRate); af.SetField("LISTBOX", wire.CurrencyType);
			    af.SetField("ESTIMATE5", wire.TotalForeign);  af.SetField("ESTIMATE4", wire.TotalForeign);  af.SetField("ESTIMATE8", wire.TotalForeign); af.SetField("TOTAL_AMT1", wire.TransAmountUSD); 
			    
			    af.SetField("PAYER_NAME", wire.sName);  af.SetField("PAYER_ADD1", wire.sAddress);  
			    if (wire.sState == "N/A" || String.IsNullOrEmpty(wire.sState)){
			    	af.SetField("PAYER_ADD2", wire.sCity+" "+wire.sZip);
			    }
			    else{
			    	af.SetField("PAYER_ADD2", wire.sCity+", "+wire.sState+" "+wire.sZip);
			    }
			    
			    af.SetField("PAYER_DAY_PH_NMBR", wire.sPhoneNum);  af.SetField("AMT_TRANSFER2", wire.TransAmountUSD);  af.SetField("PAYER_INSTRUCT", "");  af.SetField("PAYER_INSTRUCT1", wire.PaymentInstr);
			    
			    af.SetField("PAYEE_NAME", wire.rName);  af.SetField("PAYEE_ADD1", wire.rAddress);  af.SetField("PAYEE_DAY_PH_NMBR", wire.rPhone);
			    if (wire.rState == "N/A" || String.IsNullOrEmpty(wire.rState)){
			    	af.SetField("PAYEE_ADD2", wire.rCity+" "+wire.rZip+" "+wire.rCountry);
			    }
			    else{
			    	af.SetField("PAYEE_ADD2", wire.rCity+", "+wire.rState+" "+wire.rZip+" "+wire.rCountry);
			    }
			    
			    af.SetField("PAYEE_COUNTRY", wire.rCountry);  af.SetField("PAYEE_ACCT_NMBR", wire.rAccountNumIBAN);  af.SetField("PAYEE_SSN", wire.rSSN);  af.SetField("PAYEE_TIN",wire.rTIN);  af.SetField("PAYEE_DL_NMBR",wire.rDLNum);
			    
			    af.SetField("FUNDS_AVAILABLE_BY1", wire.FundsAvailableBy);  af.SetField("FUTURE_DATES_X", ""); af.SetField("FUTURE_DATES_DESC", "");  af.SetField("FUTURE_DATES_DESC1", "");
			    af.SetField("LOCATION_NAME", wire.PickupName);  af.SetField("PROPERTY_ADD1", wire.PickupAddress); af.SetField("PROPERTY_ADD2", wire.PickupCityStateZip); af.SetField("PHONE_NUM", wire.PickupPhone);
			    
//			    af.SetField("UPDATE1_X", wire.TransAmountUSD);  af.SetField("UPDATE2_X", wire.TransFeesUSD);  af.SetField("UPDATE4_X", wire.TotalUSD);  af.SetField("UPDATE5_X", wire.ExchangeRate);   af.SetField("UPDATE6_X", wire.TransAmountUSD); af.SetField("UPDATE8_X", wire.TotalUSD);
			    af.SetField("CURRENCY_TYPE_ABBR", wire.CurrencyType);  af.SetField("ESTIMATE7", wire.TotalForeign);
			    af.SetField("CHECKBOX1", "X");
			    
			    af.SetField("PAYER_ACCT_NMBR", wire.sMemberNum+"-"+wire.sSubAccount);  af.SetField("PAYER_INSTRUCT", "");  af.SetField("PAYER_INSTRUCT1", wire.PaymentInstr);
			    
			   
			    af.SetField("IBAN_NMBR", wire.rAccountNumIBAN);
			    if (!String.IsNullOrEmpty(wire.rSSN)){
			    	af.SetField("PAYEE_ID1", "SSN: "+wire.rSSN);
			    }
			    else if (!String.IsNullOrEmpty(wire.rTIN)){
			    	af.SetField("PAYEE_ID1", "TIN: "+wire.rTIN);
			    }
			    else if (!String.IsNullOrEmpty(wire.rDLNum)){
			    	af.SetField("PAYEE_ID1", "DL#: "+wire.rDLNum);
			    }
			    else{
			    	af.SetField("PAYEE_ID1", "");
			    }
			    
			    
			    af.SetField("FIN_INST_NAME", wire.ibName);  af.SetField("FIN_INST_ADD1",wire.ibAddress);  
			    if (wire.ibState == "N/A" || String.IsNullOrEmpty(wire.ibState)){
			    	af.SetField("FIN_INST_ADD2", wire.ibCity+" "+wire.ibZip);
			    }
			    else{
			    	af.SetField("FIN_INST_ADD2", wire.ibCity+" "+wire.ibState+" "+wire.ibZip);
			    }
			    
			    af.SetField("TRANSIT_NMBR", wire.ibRoutingNum);  af.SetField("SWIFT CODE", wire.ibSwiftSortCode);  af.SetField("BRANCH_INFO", wire.ibBranch);  af.SetField("SPECIAL_INSTRUCT", "");  af.SetField("SPECIAL_INSTRUCT1", wire.ibRoutingInstr);
			    
			    af.SetField("FIN_INST_NAME2", wire.rbName);  af.SetField("FIN_INST2_ADD1", wire.rbAddress);  
			    if (wire.rbState == "N/A" || String.IsNullOrEmpty(wire.rbState)){
			    	af.SetField("FIN_INST2_ADD2", wire.rbCity+" "+wire.rbZip);
			    }
			    else{
			    	af.SetField("FIN_INST2_ADD2", wire.rbCity+" "+wire.rbState+" "+wire.rbZip);
			    }
			    
			    af.SetField("SWIFT CODE1", wire.rbSwiftSortCode);  af.SetField("BRANCH_INFO1", wire.rbBranch); af.SetField("SPECIAL_INSTRUCT2", "");  af.SetField("SPECIAL_INSTRUCT3", wire.rbRoutingInstr);
			    
			    af.SetField("DT_TIME_REQ", wire.DateTime); // af.SetField("timerequest", wire.TransferDate+" "+wire.TransferTime); 
			    af.SetField("PROCESSED_BY1", wire.WireProcessedBy);  af.SetField("METHOD_TRANSFER", wire.MethodOfTransfer); af.SetField("VERIF_CODE", wire.sMemberNum+"-"+wire.sSubAccount+" "+wire.WireID);
			    af.SetField("SEC_METHOD", wire.SecurityMethodUsed);  af.SetField("CU_CHANGE_COMP_BY", wire.SecurityProcessedBy);  af.SetField("DT_TIME", wire.SecurityDateTime);  af.SetField("CALLBACK_PH_NMBR", wire.PhoneNumForCallback);
			    af.SetField("CANCEL_DT", wire.CancelDate);  af.SetField("TIME_REC", "");  af.SetField("CU_PROCESSED_BY", wire.CancelProcessedBy);   af.SetField("CONTROL_NMBR", wire.WireID.ToString());
			    
			    if (!String.IsNullOrEmpty(wire.OFACVerificationBy)){
			    	af.SetField("CHEX_OFAC_X", "X");
			    	af.SetField("OFAC_VERIFY", wire.OFACVerificationBy);
			    	af.SetField("CU_OFAC_DATE", wire.OFACDateTime);
			    }
			    else{
			    	af.SetField("CHEX_OFAC_X", "");
			    	af.SetField("OFAC_VERIFY", "");
			    	af.SetField("CU_OFAC_DATE", "");
			    }
			    
			    af.SetField("DT_TIME_REQ", wire.DateTime);
			    af.SetField("AMOUNT", wire.TotalUSD);
			    
			    
			    af.SetField("DATE", wire.TransferDate);
			    af.SetField("AMT_TRANSFER1", wire.TransAmountUSD);
			    af.SetField("XFER_FEE", wire.TransFeesUSD);
			    af.SetField("acctnumber", wire.sMemberNum);
			    af.SetField("mydate", wire.DateTime);
			    
			    
			    pdfStamper.FormFlattening = false;
			    pdfStamper.Close();
			    
			    pdfReader.Close();
				
				System.Diagnostics.Process.Start(new_file); // Immediately opens the file for the user to print it
			
			    
		    } catch (Exception err){
		    	MessageBox.Show(err.ToString());
		    }
		}
	}
}

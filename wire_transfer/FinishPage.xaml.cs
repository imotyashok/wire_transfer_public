/*
 * Created by SharpDevelop.
 * User: IrynaM
 * Date: 06/09/2020
 * Time: 10:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using iTextSharp;
using iTextSharp.text;  
using iTextSharp.text.pdf;


namespace wire_transfer
{
	/// <summary>
	/// Interaction logic for FinishPage.xaml
	/// </summary>
	public partial class FinishPage : Window
	{
		
		public class WireAddedMessage{
			public string Message {get; set;}
		}
		
		
		public FinishPage()
		{
			InitializeComponent();
			Debug.WriteLine("Global wire id: "+Globals.WIRE_ID.ToString());
			string wire_id = Globals.WIRE_ID.ToString();
			WireAddedMessage message = new WireAddedMessage();
			
			if (wire_id == "-1"){
				message.Message = "Sorry, this wire was not successful. Please try again.";
				this.printBtn.IsEnabled = false;
			} 
			else{
				message.Message = "You have successfully added wire transfer #"+wire_id+"!"+
					"\n Select what you'd like to do next.";
			}
			this.wireAddedMessage.DataContext = message;
		}
		
		
		
		private void Return_Home(object sender, EventArgs e){
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(Window1))
				{
					(window as Window1).newWireForm.Visibility = Visibility.Collapsed;
					(window as Window1).welcomePage.Visibility = Visibility.Visible;
					(window as Window1).homeBtn.Visibility = Visibility.Collapsed;
				}
			}
			
			this.Hide();
		}
		
		private void Go_Back_To_Edit(object sender, EventArgs e){
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(Window1))
				{
					(window as Window1).newWireForm.Visibility = Visibility.Visible;
					(window as Window1).welcomePage.Visibility = Visibility.Collapsed;
					(window as Window1).homeBtn.Visibility = Visibility.Visible;
					(window as Window1).updateBtn.Visibility = Visibility.Visible;
					(window as Window1).finishBtn.IsEnabled = false;
					(window as Window1).isUpdating = true;
				}
			}
			
			
			this.Hide();
			//this.Close();
		}
		
		private void fillFDFform(object sender, EventArgs e){
			try{
				
				WireHistory wire = new WireHistory();
				int wire_id = Globals.WIRE_ID;
				Debug.WriteLine(wire_id.ToString());
				wire = wire.returnOldWire(wire_id);  // Retrieves all the information pertaining to the wire that was just completed
				
				WirePDFWriter pdf = new WirePDFWriter();
				
				if (wire.WireType == "R"){
					pdf.fill_remittance_PDF(wire);
				}
				else if (wire.WireType == "NR"){
					pdf.fill_nonremittance_PDF(wire);
				}

			} catch (Exception error) {
				MessageBox.Show(error.ToString());
			}
		}
		
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
			this.Hide();  
        }
		
	}
}
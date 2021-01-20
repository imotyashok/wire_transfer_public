/*
 * Created by SharpDevelop.
 * User: iryna
 * Date: 5/18/2020
 * Time: 9:37 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
// using System.Data.SQLite;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;


namespace wire_transfer
{
	/// <summary>
	/// Description of DatabaseHandler.
	/// </summary>
	public class DatabaseHandler
	{

		private string connection_str = ConfigurationManager.ConnectionStrings["wire_transfer_db"].ConnectionString;
		
		private SqlCommand cmd;
		private SqlConnection connection; 
		public bool connectionSuccessful; 
		
		public void initializeConnection(){
			try{			
				connection = new SqlConnection(connection_str);
				connection.Open();
				cmd = connection.CreateCommand();
				connectionSuccessful = true;
			} catch (SqlException e) {
				connectionSuccessful = false;
				MessageBox.Show("Sorry, the program cannot be run since a connection to the database couldn't be established. " +
				                "\n\nPlease contact your IT department or try running the program at a later time." +"\n");
				
			}
		}
		
		public SqlConnection getConnection(){
			try{
				return this.connection;
			} catch (Exception e) {
				MessageBox.Show(e.ToString());
				return null;
			}
		}
		
		public SqlCommand getCommand(){
			try{
				return this.cmd;
			} catch (Exception e) {
				MessageBox.Show(e.ToString());
				return null;
			}
		}
		
		public void cleanup(){
			try{
				connection.Close();
			} catch (Exception e) {
				MessageBox.Show(e.ToString());
			}
		}
	
	}
}

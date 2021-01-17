/*
 * Created by SharpDevelop.
 * User: iryna
 * Date: 05/14/2020
 * Time: 09:43
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
	/// Description of Bank.
	/// </summary>
	public class Bank{
				
		public string Name {get; set;}
		public string Address {get; set;}
		public string City {get; set;}
		public string State {get; set;}
		public string Zip {get; set;}
		public string RoutingNum {get; set;}
		public string SwiftSortCode {get; set;}
		public string BranchInfo {get; set;}
		public string RoutingInstr {get; set;}
		public string Type {get; set;}
		
		public DatabaseHandler db = new DatabaseHandler();
		public SqlCommand cmd;
		
		public int insertInfo(){
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				
				cmd.CommandText = "INSERT INTO bank (name, address, city, state, zip, ABA_routing_num, swift_sort_code, branch, routing_instructions, bank_type) VALUES (@name, @address, @city, @state, @zip, @routing_num, @swift_sort_code, @branch, @routing_instr, @bank_type);" +
					"SELECT SCOPE_IDENTITY();";
				cmd.Parameters.Add(new SqlParameter("@name", this.Name));
				cmd.Parameters.Add(new SqlParameter("@address", this.Address));
				cmd.Parameters.Add(new SqlParameter("@city", this.City));
				cmd.Parameters.Add(new SqlParameter("@state", this.State));
				cmd.Parameters.Add(new SqlParameter("@zip", this.Zip));
				cmd.Parameters.Add(new SqlParameter("@routing_num", this.RoutingNum));
				cmd.Parameters.Add(new SqlParameter("@swift_sort_code", this.SwiftSortCode));
				cmd.Parameters.Add(new SqlParameter("@branch", this.BranchInfo));
				cmd.Parameters.Add(new SqlParameter("@routing_instr", this.RoutingInstr));
				cmd.Parameters.Add(new SqlParameter("@bank_type", this.Type));
				
				Int32 id = Convert.ToInt32(cmd.ExecuteScalar());
				return id;
			} catch (Exception e){
				MessageBox.Show(e.ToString());
				return -1;
			}
		}
		
		
		public int exists(string ABA_routing_num){
			// Checks if a Bank exists in the DB based on ABA routing num
			db.initializeConnection();
			cmd = db.getCommand();
			
			if (ABA_routing_num != String.Empty){
				try {
					cmd.CommandText = "SELECT COUNT(1) FROM bank WHERE ABA_routing_num='"+ABA_routing_num+"';";
					int result = Convert.ToInt32(cmd.ExecuteScalar());
					return result; 
				} catch (Exception e){
					MessageBox.Show(e.ToString());
					return 0;
				}
			}
			return 0;
		}
		
		public Bank retrieveBank(string ABA_routing_num){
			db.initializeConnection();
			cmd = db.getCommand();
			SqlDataReader reader;
			Bank bank = new Bank();
			
			cmd.CommandText = "SELECT bank.name, bank.address, bank.city, bank.state, bank.zip, bank.ABA_routing_num, bank.swift_sort_code, bank.branch, " +
			"bank.routing_instructions, bank.bank_type FROM bank WHERE bank.ABA_routing_num= '"+ABA_routing_num+"';";
			
			try{
				using (reader = cmd.ExecuteReader()){
					while (reader.Read()){
						bank.Name = reader.GetString(0);
						bank.Address = reader.GetString(1);
						bank.City = reader.GetString(2);
						bank.State = reader.GetString(3);
						//bank.Zip = reader.GetInt32(4).ToString();
						bank.Zip = reader.GetString(4);
						//bank.RoutingNum = reader.GetInt32(5).ToString();
						bank.RoutingNum = reader.GetString(5);
						//bank.SwiftSortCode = reader.GetInt32(6).ToString();
						bank.SwiftSortCode = reader.GetString(6);
						bank.BranchInfo = reader.GetString(7);
						bank.RoutingInstr = reader.GetString(8);
						bank.Type = reader.GetString(9);	
					}
				}
				return bank;
			} catch (Exception e){
				MessageBox.Show(e.ToString());
				return bank;
			}
		}
		
		public void updateBank(int bank_id){
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				
				cmd.CommandText = "UPDATE bank SET name=@name, address=@address, city=@city, state=@state, zip=@zip, ABA_routing_num=@ABA_routing_num, swift_sort_code=@swift_sort_code, " +
					"branch=@branch, routing_instructions=@routing_instructions WHERE bank.id="+bank_id+";";
				cmd.Parameters.Add(new SqlParameter("@name", this.Name));
				cmd.Parameters.Add(new SqlParameter("@address", this.Address));
				cmd.Parameters.Add(new SqlParameter("@city", this.City));
				cmd.Parameters.Add(new SqlParameter("@state", this.State));
				cmd.Parameters.Add(new SqlParameter("@zip", this.Zip));
				cmd.Parameters.Add(new SqlParameter("@routing_num", this.RoutingNum));
				cmd.Parameters.Add(new SqlParameter("@swift_sort_code", this.SwiftSortCode));
				cmd.Parameters.Add(new SqlParameter("@branch", this.BranchInfo));
				cmd.Parameters.Add(new SqlParameter("@routing_instr", this.RoutingInstr));
			} catch (Exception e){
				MessageBox.Show(e.ToString());
			}
		}
		
	}
}

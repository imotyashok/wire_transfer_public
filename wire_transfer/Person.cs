/*
 * Created by SharpDevelop.
 * User: iryna
 * Date: 05/14/2020
 * Time: 09:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace wire_transfer
{
	/// <summary>
	/// Description of Person.
	/// </summary>
	public class Person
	{
			
		public string Name { get; set; }
		public string Type { get; set; }  // "S" for Sender, "R" for Recipient
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set;}
		public string Zip { get; set; }
		
		public DatabaseHandler db = new DatabaseHandler();
		public SqlCommand cmd;
		
		public int insertInfo(){
			// Adds the person into the DB 
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				
				cmd.CommandText = "INSERT INTO person (id_type, name, address, city, state, zip) VALUES (@id_type, @name, @address, @city, @state, @zip); " +
					"SELECT SCOPE_IDENTITY();";
				cmd.Parameters.Add(new SqlParameter("@id_type", this.Type));
				cmd.Parameters.Add(new SqlParameter("@name", this.Name));
				cmd.Parameters.Add(new SqlParameter("@address", this.Address));
				cmd.Parameters.Add(new SqlParameter("@city", this.City));
				cmd.Parameters.Add(new SqlParameter("@state", this.State));
				cmd.Parameters.Add(new SqlParameter("@zip", this.Zip));
				
				Int32 id = Convert.ToInt32(cmd.ExecuteScalar());
				return id;
			
			} catch (Exception e){
				MessageBox.Show(e.ToString());
				return -1;
			}
			
		}
		
		public void updatePerson(int id){
			// Updates the person in DB
			try{
				db.initializeConnection();
				cmd = db.getCommand();
				cmd.CommandText = "UPDATE person SET name=@name, address=@address, city=@city, state=@state, zip=@zip WHERE person.id="+id+";";
				cmd.Parameters.Add(new SqlParameter("@name", this.Name));
				cmd.Parameters.Add(new SqlParameter("@address", this.Address));
				cmd.Parameters.Add(new SqlParameter("@city", this.City));
				cmd.Parameters.Add(new SqlParameter("@state", this.State));
				cmd.Parameters.Add(new SqlParameter("@zip", this.Zip));
				cmd.ExecuteNonQuery();
				
			} catch (Exception e){
				MessageBox.Show(e.ToString());
			}
		}
	
	}
		
		public class Sender : Person{
			
			public string Phone { get; set; }
			public string MemberNum { get; set; }
			public string SubAccount {get; set;}
		
			
			public void insertSenderInfo(int id){
				try{
					db.initializeConnection();
					cmd = db.getCommand();
					
					cmd.CommandText = "INSERT INTO sender (id, person_type, phone_num, member_num, sub_account) VALUES (@id, @id_type, @phone, @member_num, @sub_account);"; 				
					cmd.Parameters.Add(new SqlParameter("@id", id));
					cmd.Parameters.Add(new SqlParameter("@id_type", this.Type));
					cmd.Parameters.Add(new SqlParameter("@phone", this.Phone));
					cmd.Parameters.Add(new SqlParameter("@member_num", this.MemberNum));
					cmd.Parameters.Add(new SqlParameter("@sub_account", this.SubAccount));
					cmd.ExecuteNonQuery();
				} catch (Exception e){
					MessageBox.Show(e.ToString());
				}
			}
			
			public int exists(string member_num){
				// Checks if a Sender exists in the DB based on member number 
				if (member_num != String.Empty){
					try{
						db.initializeConnection();
						cmd = db.getCommand();
						
						cmd.CommandText = "SELECT COUNT(1) FROM sender WHERE member_num='"+member_num+"';";
						int result = Convert.ToInt32(cmd.ExecuteScalar());
						return result;
					} catch (Exception e){
						MessageBox.Show(e.ToString());
						return 0;
					}
				}
				return 0;
			}
			
			public Sender retrieveSender(string member_num){
				db.initializeConnection();
				cmd = db.getCommand();
				SqlDataReader reader;
				Sender sender = new Sender();
				
				cmd.CommandText = "SELECT person.name, person.address, person.city, person.state, person.zip, sender.phone_num, sender.member_num, sender.sub_account " +
				"FROM person INNER JOIN sender ON person.id = sender.id WHERE sender.member_num = '"+member_num+"';";
				try{
					using (reader = cmd.ExecuteReader()){
						while (reader.Read()){
							sender.Name = reader.GetString(0);
							sender.Address = reader.GetString(1);
							sender.City = reader.GetString(2);
							sender.State = reader.GetString(3);
							sender.Zip = reader.GetString(4);
							sender.Phone = reader.GetString(5);
							sender.MemberNum = reader.GetString(6);
							sender.SubAccount = reader.GetString(7);
						}
					}
				} catch (Exception e){
					MessageBox.Show(e.ToString());
				}
				
				return sender;
			}
			
			public void updateSender(int sender_id){
				try{
					db.initializeConnection();
					cmd = db.getCommand();
					cmd.CommandText = "UPDATE sender SET phone_num=@phone_num, member_num=@member_num, sub_account=@sub_account WHERE sender.id="+sender_id+";";
					cmd.Parameters.Add(new SqlParameter("@phone_num", this.Phone));
					cmd.Parameters.Add(new SqlParameter("@member_num", this.MemberNum));
					cmd.Parameters.Add(new SqlParameter("@sub_account", this.SubAccount));
					cmd.ExecuteNonQuery();
				} catch (Exception e){
					MessageBox.Show(e.ToString());
				}
			}
			
	}
		
		public class Recipient : Person{
			
			
			public string AccountNumIBAN { get; set; }
			public string SSN { get; set; }
			public string TIN { get; set; }
			public string DLNum { get; set; }
			public string Country { get; set; }
			public string PhoneNum { get; set; }
			
			public void insertRecipientInfo(int id){
				try{
					db.initializeConnection();
					cmd = db.getCommand();
					
					cmd.CommandText = "INSERT INTO recipient (id, person_type, country, phone_num, account_num_IBAN, SSN, TIN, DLnum) VALUES (@id, @id_type, @country, @phone_num, @account_num_IBAN, @SSN, @TIN, @DLnum);";  
					cmd.Parameters.Add(new SqlParameter("@id", id));
					cmd.Parameters.Add(new SqlParameter("@id_type", this.Type));
					cmd.Parameters.Add(new SqlParameter("@country", this.Country));
					cmd.Parameters.Add(new SqlParameter("@phone_num", this.PhoneNum));
					cmd.Parameters.Add(new SqlParameter("@account_num_IBAN", this.AccountNumIBAN));
					cmd.Parameters.Add(new SqlParameter("@SSN", this.SSN));
					cmd.Parameters.Add(new SqlParameter("@TIN", this.TIN));
					cmd.Parameters.Add(new SqlParameter("@DLnum", this.DLNum));
					cmd.ExecuteNonQuery();
				} catch (Exception e){
					MessageBox.Show(e.ToString());
				}
				
			}
			
			public int exists(string account_num_IBAN){
				// Checks if a Sender exists in the DB based on member number 
				if (account_num_IBAN != String.Empty){
					try{
						db.initializeConnection();
						cmd = db.getCommand();
						
						// INTERESTING (?) NOTE: the same command, but with ' ' around @account_num_IBAN didn't work...
						cmd.CommandText = "SELECT COUNT(1) FROM recipient WHERE account_num_IBAN=@account_num_IBAN;";
						cmd.Parameters.Add(new SqlParameter("@account_num_IBAN", account_num_IBAN));
						int result = Convert.ToInt32(cmd.ExecuteScalar());
						return result; 
					} catch (Exception e){
						MessageBox.Show(e.ToString());
						return 0;
					}
				}
				return 0;
			}
			
			public Recipient retrieveRecipient(string account_num_IBAN){
				db.initializeConnection();
				cmd = db.getCommand();
				SqlDataReader reader;
				Recipient recipient = new Recipient();
				
				cmd.CommandText = "SELECT person.name, person.address, person.city, person.state, person.zip, recipient.country, recipient.phone_num, recipient.account_num_IBAN, recipient.SSN, recipient.TIN, " +
				"recipient.DLnum FROM person INNER JOIN recipient ON person.id = recipient.id WHERE recipient.account_num_IBAN = '"+account_num_IBAN+"';";
				
				try{
					using (reader = cmd.ExecuteReader()){
						while (reader.Read()){
							recipient.Name = reader.GetString(0);
							recipient.Address = reader.GetString(1);
							recipient.City = reader.GetString(2);
							recipient.State = reader.GetString(3);
							recipient.Zip = reader.GetString(4);
							recipient.Country = reader.GetString(5);
							recipient.PhoneNum = reader.GetString(6);
							recipient.AccountNumIBAN = reader.GetString(7);
							recipient.SSN = reader.GetString(8);
							recipient.TIN = reader.GetString(9);  
							recipient.DLNum = reader.GetString(10);
						}
					}
				} catch (Exception e){
					MessageBox.Show(e.ToString());
				}
				
				return recipient;
			}
			
			public void updateRecipient(int recipient_id){
				try{
					db.initializeConnection();
					cmd = db.getCommand();
					cmd.CommandText = "UPDATE recipient SET country=@country, phone_num=@phone_num, account_num_IBAN=@account_num_IBAN, SSN=@SSN, TIN=@TIN, DLnum=@DLnum WHERE recipient.id="+recipient_id+";";
					cmd.Parameters.Add(new SqlParameter("@country", this.Country));
					cmd.Parameters.Add(new SqlParameter("@phone_num", this.PhoneNum));
					cmd.Parameters.Add(new SqlParameter("@account_num_IBAN", this.AccountNumIBAN));
					cmd.Parameters.Add(new SqlParameter("@SSN", this.SSN));
					cmd.Parameters.Add(new SqlParameter("@TIN", this.TIN));
					cmd.Parameters.Add(new SqlParameter("@DLnum", this.DLNum));
					cmd.ExecuteNonQuery();
				} catch (Exception e){
					MessageBox.Show(e.ToString());
				}
			}
			
		}
	
}

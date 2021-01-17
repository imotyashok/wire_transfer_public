/*
 * Created by SharpDevelop.
 * User: iryna
 * Date: 5/18/2020
 * Time: 10:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace wire_transfer
{
	/// <summary>
	/// Description of Dropdowns.
	/// </summary>
	public class Dropdowns
	{
		public Dropdowns()
		{
		}
	
		public string[] states; 
		public string[] States 
		{
			get{
				return states;
			}
			set{
		    states = new string[51]{"N/A","AK","AL","AR","AZ","CA","CO","CT","DE","FL","GA","HI","IA","ID","IL","IN","KS","KY",
				"LA","MA","MD","ME","MI","MN","MO","MS","MT","NC","ND","NE","NH","NJ","NM","NV","NY","OH","OK","OR","PA","RI",
				"SC","SD","TN","TX","UT","VA","VT","WA","WI","WV","WY"
			};
			}
			//return states_array; 
		}
		
//		public string[] Countries(){
//			
//		}
	}
}

using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Net.Mail;
using System.Windows.Forms;

using Microsoft.Win32;

namespace CalPal
{
	/// <summary>
	/// Summary description for TheApp.
	/// </summary>
	class TheApp
	{
		private static Hashtable ms_htMessages;
  private static string ms_strDataFile;
  private const  string m_strCaption = "Calendar Pal";

  public static string Caption
  {
    get { return m_strCaption; }
  }

		public static Hashtable MsgDict
		{
			set { ms_htMessages = value; }
			get { return ms_htMessages; }
		}

		public static String DataFile
		{
			get { return ms_strDataFile; }
   set { ms_strDataFile = Path.Combine (DataFolder,value); }
		}

		public static String DataFolder
		{
			get { return Path.Combine (Application.StartupPath,"Messages"); }
		}

		/// <summary>
		/// The main entry point for the application.r
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			RegistryKey appKey = Registry.CurrentUser.CreateSubKey(@"Software\CalendarPal");	
   string strEmailAddress = (string)appKey.GetValue("emailAddress",string.Empty);
			appKey.Close();

   DataFile = strEmailAddress;

//   DownloadMsgFile();

			if ( args.Length > 0 && args[0].ToLower().Equals("-batchmode") )
			{    
		  ProcessMessages();
			}
			else
			{
				if ( strEmailAddress.Length == 0 )
    {
					AppInitialize();

     appKey = Registry.CurrentUser.CreateSubKey (@"Software\CalendarPal");
				 strEmailAddress = (string)appKey.GetValue("emailAddress");
				 appKey.Close();

     DataFile = strEmailAddress;
    }

				LoadMessages(DataFile);			
				RemoveObsoleteMsgs();
	
				Application.Run(new MainForm());
			}
		}

		private static void AppInitialize()
		{
			MessageBox.Show("Welcome to the Calendar Pal reminder system.\n\n" +
				"Because this application automatically generates\n" +
				"and sends email messages, you may need to adjust\n" +
				"some settings in certain virus detection applications.\n\n" +
				"Thank you for purchasing Calendar Pal.",
				"Welcome to Calendar Pal",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);

			EmailAddrForm emailAddrForm = new EmailAddrForm();			
			
			while ( emailAddrForm.ShowDialog() != DialogResult.OK ||
				     !emailAddrForm.VerifyEmailAddress() )
			{
				MessageBox.Show("Please supply a valid email address.\n\n",
					"CalendarPal - Setup Incomplete",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			}

			emailAddrForm.Dispose();
		}

		private static void ProcessMessages()
		{
			RegistryKey appKey = Registry.CurrentUser.OpenSubKey(@"Software\CalendarPal");

			String strSmtpServer = (String)appKey.GetValue("smtpServer");

			appKey.Close();

   DirectoryInfo dirInfo = new DirectoryInfo (DataFolder);
			FileInfo [] vFileInfo = dirInfo.GetFiles("*");

			foreach ( FileInfo fileInfo in vFileInfo )
			{
				LoadMessages(dirInfo.FullName + "\\" + fileInfo);
				SendMessages(GetOutgoingMessages(),
					fileInfo.Name,
					strSmtpServer);
			}
		}

  public static void DownloadMsgFile ()
  {
			 if ( !Directory.Exists(DataFolder) )
			 		Directory.CreateDirectory(DataFolder);

    WebClient requestWeb = new WebClient ();
    requestWeb.Credentials = new NetworkCredential ("pkc\\contentftpuser", "p3Nguin");

    string strURI = Path.Combine ("ftp://contentftp.pkc.com/Content/ContentWksBackups/jacant",
                                  Path.GetFileName(DataFile));

    try
    {
      requestWeb.DownloadFile (strURI, DataFile);    
    }
    catch (System.Net.WebException Excp)
    {
      MessageBox.Show (Excp.Message,Caption);
    }  
  }

  public static void UploadMsgFile ()
  {
    WebClient requestWeb   = new WebClient ();
    requestWeb.Credentials = new NetworkCredential("pkc\\contentftpuser", "p3Nguin");

    string strURI = Path.Combine ("ftp://contentftp.pkc.com/Content/ContentWksBackups/jacant",
                                  Path.GetFileName(DataFile));

    try
    {
      requestWeb.UploadFile(strURI, DataFile);
    }
    catch (System.Net.WebException Excp) 
    {
      MessageBox.Show (Excp.Message,Caption);
    }
  }

		private static void LoadMessages(String filePath)
		{
			if ( File.Exists(filePath) )
			{
				Stream stream = new FileStream(filePath, 
				                              	FileMode.Open, 
			                              		FileAccess.Read, 
			                              		FileShare.None);
				IFormatter formatter = new BinaryFormatter();
				MsgDict = (Hashtable)formatter.Deserialize(stream);
				stream.Close();
			}
			else
			{
				MsgDict = new Hashtable();
			}
		}

		private static void RemoveObsoleteMsgs()
		{
			DateTime yesterday = DateTime.Today.AddDays(-1);

			ArrayList alMessages = (ArrayList)MsgDict[yesterday.Day];

			if ( alMessages != null )
			{
				for ( int i = 0; i < alMessages.Count; i++ )
				{
					Message aMessage = (Message)alMessages[i];
					if ( aMessage.RecurFrequency == Message.Recurs.enNone )
					{
						alMessages.Remove(aMessage);
						i--;
					}
				}
				if ( alMessages.Count == 0 )	// if no message left, remove key
				{
					MsgDict.Remove(yesterday.Day);
				}
				MainForm.Commit();
			}
		}

		private static ArrayList GetOutgoingMessages()
		{
			ArrayList outgoingMsgs = new ArrayList();
			DateTime today = DateTime.Today;

			for ( int i = 0; i < 6; i++ )
			{
				DateTime todayPlus = today.AddDays(i);

				ArrayList alMessages = (ArrayList)MsgDict[todayPlus.Day];
				
				if ( alMessages != null )
				{
					foreach ( Message message in alMessages )
					{
						switch ( message.RecurFrequency )
						{
							case Message.Recurs.enNone :
								if ( message.SendDate.CompareTo(today) == 0 )
								{
									outgoingMsgs.Add(message);
								}
								break;
							case Message.Recurs.enYearly :
								if ( message.SendDate.Month == today.Month )
								{
									if ( today.Month == 2 ) // reset date to account for leap year
									{
										message.Date = todayPlus;										
									}
									if ( message.SendDate.Day == today.Day ) 
									{
										outgoingMsgs.Add(message);
									}
								}
								break;
							case Message.Recurs.enMonthly :
								message.Date = todayPlus;	// reset date to account for month length variability
								if ( message.SendDate.Day == today.Day )
								{
									outgoingMsgs.Add(message);
								}
								break;
							default :
								break;
						}
					}
				}
			}
			return outgoingMsgs;
		}

		private static void SendMessages(ArrayList outgoingMsgs, String emailAddress, String smtpServer)
		{
			if ( outgoingMsgs.Count == 0 ) return;

			if ( IsConnectedToInternet(SystemInformation.ComputerName) )
			{
				int nSendInterval = 3000;

				foreach ( Message msg in outgoingMsgs )
				{
     MailAddress from = new MailAddress (emailAddress);
     MailAddress to   = new MailAddress (emailAddress);


					MailMessage emailMessage = new MailMessage(from, to);
					emailMessage.Subject = msg.Text;
					emailMessage.Body = msg.SubItems[1].Text;                            

					SmtpClient mailClient = new SmtpClient (smtpServer, 587);
                                        mailClient.EnableSsl = true;
                                        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        mailClient.UseDefaultCredentials = false;
                                        mailClient.Credentials = new NetworkCredential ("jrc12","cjanesew");
                                        
					mailClient.Send (emailMessage);

					Thread.Sleep(nSendInterval);	// allow for delay b/w message sends
				}
			}
		}

		private static bool IsConnectedToInternet(String strMachine)
		{
      bool bConnected = true;
      do
      {
        if (strMachine == null || strMachine.Length == 0)
          break;
        try 
        {
          //////////////////////////////////////////////////////////
          // Resolve the machine name to its current IP address(es).
          //////////////////////////////////////////////////////////

          IPHostEntry HEntry = Dns.GetHostEntry (strMachine);
          if (HEntry == null)
            throw new ApplicationException (string.Format ("Can't resolve machine name {0}",strMachine));

          ////////////////////////////////////////////////////////////////////////
          // Machines connected to internet resolve to some IP adresse.  
          ////////////////////////////////////////////////////////////////////////

          IPAddress [] IPAddr = HEntry.AddressList;

          foreach ( IPAddress address in IPAddr )
          { 
            if ( IPAddress.IsLoopback(address) )
            {
              bConnected = false;
              break;
            }
          }
        }
        catch (Exception Excpt) 
        {
          string strError = string.Format ("ConnectedToInternet error: {0}",Excpt.Message);
          MessageBox.Show (strError,"CalendarPal");
        }
      }
      while (false);

      return (bConnected);
		}
	}
}

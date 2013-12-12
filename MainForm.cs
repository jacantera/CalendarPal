using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

using Microsoft.Win32;

namespace CalPal
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.NotifyIcon m_niSysTray;
		private System.ComponentModel.IContainer components;

		private Icon [] m_vIcons;
		private CalForm m_calForm;

        private System.Threading.Timer m_Timer;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
				
			SetupSysTrayIcon();
		}

        private void TimerAction(object e)
        {
            m_niSysTray.Icon = m_vIcons[DateTime.Today.Day];		// set sys tray icon to current day
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
                  this.components = new System.ComponentModel.Container();
                  this.m_niSysTray = new System.Windows.Forms.NotifyIcon(this.components);
                  this.SuspendLayout();
                  // 
                  // m_niSysTray
                  // 
                  this.m_niSysTray.Visible = true;
                  this.m_niSysTray.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SysTrayClickEvent);
                  // 
                  // MainForm
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                  this.ClientSize = new System.Drawing.Size(240,22);
                  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                  this.Name = "MainForm";
                  this.ShowInTaskbar = false;
                  this.Text = "MainForm";
                  this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                  this.Load += new System.EventHandler(this.MainForm_Load);
                  this.ResumeLayout(false);

    }
		#endregion

		private void MainForm_Load(object sender, System.EventArgs e)
		{            
			m_vIcons = new Icon[32];		// ignore 0 index

			Assembly assembly = Assembly.GetExecutingAssembly();
    
			String [] vstrResNames = assembly.GetManifestResourceNames();
			for( int i=0; i < vstrResNames.Length; i++ )
			{
				String strName = vstrResNames[i];
				if(strName.EndsWith(".ico"))
				{				
					Regex regex = new Regex("[0-9]+");
					int nIndex = Int32.Parse(regex.Match(strName).Value);
					m_vIcons[nIndex] = new Icon(assembly.GetManifestResourceStream(strName));
				}
			}         
			m_niSysTray.Icon = m_vIcons[DateTime.Today.Day];		// set sys tray icon to current day

            SetTimerValue();
		}

		private void SetupSysTrayIcon()
		{
			// setup up tool tip
			m_niSysTray.Text = DateTime.Today.ToLongDateString();

			// setup context menu
			MenuItem menuEmail = new MenuItem("Email Setup");
			menuEmail.Click += new EventHandler(MenuEmailClickEvent);
			MenuItem menuAbout = new MenuItem("About...");
			menuAbout.Click += new EventHandler(MenuAboutClickEvent);
			MenuItem menuExit = new MenuItem("Exit");
			menuExit.Click += new EventHandler(MenuExitClickEvent);

			MenuItem menuSeparator = new MenuItem();
			menuSeparator.Text = "-";

			MenuItem [] menuItems = { menuEmail, menuSeparator, menuAbout, menuExit };
			m_niSysTray.ContextMenu = new ContextMenu(menuItems);
		}

		private void SysTrayClickEvent(Object sender, MouseEventArgs e)
		{	
			if ( e.Button == MouseButtons.Left )
			{
				if ( m_calForm == null )
				{
					m_calForm = new CalForm();
					m_calForm.Closing += new CancelEventHandler(CalForm_Closing);
				}
				
				m_calForm.ShowCalendar();
			}
		}

		private void CalForm_Closing(object sender, CancelEventArgs args)
		{
			m_calForm = null;
		}

        private void SetTimerValue()
        {
            string g = "Hello World";
            // trigger the event at midnight
            DateTime midnight = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 0, 0, 5);

            // trigger the event at midnight
            DateTime midnight2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 0, 0, 5);

            // interval between the timer events is 24 hours
            TimeSpan interval2 = new TimeSpan(24, 0, 0);
            int d = 0;
            m_Timer = new System.Threading.Timer(new TimerCallback(TimerAction), null, midnight2.Subtract(DateTime.Now), interval2);
            int e = 3;

            // interval between the timer events is 24 hours
            string k = "Goodbye";
            TimeSpan interval = new TimeSpan(24, 0, 0);
            int a = 0;
            m_Timer = new System.Threading.Timer(new TimerCallback(TimerAction), null, midnight.Subtract(DateTime.Now), interval);
            int b = 3;
        }

		private void MenuEmailClickEvent(object sender, EventArgs e)
		{
			EmailAddrForm emailAddrForm = new EmailAddrForm();			
			DialogResult result = emailAddrForm.ShowDialog();

			while ( !emailAddrForm.VerifyEmailAddress() &&
				    result != DialogResult.Cancel )
			{
				MessageBox.Show("Please supply a valid email address.\n\n",
					"CalendarPal - Setup Incomplete",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);

				result = emailAddrForm.ShowDialog();
			}

			emailAddrForm.Dispose();
		}

		private void MenuAboutClickEvent(object sender, EventArgs e)
		{
			FileVersionInfo fviCalPal = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
			DialogResult drResponse = MessageBox.Show("Calendar Pal\nVersion " + fviCalPal.FileVersion + "\n\nCopyright © JAC Industries 2004\nAll rights reserved.",
				"About Calendar Pal", MessageBoxButtons.OK);
		}

		private void MenuExitClickEvent(object sender, EventArgs e)
		{
			Application.Exit();
		}

		public static void AddMsg(Message oMsg, int nDay)
		{
			if ( TheApp.MsgDict.ContainsKey(nDay) )
				((ArrayList)TheApp.MsgDict[nDay]).Add(oMsg);
			else
			{
				ArrayList alMsgs = new ArrayList();
				alMsgs.Add(oMsg);
				TheApp.MsgDict[nDay] = alMsgs;
			}
		}

		public static void Commit()
		{
   Cursor.Current = Cursors.WaitCursor;

//   TheApp.DownloadMsgFile ();

			if ( !File.Exists(TheApp.DataFile) )
			{
					File.Create(TheApp.DataFile).Close();
			}

			FileStream stream;
			IFormatter formatter;

			stream = new FileStream(TheApp.DataFile, 
		                       		FileMode.Open, 
			                       	FileAccess.Write, 
				                       FileShare.None);

   stream.SetLength (0);
			formatter = new BinaryFormatter();		
			formatter.Serialize(stream,TheApp.MsgDict);
			stream.Close();

//   TheApp.UploadMsgFile ();

   Cursor.Current = Cursors.Default;
		}

		public static Hashtable Messages
		{
			get { return TheApp.MsgDict; }
		}

		public static ArrayList MsgsForDay(int nDay)
		{
			return (ArrayList)TheApp.MsgDict[nDay];
		}

		public static bool MsgExistsForDay(int nDay)
		{
			if ( TheApp.MsgDict ==  null ) return false;

			return TheApp.MsgDict.ContainsKey(nDay);
		}
	}
}

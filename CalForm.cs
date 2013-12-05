using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CalPal
{
	/// <summary>
	/// Summary description for CalForm.
	/// </summary>
	public class CalForm : System.Windows.Forms.Form
	{
		private MsgForm m_msgForm;

		private System.Windows.Forms.MonthCalendar m_calMonthCurrent;
		private System.Windows.Forms.MonthCalendar m_calMonthNext;

		private System.ComponentModel.IContainer components = null;

		public CalForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
	
			SetLocation();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
                  this.m_calMonthCurrent = new System.Windows.Forms.MonthCalendar();
                  this.m_calMonthNext = new System.Windows.Forms.MonthCalendar();
                  this.SuspendLayout();
                  // 
                  // m_calMonthCurrent
                  // 
                  this.m_calMonthCurrent.Location = new System.Drawing.Point(0,0);
                  this.m_calMonthCurrent.MaxSelectionCount = 1;
                  this.m_calMonthCurrent.Name = "m_calMonthCurrent";
                  this.m_calMonthCurrent.TabIndex = 0;
                  this.m_calMonthCurrent.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.DateSelectedEvent);
                  // 
                  // m_calMonthNext
                  // 
                  this.m_calMonthNext.Location = new System.Drawing.Point(208,0);
                  this.m_calMonthNext.MaxSelectionCount = 1;
                  this.m_calMonthNext.Name = "m_calMonthNext";
                  this.m_calMonthNext.ShowToday = false;
                  this.m_calMonthNext.TabIndex = 1;
                  this.m_calMonthNext.TodayDate = new System.DateTime(2004,6,27,0,0,0,0);
                  this.m_calMonthNext.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.DateSelectedEvent);
                  // 
                  // CalForm
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                  this.ClientSize = new System.Drawing.Size(410,152);
                  this.Controls.Add(this.m_calMonthNext);
                  this.Controls.Add(this.m_calMonthCurrent);
                  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                  this.Name = "CalForm";
                  this.ShowInTaskbar = false;
                  this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                  this.Text = "Calendar Pal";
                  this.Closed += new System.EventHandler(this.CalForm_Closed);
                  this.ResumeLayout(false);

		}
		#endregion
	
		public void ShowCalendar()
		{
			m_calMonthCurrent.SelectionStart = DateTime.Today;
			SetNextMonth();
			BoldDatesWithMsg();
			this.Show();
			this.Activate();
		}

		private void SetNextMonth()
		{
			m_calMonthNext.SetDate(DateTime.Today.AddMonths(1));
		}

		private void SetLocation()
		{
			Rectangle rectWorkArea = Screen.PrimaryScreen.WorkingArea;
			this.Location = new Point(rectWorkArea.Width-this.Size.Width,
			rectWorkArea.Height-this.Size.Height);
		}

		private void DateSelectedEvent(object sender,
			DateRangeEventArgs e)
		{
			if ( m_msgForm == null )
			{
				m_msgForm = new MsgForm();
				m_msgForm.Closing += new CancelEventHandler(MsgForm_Closing);
			}

			m_msgForm.UpdateMsgForm(e.Start);
			m_msgForm.Show();
			m_msgForm.Activate();
		}

		private void BoldDatesWithMsg()
		{
			ArrayList datesSingle = new ArrayList();
			ArrayList datesMonthly = new ArrayList();
			ArrayList datesYearly = new ArrayList();
			
			Hashtable htMessagesByDay = MainForm.Messages;
			ICollection days = htMessagesByDay.Keys;	

			foreach ( int nDay in days )
			{ 
				ArrayList alMessages = (ArrayList)htMessagesByDay[nDay];

				Message.Recurs recurFrequency = Message.Recurs.enNone;

				foreach ( Message message in alMessages )
				{
					recurFrequency = message.RecurFrequency;

					switch ( recurFrequency )
					{
						case Message.Recurs.enNone : 
							datesSingle.Add(message.Date);
							break;
						case Message.Recurs.enMonthly : 
							datesMonthly.Add(message.Date);
							break;
						case Message.Recurs.enYearly : 
							datesYearly.Add(message.Date);
							break;
						default :
							break;
					}
				}
			}
			m_calMonthCurrent.BoldedDates = (DateTime [])datesSingle.ToArray(Type.GetType("System.DateTime"));
			m_calMonthCurrent.MonthlyBoldedDates = (DateTime [])datesMonthly.ToArray(Type.GetType("System.DateTime"));;
			m_calMonthCurrent.AnnuallyBoldedDates = (DateTime [])datesYearly.ToArray(Type.GetType("System.DateTime"));;

			m_calMonthNext.BoldedDates = (DateTime [])datesSingle.ToArray(Type.GetType("System.DateTime"));;
			m_calMonthNext.MonthlyBoldedDates = (DateTime [])datesMonthly.ToArray(Type.GetType("System.DateTime"));;
			m_calMonthNext.AnnuallyBoldedDates = (DateTime [])datesYearly.ToArray(Type.GetType("System.DateTime"));;
		}

		private void MsgForm_Closing(object sender, CancelEventArgs args)
		{
			BoldDatesWithMsg();
			m_msgForm = null;
		}

		private void CalForm_Closed(object sender, EventArgs args)
		{
			if ( m_msgForm != null )
			{
				m_msgForm.Close();
				m_msgForm = null;
			}
			GC.Collect();
		}
	}
}

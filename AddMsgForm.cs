using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CalPal
{
	/// <summary>
	/// Summary description for AddMsgForm.
	/// </summary>
	public class AddMsgForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox m_txtboxSubj;
		private System.Windows.Forms.Label m_lblSubj;
		private System.Windows.Forms.Label m_lblMsg;
		private System.Windows.Forms.RichTextBox m_rchboxMsg;
		private System.Windows.Forms.ComboBox m_cmbboxRecur;
		private System.Windows.Forms.Label m_lblRecur;
		private System.Windows.Forms.ComboBox m_cmbboxLeadTime;
		private System.Windows.Forms.Label m_lblLeadTime;
		private System.Windows.Forms.Label m_lblDays;
		private System.Windows.Forms.Button m_btnSave;
		private System.Windows.Forms.Button m_btnCancel;

		private System.ComponentModel.Container components = null;

		private DateTime m_Date;

		public AddMsgForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
                  this.m_txtboxSubj = new System.Windows.Forms.TextBox();
                  this.m_lblSubj = new System.Windows.Forms.Label();
                  this.m_lblMsg = new System.Windows.Forms.Label();
                  this.m_rchboxMsg = new System.Windows.Forms.RichTextBox();
                  this.m_cmbboxRecur = new System.Windows.Forms.ComboBox();
                  this.m_lblRecur = new System.Windows.Forms.Label();
                  this.m_cmbboxLeadTime = new System.Windows.Forms.ComboBox();
                  this.m_lblLeadTime = new System.Windows.Forms.Label();
                  this.m_lblDays = new System.Windows.Forms.Label();
                  this.m_btnSave = new System.Windows.Forms.Button();
                  this.m_btnCancel = new System.Windows.Forms.Button();
                  this.SuspendLayout();
                  // 
                  // m_txtboxSubj
                  // 
                  this.m_txtboxSubj.Location = new System.Drawing.Point(16,32);
                  this.m_txtboxSubj.Name = "m_txtboxSubj";
                  this.m_txtboxSubj.Size = new System.Drawing.Size(280,20);
                  this.m_txtboxSubj.TabIndex = 0;
                  // 
                  // m_lblSubj
                  // 
                  this.m_lblSubj.Location = new System.Drawing.Point(16,8);
                  this.m_lblSubj.Name = "m_lblSubj";
                  this.m_lblSubj.Size = new System.Drawing.Size(48,23);
                  this.m_lblSubj.TabIndex = 1;
                  this.m_lblSubj.Text = "Subject:";
                  this.m_lblSubj.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                  // 
                  // m_lblMsg
                  // 
                  this.m_lblMsg.Location = new System.Drawing.Point(16,64);
                  this.m_lblMsg.Name = "m_lblMsg";
                  this.m_lblMsg.Size = new System.Drawing.Size(56,23);
                  this.m_lblMsg.TabIndex = 2;
                  this.m_lblMsg.Text = "Message:";
                  this.m_lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                  // 
                  // m_rchboxMsg
                  // 
                  this.m_rchboxMsg.Location = new System.Drawing.Point(16,88);
                  this.m_rchboxMsg.Name = "m_rchboxMsg";
                  this.m_rchboxMsg.Size = new System.Drawing.Size(280,72);
                  this.m_rchboxMsg.TabIndex = 3;
                  this.m_rchboxMsg.Text = "";
                  // 
                  // m_cmbboxRecur
                  // 
                  this.m_cmbboxRecur.Items.AddRange(new object[] {
            "None",
            "Monthly",
            "Annually"});
                  this.m_cmbboxRecur.Location = new System.Drawing.Point(56,166);
                  this.m_cmbboxRecur.Name = "m_cmbboxRecur";
                  this.m_cmbboxRecur.Size = new System.Drawing.Size(68,21);
                  this.m_cmbboxRecur.TabIndex = 4;
                  // 
                  // m_lblRecur
                  // 
                  this.m_lblRecur.Location = new System.Drawing.Point(-2,167);
                  this.m_lblRecur.Name = "m_lblRecur";
                  this.m_lblRecur.Size = new System.Drawing.Size(52,21);
                  this.m_lblRecur.TabIndex = 5;
                  this.m_lblRecur.Text = "Recurs";
                  this.m_lblRecur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                  // 
                  // m_cmbboxLeadTime
                  // 
                  this.m_cmbboxLeadTime.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
                  this.m_cmbboxLeadTime.Location = new System.Drawing.Point(177,167);
                  this.m_cmbboxLeadTime.Name = "m_cmbboxLeadTime";
                  this.m_cmbboxLeadTime.Size = new System.Drawing.Size(34,21);
                  this.m_cmbboxLeadTime.TabIndex = 6;
                  // 
                  // m_lblLeadTime
                  // 
                  this.m_lblLeadTime.Location = new System.Drawing.Point(139,167);
                  this.m_lblLeadTime.Name = "m_lblLeadTime";
                  this.m_lblLeadTime.Size = new System.Drawing.Size(32,21);
                  this.m_lblLeadTime.TabIndex = 7;
                  this.m_lblLeadTime.Text = "Send";
                  this.m_lblLeadTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                  // 
                  // m_lblDays
                  // 
                  this.m_lblDays.Location = new System.Drawing.Point(217,167);
                  this.m_lblDays.Name = "m_lblDays";
                  this.m_lblDays.Size = new System.Drawing.Size(88,21);
                  this.m_lblDays.TabIndex = 8;
                  this.m_lblDays.Text = "days in advance";
                  this.m_lblDays.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                  // 
                  // m_btnSave
                  // 
                  this.m_btnSave.DialogResult = System.Windows.Forms.DialogResult.Yes;
                  this.m_btnSave.Location = new System.Drawing.Point(136,208);
                  this.m_btnSave.Name = "m_btnSave";
                  this.m_btnSave.Size = new System.Drawing.Size(75,23);
                  this.m_btnSave.TabIndex = 9;
                  this.m_btnSave.Text = "Save";
                  // 
                  // m_btnCancel
                  // 
                  this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                  this.m_btnCancel.Location = new System.Drawing.Point(224,208);
                  this.m_btnCancel.Name = "m_btnCancel";
                  this.m_btnCancel.Size = new System.Drawing.Size(75,23);
                  this.m_btnCancel.TabIndex = 10;
                  this.m_btnCancel.Text = "Cancel";
                  // 
                  // AddMsgForm
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                  this.ClientSize = new System.Drawing.Size(314,240);
                  this.Controls.Add(this.m_btnCancel);
                  this.Controls.Add(this.m_btnSave);
                  this.Controls.Add(this.m_lblDays);
                  this.Controls.Add(this.m_lblLeadTime);
                  this.Controls.Add(this.m_cmbboxLeadTime);
                  this.Controls.Add(this.m_lblRecur);
                  this.Controls.Add(this.m_cmbboxRecur);
                  this.Controls.Add(this.m_rchboxMsg);
                  this.Controls.Add(this.m_lblMsg);
                  this.Controls.Add(this.m_lblSubj);
                  this.Controls.Add(this.m_txtboxSubj);
                  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                  this.Name = "AddMsgForm";
                  this.ShowInTaskbar = false;
                  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                  this.ResumeLayout(false);
                  this.PerformLayout();

    }
		#endregion

		public void ClearForm(DateTime oDate)
		{
			m_Date = oDate;
			this.Text = m_Date.ToLongDateString();
			this.m_txtboxSubj.Text = "";
			this.m_rchboxMsg.Text = "";
			this.m_cmbboxRecur.SelectedIndex = 0;
			this.m_cmbboxLeadTime.SelectedIndex = 0;
		}

		public void PopulateForm(DateTime oDate, Message oMessage)
		{
			m_Date = oDate;
			this.Text = m_Date.ToLongDateString();
			this.m_txtboxSubj.Text = oMessage.Subject;
			this.m_rchboxMsg.Text = oMessage.Body;
			this.m_cmbboxRecur.SelectedIndex = (int)oMessage.RecurFrequency;
			this.m_cmbboxLeadTime.SelectedIndex = oMessage.LeadTime;
		}

		public bool SetMessage(Message oMsg)
		{
			bool bIsValidMsg = CheckValidation();

			if ( bIsValidMsg )
			{
				if ( oMsg == null )
				{
					oMsg = new Message(m_txtboxSubj.Text,m_rchboxMsg.Text,
						m_cmbboxRecur.SelectedIndex,m_cmbboxRecur.SelectedItem.ToString(),
						m_cmbboxLeadTime.SelectedIndex,m_Date);
					MainForm.AddMsg(oMsg,m_Date.Day);
				}
				else
				{
					oMsg.Subject = m_txtboxSubj.Text;
					oMsg.Body = m_rchboxMsg.Text;
					oMsg.strRecurFrequency = m_cmbboxRecur.SelectedItem.ToString();
					oMsg.RecurFrequency = (Message.Recurs)m_cmbboxRecur.SelectedIndex;
					oMsg.LeadTime = m_cmbboxLeadTime.SelectedIndex;
					oMsg.Date = m_Date;
				}
			}
			return bIsValidMsg;
		}

		private bool CheckValidation()
		{
			bool bIsValidMsg = true;

			if ( m_cmbboxRecur.SelectedIndex == 0 && 
				 m_Date.CompareTo(DateTime.Today) < 0 )
			{
				MessageBox.Show("The date of this message is in the past.\n" +
					"It must have a recur value other than None.",
					"CalendarPal - Invalid Recur Value",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				bIsValidMsg = false;
			}
			else if ( m_cmbboxRecur.SelectedIndex == 0 && 
				 m_Date.AddDays(-m_cmbboxLeadTime.SelectedIndex).CompareTo(DateTime.Today) <= 0 )
			{
				MessageBox.Show("The message cannot have a send date earlier than tomorrow.\n" +
					"Reduce the number of days in advance to send the message,\n" +
					"or set the message to recur.",
					"CalendarPal - Invalid Send Date",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				bIsValidMsg = false;
			}
			else if ( m_cmbboxRecur.SelectedIndex == 1 && 
				 m_Date.Day > 28 )
			{
				MessageBox.Show("The message cannot be set to recur monthly\n" +
					"since not all months contain a day " + m_Date.Day + ".",
					"CalendarPal - Invalid Recur Value",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				bIsValidMsg = false;
			}
			else if ( m_cmbboxRecur.SelectedIndex == 2 && 
				 m_Date.Month == 2 && m_Date.Day == 29 )
			{
				MessageBox.Show("The message cannot be set to recur annually since\n" +
					"February 29 recurs once every four years.",
					"CalendarPal - Invalid Recur Value",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				bIsValidMsg = false;
			}
			return bIsValidMsg;
		}
	}
}

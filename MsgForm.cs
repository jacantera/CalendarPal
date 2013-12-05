using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CalPal
{
	/// <summary>
	/// Summary description for MsgForm.
	/// </summary>
	public class MsgForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_btnAddMsg;
		private System.Windows.Forms.Button m_btnDelMsg;
		private System.Windows.Forms.Button m_btnEditMsg;
		private System.Windows.Forms.ListView m_lstvwMsgs;
		private System.Windows.Forms.ColumnHeader m_colhdrSubj;
		private System.Windows.Forms.ColumnHeader m_colhdrMsgTxt;
		private System.Windows.Forms.ColumnHeader m_colhdrRecur;
		private System.Windows.Forms.ColumnHeader m_colhdrSendDate;
		
		private System.ComponentModel.Container components = null;

		private DateTime m_Date;
		private AddMsgForm m_addMsgForm;

		public MsgForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_addMsgForm = new AddMsgForm();
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
                  this.m_btnAddMsg = new System.Windows.Forms.Button();
                  this.m_lstvwMsgs = new System.Windows.Forms.ListView();
                  this.m_colhdrSubj = new System.Windows.Forms.ColumnHeader();
                  this.m_colhdrMsgTxt = new System.Windows.Forms.ColumnHeader();
                  this.m_colhdrRecur = new System.Windows.Forms.ColumnHeader();
                  this.m_colhdrSendDate = new System.Windows.Forms.ColumnHeader();
                  this.m_btnDelMsg = new System.Windows.Forms.Button();
                  this.m_btnEditMsg = new System.Windows.Forms.Button();
                  this.SuspendLayout();
                  // 
                  // m_btnAddMsg
                  // 
                  this.m_btnAddMsg.Location = new System.Drawing.Point(48,128);
                  this.m_btnAddMsg.Name = "m_btnAddMsg";
                  this.m_btnAddMsg.Size = new System.Drawing.Size(75,23);
                  this.m_btnAddMsg.TabIndex = 0;
                  this.m_btnAddMsg.Text = "Add";
                  this.m_btnAddMsg.Click += new System.EventHandler(this.AddMsg_Click);
                  // 
                  // m_lstvwMsgs
                  // 
                  this.m_lstvwMsgs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colhdrSubj,
            this.m_colhdrMsgTxt,
            this.m_colhdrRecur,
            this.m_colhdrSendDate});
                  this.m_lstvwMsgs.FullRowSelect = true;
                  this.m_lstvwMsgs.GridLines = true;
                  this.m_lstvwMsgs.Location = new System.Drawing.Point(0,0);
                  this.m_lstvwMsgs.MultiSelect = false;
                  this.m_lstvwMsgs.Name = "m_lstvwMsgs";
                  this.m_lstvwMsgs.Size = new System.Drawing.Size(384,112);
                  this.m_lstvwMsgs.TabIndex = 3;
                  this.m_lstvwMsgs.UseCompatibleStateImageBehavior = false;
                  this.m_lstvwMsgs.View = System.Windows.Forms.View.Details;
                  // 
                  // m_colhdrSubj
                  // 
                  this.m_colhdrSubj.Text = "Subject";
                  this.m_colhdrSubj.Width = 73;
                  // 
                  // m_colhdrMsgTxt
                  // 
                  this.m_colhdrMsgTxt.Text = "Message";
                  this.m_colhdrMsgTxt.Width = 200;
                  // 
                  // m_colhdrRecur
                  // 
                  this.m_colhdrRecur.Text = "Recurs";
                  this.m_colhdrRecur.Width = 51;
                  // 
                  // m_colhdrSendDate
                  // 
                  this.m_colhdrSendDate.Text = "Send";
                  this.m_colhdrSendDate.Width = 51;
                  // 
                  // m_btnDelMsg
                  // 
                  this.m_btnDelMsg.Location = new System.Drawing.Point(256,128);
                  this.m_btnDelMsg.Name = "m_btnDelMsg";
                  this.m_btnDelMsg.Size = new System.Drawing.Size(75,23);
                  this.m_btnDelMsg.TabIndex = 2;
                  this.m_btnDelMsg.Text = "Delete";
                  this.m_btnDelMsg.Click += new System.EventHandler(this.DelMsg_Click);
                  // 
                  // m_btnEditMsg
                  // 
                  this.m_btnEditMsg.Location = new System.Drawing.Point(152,128);
                  this.m_btnEditMsg.Name = "m_btnEditMsg";
                  this.m_btnEditMsg.Size = new System.Drawing.Size(75,23);
                  this.m_btnEditMsg.TabIndex = 1;
                  this.m_btnEditMsg.Text = "Edit";
                  this.m_btnEditMsg.Click += new System.EventHandler(this.EditMsg_Click);
                  // 
                  // MsgForm
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                  this.ClientSize = new System.Drawing.Size(378,168);
                  this.Controls.Add(this.m_btnEditMsg);
                  this.Controls.Add(this.m_btnDelMsg);
                  this.Controls.Add(this.m_lstvwMsgs);
                  this.Controls.Add(this.m_btnAddMsg);
                  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                  this.Name = "MsgForm";
                  this.ShowInTaskbar = false;
                  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                  this.Closed += new System.EventHandler(this.MsgForm_Closed);
                  this.ResumeLayout(false);

		}
		#endregion

		public void UpdateMsgForm(DateTime oDate)
		{
			m_Date = oDate;
			this.Text = m_Date.ToLongDateString();
			PopulateListView();	
		}

		private void AddMsg_Click(object sender, EventArgs e)
		{
			m_addMsgForm.ClearForm(m_Date);

			DialogResult result = m_addMsgForm.ShowDialog(this);

			while ( result == DialogResult.Yes )
			{				
				if ( !m_addMsgForm.SetMessage(null) )
				{
					result = m_addMsgForm.ShowDialog(this);
				}
				else
				{
					MainForm.Commit();
					break;
				}
			}	
			PopulateListView();
		}

		private void EditMsg_Click(object sender, EventArgs e)
		{
			if ( m_lstvwMsgs.SelectedItems.Count == 0 ) return;

			int nIndex = m_lstvwMsgs.SelectedIndices[0];
			Message oMsg = (Message)m_lstvwMsgs.Items[nIndex];

			m_addMsgForm.PopulateForm(m_Date,oMsg);

			DialogResult result = m_addMsgForm.ShowDialog(this);

			while ( result == DialogResult.Yes )
			{				
				if ( !m_addMsgForm.SetMessage(oMsg) )
				{
					result = m_addMsgForm.ShowDialog(this);
				}
				else
				{
					MainForm.Commit();
					break;
				}
			}	
			PopulateListView();
			m_lstvwMsgs.Items[nIndex].Selected = false;
		}

		private void DelMsg_Click(object sender, EventArgs e)
		{
			if ( m_lstvwMsgs.SelectedItems.Count == 0 ) return;
			
			Message aMessage = (Message)m_lstvwMsgs.SelectedItems[0];

			ArrayList alMsgs = MainForm.MsgsForDay(m_Date.Day);
			alMsgs.Remove(aMessage);
			m_lstvwMsgs.Items.Remove(aMessage);

			if (alMsgs.Count == 0 )    // if no messages, remove key
				MainForm.Messages.Remove(m_Date.Day);	
		
			MainForm.Commit();
		}

		private void PopulateListView()
		{
			m_lstvwMsgs.Items.Clear();
			ArrayList alMsgs = MainForm.MsgsForDay(m_Date.Day);
			ArrayList msgsForDate = new ArrayList();

			if ( alMsgs == null ) return;

			for ( int i = 0; i < alMsgs.Count; i++ )
			{
				Message msg = (Message)alMsgs[i];
				switch ( msg.RecurFrequency )
				{
					case Message.Recurs.enNone :
						if ( msg.Date.CompareTo(m_Date) == 0 )
							msgsForDate.Add(msg);
						break;
					case Message.Recurs.enYearly :
						if ( msg.Date.Month == m_Date.Month )
							msgsForDate.Add(msg);
						break;
					case Message.Recurs.enMonthly :
						msg.Date = m_Date;		// update date to reflect date chosen
						msgsForDate.Add(msg);
						break;
					default :
						break;
				}
			}
			m_lstvwMsgs.Items.AddRange((Message[])(msgsForDate.ToArray(Type.GetType("CalPal.Message"))));
		}

		private void MsgForm_Closed(object sender, EventArgs args)
		{
			if ( m_addMsgForm != null )
			{
				m_addMsgForm.Dispose();
			}
		}
	}
}

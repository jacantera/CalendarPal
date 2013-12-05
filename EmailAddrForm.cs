using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CalPal
{
	/// <summary>
	/// Summary description for EmailAddrForm.
	/// </summary>
	public class EmailAddrForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label m_lblEmailAddress;
		private System.Windows.Forms.TextBox m_txtboxEmailAddress;
		private System.Windows.Forms.Button m_btnOK;

		private System.ComponentModel.Container components = null;
		
		public EmailAddrForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			RegistryKey appKey = Registry.CurrentUser.OpenSubKey(@"Software\CalendarPal");
			this.m_txtboxEmailAddress.Text = (String)appKey.GetValue("emailAddress","");
			appKey.Close();
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
                  this.m_lblEmailAddress = new System.Windows.Forms.Label();
                  this.m_txtboxEmailAddress = new System.Windows.Forms.TextBox();
                  this.m_btnOK = new System.Windows.Forms.Button();
                  this.SuspendLayout();
                  // 
                  // m_lblEmailAddress
                  // 
                  this.m_lblEmailAddress.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
                  this.m_lblEmailAddress.Location = new System.Drawing.Point(72,16);
                  this.m_lblEmailAddress.Name = "m_lblEmailAddress";
                  this.m_lblEmailAddress.Size = new System.Drawing.Size(88,16);
                  this.m_lblEmailAddress.TabIndex = 0;
                  this.m_lblEmailAddress.Text = "Email Address";
                  // 
                  // m_txtboxEmailAddress
                  // 
                  this.m_txtboxEmailAddress.Location = new System.Drawing.Point(24,40);
                  this.m_txtboxEmailAddress.Name = "m_txtboxEmailAddress";
                  this.m_txtboxEmailAddress.Size = new System.Drawing.Size(168,20);
                  this.m_txtboxEmailAddress.TabIndex = 2;
                  // 
                  // m_btnOK
                  // 
                  this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
                  this.m_btnOK.Location = new System.Drawing.Point(64,88);
                  this.m_btnOK.Name = "m_btnOK";
                  this.m_btnOK.Size = new System.Drawing.Size(75,23);
                  this.m_btnOK.TabIndex = 4;
                  this.m_btnOK.Text = "OK";
                  this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
                  // 
                  // EmailAddrForm
                  // 
                  this.AcceptButton = this.m_btnOK;
                  this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                  this.ClientSize = new System.Drawing.Size(210,136);
                  this.Controls.Add(this.m_btnOK);
                  this.Controls.Add(this.m_txtboxEmailAddress);
                  this.Controls.Add(this.m_lblEmailAddress);
                  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                  this.Name = "EmailAddrForm";
                  this.ShowInTaskbar = false;
                  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                  this.Text = "Email Setup";
                  this.ResumeLayout(false);
                  this.PerformLayout();

		}
		#endregion

		private void m_btnOK_Click(object sender, EventArgs e)
		{
			if ( !m_txtboxEmailAddress.Text.Equals(String.Empty) )
			{
				RegistryKey appKey = Registry.CurrentUser.OpenSubKey(@"Software\CalendarPal",true);
				appKey.SetValue("emailAddress",this.m_txtboxEmailAddress.Text);
				appKey.Close();	
			}
		}

		public bool VerifyEmailAddress()
		{
			if ( m_txtboxEmailAddress.Text.Equals(String.Empty) ) 
				return false;
			else
				return true;
		}
	}
}

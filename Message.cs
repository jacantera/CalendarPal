using System;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Globalization;

namespace CalPal
{
	/// <summary>
	/// Summary description for Message.
	/// </summary>
	/// 
	[Serializable]
	public class Message : ListViewItem, ISerializable
	{
		private Recurs m_enRecurFrequency;
		private int m_nLeadTime;
		private DateTime m_Date;

		public enum Recurs 
		{
			enNone = 0,
			enMonthly = 1,
			enYearly = 2
	};
		
		protected Message(SerializationInfo info, StreamingContext context)
		{
			this.Text = info.GetString("subj");
			this.SubItems.Add(info.GetString("msgtxt"));
			this.SubItems.Add(info.GetString("recur"));
			this.SubItems.Add(info.GetString("senddate"));
			m_enRecurFrequency = (Recurs)info.GetInt32("ri");
			m_nLeadTime = info.GetInt32("lti");
			m_Date = info.GetDateTime("msgdate");
		}

		public Message(String strSubj, String strMsgTxt, int nRecurIndex, String strRecur,
			int nLeadTimeIndex, DateTime date)
			: base(strSubj)
		{
			m_enRecurFrequency = (Recurs)nRecurIndex;
			m_nLeadTime = nLeadTimeIndex;
			m_Date = date;
			this.SubItems.Add(strMsgTxt);
			this.SubItems.Add(strRecur);
			this.SubItems.Add(this.SendDate.ToString("MMM d",DateTimeFormatInfo.InvariantInfo));
		}

		public String Subject
		{
			get { return this.Text; }
			set { this.Text = value; }
		}

		public String Body
		{
			get { return this.SubItems[1].Text; }
			set { this.SubItems[1].Text = value; }
		}

		public String strRecurFrequency
		{
			set { this.SubItems[2].Text = value; }
		}

		public Recurs RecurFrequency
		{
			get { return m_enRecurFrequency; }
			set { m_enRecurFrequency = value; }
		}

		public int LeadTime
		{
			get { return m_nLeadTime; }
			set { m_nLeadTime = value; }
		}

		public DateTime Date
		{
			get { return m_Date; }
			set 
			{ 
				m_Date = value; 
				this.SubItems[3].Text = SendDate.ToString("MMM d",DateTimeFormatInfo.InvariantInfo);
			}
		}

		public DateTime SendDate
		{
			get { return m_Date.AddDays(-m_nLeadTime); }
		}

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("subj",this.SubItems[0].Text);
			info.AddValue("msgtxt",this.SubItems[1].Text);
			info.AddValue("recur",this.SubItems[2].Text);
			info.AddValue("senddate",this.SubItems[3].Text);
			info.AddValue("ri",m_enRecurFrequency);
			info.AddValue("lti",m_nLeadTime);
			info.AddValue("msgdate",m_Date);
		}

		#endregion
	}
}

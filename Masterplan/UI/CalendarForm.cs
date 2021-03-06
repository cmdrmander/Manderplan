using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CalendarForm : Form
	{
		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label YearLbl;

		private NumericUpDown YearBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TabPage MonthsPage;

		private TabPage DaysPage;

		private TextBox DetailsBox;

		private ListView MonthList;

		private ToolStrip MonthToolbar;

		private ColumnHeader MonthHdr;

		private ColumnHeader DaysHdr;

		private ToolStripButton MonthAddBtn;

		private ToolStripButton MonthRemoveBtn;

		private ToolStripButton MonthEditBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton MonthUpBtn;

		private ToolStripButton MonthDownBtn;

		private ListView DayList;

		private ColumnHeader DayHdr;

		private ToolStrip DayToolbar;

		private ToolStripButton DayAddBtn;

		private ToolStripButton DayRemoveBtn;

		private ToolStripButton DayEditBtn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton DayUpBtn;

		private ToolStripButton DayDownBtn;

		private TabPage EventsPage;

		private ListView EventList;

		private ColumnHeader EventHdr;

		private ColumnHeader DateHdr;

		private ToolStrip EventToolbar;

		private ToolStripButton EventAddBtn;

		private ToolStripButton EventRemoveBtn;

		private ToolStripButton EventEditBtn;

		private TabPage SatellitePage;

		private ListView SatelliteList;

		private ColumnHeader SatelliteHdr;

		private ToolStrip SatelliteToolbar;

		private ToolStripButton SatelliteAddBtn;

		private ToolStripButton SatelliteRemoveBtn;

		private ToolStripButton SatelliteEditBtn;

		private TabPage SeasonsPage;

		private ListView SeasonList;

		private ColumnHeader SeasonHdr;

		private ColumnHeader SeasonDateHdr;

		private ToolStrip SeasonToolbar;

		private ToolStripButton SeasonAddBtn;

		private ToolStripButton SeasonRemoveBtn;

		private ToolStripButton SeasonEditBtn;

		private Masterplan.Data.Calendar fCalendar;

		public Masterplan.Data.Calendar Calendar
		{
			get
			{
				return this.fCalendar;
			}
		}

		public DayInfo SelectedDay
		{
			get
			{
				if (this.DayList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.DayList.SelectedItems[0].Tag as DayInfo;
			}
		}

		public CalendarEvent SelectedEvent
		{
			get
			{
				if (this.EventList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.EventList.SelectedItems[0].Tag as CalendarEvent;
			}
		}

		public MonthInfo SelectedMonth
		{
			get
			{
				if (this.MonthList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.MonthList.SelectedItems[0].Tag as MonthInfo;
			}
		}

		public Satellite SelectedSatellite
		{
			get
			{
				if (this.SatelliteList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SatelliteList.SelectedItems[0].Tag as Satellite;
			}
		}

		public CalendarEvent SelectedSeason
		{
			get
			{
				if (this.SeasonList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.SeasonList.SelectedItems[0].Tag as CalendarEvent;
			}
		}

		public CalendarForm(Masterplan.Data.Calendar calendar)
		{
			this.InitializeComponent();
			this.fCalendar = calendar.Copy();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.EventList.ListViewItemSorter = new CalendarForm.EventSorter(this.fCalendar);
			this.SeasonList.ListViewItemSorter = new CalendarForm.EventSorter(this.fCalendar);
			this.NameBox.Text = this.fCalendar.Name;
			this.YearBox.Value = this.fCalendar.CampaignYear;
			this.DetailsBox.Text = this.fCalendar.Details;
			this.update_months();
			this.update_days();
			this.update_seasons();
			this.update_events();
			this.update_satellites();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.MonthRemoveBtn.Enabled = this.SelectedMonth != null;
			this.MonthEditBtn.Enabled = this.SelectedMonth != null;
			this.MonthUpBtn.Enabled = (this.SelectedMonth == null ? false : this.fCalendar.Months.IndexOf(this.SelectedMonth) != 0);
			this.MonthDownBtn.Enabled = (this.SelectedMonth == null ? false : this.fCalendar.Months.IndexOf(this.SelectedMonth) != this.fCalendar.Months.Count - 1);
			this.DayRemoveBtn.Enabled = this.SelectedDay != null;
			this.DayEditBtn.Enabled = this.SelectedDay != null;
			this.DayUpBtn.Enabled = (this.SelectedDay == null ? false : this.fCalendar.Days.IndexOf(this.SelectedDay) != 0);
			this.DayDownBtn.Enabled = (this.SelectedDay == null ? false : this.fCalendar.Days.IndexOf(this.SelectedDay) != this.fCalendar.Days.Count - 1);
			this.SeasonAddBtn.Enabled = this.fCalendar.Months.Count != 0;
			this.SeasonRemoveBtn.Enabled = this.SelectedSeason != null;
			this.SeasonEditBtn.Enabled = this.SelectedSeason != null;
			this.EventAddBtn.Enabled = this.fCalendar.Months.Count != 0;
			this.EventRemoveBtn.Enabled = this.SelectedEvent != null;
			this.EventEditBtn.Enabled = this.SelectedEvent != null;
			this.SatelliteRemoveBtn.Enabled = this.SelectedSatellite != null;
			this.SatelliteEditBtn.Enabled = this.SelectedSatellite != null;
			this.OKBtn.Enabled = (this.fCalendar.Months.Count == 0 ? false : this.fCalendar.Days.Count != 0);
		}

		private void DayAddBtn_Click(object sender, EventArgs e)
		{
			DayForm dayForm = new DayForm(new DayInfo()
			{
				Name = "New Day"
			});
			if (dayForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fCalendar.Days.Add(dayForm.DayInfo);
				this.update_days();
			}
		}

		private void DayDownBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDay != null)
			{
				int selectedDay = this.fCalendar.Days.IndexOf(this.SelectedDay);
				if (selectedDay == this.fCalendar.Days.Count - 1)
				{
					return;
				}
				DayInfo item = this.fCalendar.Days[selectedDay + 1];
				this.fCalendar.Days[selectedDay + 1] = this.SelectedDay;
				this.fCalendar.Days[selectedDay] = item;
				this.update_days();
				this.DayList.Items[selectedDay + 1].Selected = true;
			}
		}

		private void DayEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDay != null)
			{
				int dayInfo = this.fCalendar.Days.IndexOf(this.SelectedDay);
				DayForm dayForm = new DayForm(this.SelectedDay);
				if (dayForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fCalendar.Days[dayInfo] = dayForm.DayInfo;
					this.update_days();
				}
			}
		}

		private void DayRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDay != null)
			{
				this.fCalendar.Days.Remove(this.SelectedDay);
				this.update_days();
			}
		}

		private void DayUpBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDay != null)
			{
				int selectedDay = this.fCalendar.Days.IndexOf(this.SelectedDay);
				if (selectedDay == 0)
				{
					return;
				}
				DayInfo item = this.fCalendar.Days[selectedDay - 1];
				this.fCalendar.Days[selectedDay - 1] = this.SelectedDay;
				this.fCalendar.Days[selectedDay] = item;
				this.update_days();
				this.DayList.Items[selectedDay - 1].Selected = true;
			}
		}

		private void EventAddBtn_Click(object sender, EventArgs e)
		{
			CalendarEvent calendarEvent = new CalendarEvent()
			{
				Name = "New Event",
				MonthID = this.fCalendar.Months[0].ID
			};
			CalendarEventForm calendarEventForm = new CalendarEventForm(calendarEvent, this.fCalendar);
			if (calendarEventForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fCalendar.Events.Add(calendarEventForm.Event);
				this.update_events();
			}
		}

		private void EventEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEvent != null)
			{
				int @event = this.fCalendar.Events.IndexOf(this.SelectedEvent);
				CalendarEventForm calendarEventForm = new CalendarEventForm(this.SelectedEvent, this.fCalendar);
				if (calendarEventForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fCalendar.Events[@event] = calendarEventForm.Event;
					this.update_events();
				}
			}
		}

		private void EventRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEvent != null)
			{
				this.fCalendar.Events.Remove(this.SelectedEvent);
				this.update_events();
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CalendarForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.YearLbl = new Label();
			this.YearBox = new NumericUpDown();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.MonthsPage = new TabPage();
			this.MonthList = new ListView();
			this.MonthHdr = new ColumnHeader();
			this.DaysHdr = new ColumnHeader();
			this.MonthToolbar = new ToolStrip();
			this.MonthAddBtn = new ToolStripButton();
			this.MonthRemoveBtn = new ToolStripButton();
			this.MonthEditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.MonthUpBtn = new ToolStripButton();
			this.MonthDownBtn = new ToolStripButton();
			this.DaysPage = new TabPage();
			this.DayList = new ListView();
			this.DayHdr = new ColumnHeader();
			this.DayToolbar = new ToolStrip();
			this.DayAddBtn = new ToolStripButton();
			this.DayRemoveBtn = new ToolStripButton();
			this.DayEditBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.DayUpBtn = new ToolStripButton();
			this.DayDownBtn = new ToolStripButton();
			this.SeasonsPage = new TabPage();
			this.SeasonList = new ListView();
			this.SeasonHdr = new ColumnHeader();
			this.SeasonDateHdr = new ColumnHeader();
			this.SeasonToolbar = new ToolStrip();
			this.SeasonAddBtn = new ToolStripButton();
			this.SeasonRemoveBtn = new ToolStripButton();
			this.SeasonEditBtn = new ToolStripButton();
			this.EventsPage = new TabPage();
			this.EventList = new ListView();
			this.EventHdr = new ColumnHeader();
			this.DateHdr = new ColumnHeader();
			this.EventToolbar = new ToolStrip();
			this.EventAddBtn = new ToolStripButton();
			this.EventRemoveBtn = new ToolStripButton();
			this.EventEditBtn = new ToolStripButton();
			this.SatellitePage = new TabPage();
			this.SatelliteList = new ListView();
			this.SatelliteHdr = new ColumnHeader();
			this.SatelliteToolbar = new ToolStrip();
			this.SatelliteAddBtn = new ToolStripButton();
			this.SatelliteRemoveBtn = new ToolStripButton();
			this.SatelliteEditBtn = new ToolStripButton();
			((ISupportInitialize)this.YearBox).BeginInit();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.MonthsPage.SuspendLayout();
			this.MonthToolbar.SuspendLayout();
			this.DaysPage.SuspendLayout();
			this.DayToolbar.SuspendLayout();
			this.SeasonsPage.SuspendLayout();
			this.SeasonToolbar.SuspendLayout();
			this.EventsPage.SuspendLayout();
			this.EventToolbar.SuspendLayout();
			this.SatellitePage.SuspendLayout();
			this.SatelliteToolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(179, 365);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(260, 365);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 2;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(100, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(235, 20);
			this.NameBox.TabIndex = 3;
			this.YearLbl.AutoSize = true;
			this.YearLbl.Location = new Point(12, 40);
			this.YearLbl.Name = "YearLbl";
			this.YearLbl.Size = new System.Drawing.Size(82, 13);
			this.YearLbl.TabIndex = 4;
			this.YearLbl.Text = "Campaign Year:";
			this.YearBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.YearBox.Location = new Point(100, 38);
			NumericUpDown yearBox = this.YearBox;
			int[] numArray = new int[] { 10000, 0, 0, 0 };
			yearBox.Maximum = new decimal(numArray);
			NumericUpDown num = this.YearBox;
			int[] numArray1 = new int[] { 10000, 0, 0, -2147483648 };
			num.Minimum = new decimal(numArray1);
			this.YearBox.Name = "YearBox";
			this.YearBox.Size = new System.Drawing.Size(235, 20);
			this.YearBox.TabIndex = 5;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.MonthsPage);
			this.Pages.Controls.Add(this.DaysPage);
			this.Pages.Controls.Add(this.SeasonsPage);
			this.Pages.Controls.Add(this.EventsPage);
			this.Pages.Controls.Add(this.SatellitePage);
			this.Pages.Location = new Point(12, 64);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(323, 295);
			this.Pages.TabIndex = 6;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(315, 269);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new System.Drawing.Size(309, 263);
			this.DetailsBox.TabIndex = 0;
			this.MonthsPage.Controls.Add(this.MonthList);
			this.MonthsPage.Controls.Add(this.MonthToolbar);
			this.MonthsPage.Location = new Point(4, 22);
			this.MonthsPage.Name = "MonthsPage";
			this.MonthsPage.Padding = new System.Windows.Forms.Padding(3);
			this.MonthsPage.Size = new System.Drawing.Size(315, 269);
			this.MonthsPage.TabIndex = 1;
			this.MonthsPage.Text = "Months";
			this.MonthsPage.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columns = this.MonthList.Columns;
			ColumnHeader[] monthHdr = new ColumnHeader[] { this.MonthHdr, this.DaysHdr };
			columns.AddRange(monthHdr);
			this.MonthList.Dock = DockStyle.Fill;
			this.MonthList.FullRowSelect = true;
			this.MonthList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MonthList.HideSelection = false;
			this.MonthList.Location = new Point(3, 28);
			this.MonthList.MultiSelect = false;
			this.MonthList.Name = "MonthList";
			this.MonthList.Size = new System.Drawing.Size(309, 238);
			this.MonthList.TabIndex = 1;
			this.MonthList.UseCompatibleStateImageBehavior = false;
			this.MonthList.View = View.Details;
			this.MonthList.DoubleClick += new EventHandler(this.MonthEditBtn_Click);
			this.MonthHdr.Text = "Month";
			this.MonthHdr.Width = 150;
			this.DaysHdr.Text = "Days";
			this.DaysHdr.TextAlign = HorizontalAlignment.Right;
			ToolStripItemCollection items = this.MonthToolbar.Items;
			ToolStripItem[] monthAddBtn = new ToolStripItem[] { this.MonthAddBtn, this.MonthRemoveBtn, this.MonthEditBtn, this.toolStripSeparator1, this.MonthUpBtn, this.MonthDownBtn };
			items.AddRange(monthAddBtn);
			this.MonthToolbar.Location = new Point(3, 3);
			this.MonthToolbar.Name = "MonthToolbar";
			this.MonthToolbar.Size = new System.Drawing.Size(309, 25);
			this.MonthToolbar.TabIndex = 0;
			this.MonthToolbar.Text = "toolStrip1";
			this.MonthAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MonthAddBtn.Image = (Image)componentResourceManager.GetObject("MonthAddBtn.Image");
			this.MonthAddBtn.ImageTransparentColor = Color.Magenta;
			this.MonthAddBtn.Name = "MonthAddBtn";
			this.MonthAddBtn.Size = new System.Drawing.Size(33, 22);
			this.MonthAddBtn.Text = "Add";
			this.MonthAddBtn.Click += new EventHandler(this.MonthAddBtn_Click);
			this.MonthRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MonthRemoveBtn.Image = (Image)componentResourceManager.GetObject("MonthRemoveBtn.Image");
			this.MonthRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.MonthRemoveBtn.Name = "MonthRemoveBtn";
			this.MonthRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.MonthRemoveBtn.Text = "Remove";
			this.MonthRemoveBtn.Click += new EventHandler(this.MonthRemoveBtn_Click);
			this.MonthEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MonthEditBtn.Image = (Image)componentResourceManager.GetObject("MonthEditBtn.Image");
			this.MonthEditBtn.ImageTransparentColor = Color.Magenta;
			this.MonthEditBtn.Name = "MonthEditBtn";
			this.MonthEditBtn.Size = new System.Drawing.Size(31, 22);
			this.MonthEditBtn.Text = "Edit";
			this.MonthEditBtn.Click += new EventHandler(this.MonthEditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.MonthUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MonthUpBtn.Image = (Image)componentResourceManager.GetObject("MonthUpBtn.Image");
			this.MonthUpBtn.ImageTransparentColor = Color.Magenta;
			this.MonthUpBtn.Name = "MonthUpBtn";
			this.MonthUpBtn.Size = new System.Drawing.Size(59, 22);
			this.MonthUpBtn.Text = "Move Up";
			this.MonthUpBtn.Click += new EventHandler(this.MonthUpBtn_Click);
			this.MonthDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MonthDownBtn.Image = (Image)componentResourceManager.GetObject("MonthDownBtn.Image");
			this.MonthDownBtn.ImageTransparentColor = Color.Magenta;
			this.MonthDownBtn.Name = "MonthDownBtn";
			this.MonthDownBtn.Size = new System.Drawing.Size(75, 22);
			this.MonthDownBtn.Text = "Move Down";
			this.MonthDownBtn.Click += new EventHandler(this.MonthDownBtn_Click);
			this.DaysPage.Controls.Add(this.DayList);
			this.DaysPage.Controls.Add(this.DayToolbar);
			this.DaysPage.Location = new Point(4, 22);
			this.DaysPage.Name = "DaysPage";
			this.DaysPage.Padding = new System.Windows.Forms.Padding(3);
			this.DaysPage.Size = new System.Drawing.Size(315, 269);
			this.DaysPage.TabIndex = 2;
			this.DaysPage.Text = "Days";
			this.DaysPage.UseVisualStyleBackColor = true;
			this.DayList.Columns.AddRange(new ColumnHeader[] { this.DayHdr });
			this.DayList.Dock = DockStyle.Fill;
			this.DayList.FullRowSelect = true;
			this.DayList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DayList.HideSelection = false;
			this.DayList.Location = new Point(3, 28);
			this.DayList.MultiSelect = false;
			this.DayList.Name = "DayList";
			this.DayList.Size = new System.Drawing.Size(309, 238);
			this.DayList.TabIndex = 3;
			this.DayList.UseCompatibleStateImageBehavior = false;
			this.DayList.View = View.Details;
			this.DayList.DoubleClick += new EventHandler(this.DayEditBtn_Click);
			this.DayHdr.Text = "Day";
			this.DayHdr.Width = 150;
			ToolStripItemCollection toolStripItemCollections = this.DayToolbar.Items;
			ToolStripItem[] dayAddBtn = new ToolStripItem[] { this.DayAddBtn, this.DayRemoveBtn, this.DayEditBtn, this.toolStripSeparator2, this.DayUpBtn, this.DayDownBtn };
			toolStripItemCollections.AddRange(dayAddBtn);
			this.DayToolbar.Location = new Point(3, 3);
			this.DayToolbar.Name = "DayToolbar";
			this.DayToolbar.Size = new System.Drawing.Size(309, 25);
			this.DayToolbar.TabIndex = 2;
			this.DayToolbar.Text = "toolStrip2";
			this.DayAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DayAddBtn.Image = (Image)componentResourceManager.GetObject("DayAddBtn.Image");
			this.DayAddBtn.ImageTransparentColor = Color.Magenta;
			this.DayAddBtn.Name = "DayAddBtn";
			this.DayAddBtn.Size = new System.Drawing.Size(33, 22);
			this.DayAddBtn.Text = "Add";
			this.DayAddBtn.Click += new EventHandler(this.DayAddBtn_Click);
			this.DayRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DayRemoveBtn.Image = (Image)componentResourceManager.GetObject("DayRemoveBtn.Image");
			this.DayRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.DayRemoveBtn.Name = "DayRemoveBtn";
			this.DayRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.DayRemoveBtn.Text = "Remove";
			this.DayRemoveBtn.Click += new EventHandler(this.DayRemoveBtn_Click);
			this.DayEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DayEditBtn.Image = (Image)componentResourceManager.GetObject("DayEditBtn.Image");
			this.DayEditBtn.ImageTransparentColor = Color.Magenta;
			this.DayEditBtn.Name = "DayEditBtn";
			this.DayEditBtn.Size = new System.Drawing.Size(31, 22);
			this.DayEditBtn.Text = "Edit";
			this.DayEditBtn.Click += new EventHandler(this.DayEditBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.DayUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DayUpBtn.Image = (Image)componentResourceManager.GetObject("DayUpBtn.Image");
			this.DayUpBtn.ImageTransparentColor = Color.Magenta;
			this.DayUpBtn.Name = "DayUpBtn";
			this.DayUpBtn.Size = new System.Drawing.Size(59, 22);
			this.DayUpBtn.Text = "Move Up";
			this.DayUpBtn.Click += new EventHandler(this.DayUpBtn_Click);
			this.DayDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DayDownBtn.Image = (Image)componentResourceManager.GetObject("DayDownBtn.Image");
			this.DayDownBtn.ImageTransparentColor = Color.Magenta;
			this.DayDownBtn.Name = "DayDownBtn";
			this.DayDownBtn.Size = new System.Drawing.Size(75, 22);
			this.DayDownBtn.Text = "Move Down";
			this.DayDownBtn.Click += new EventHandler(this.DayDownBtn_Click);
			this.SeasonsPage.Controls.Add(this.SeasonList);
			this.SeasonsPage.Controls.Add(this.SeasonToolbar);
			this.SeasonsPage.Location = new Point(4, 22);
			this.SeasonsPage.Name = "SeasonsPage";
			this.SeasonsPage.Padding = new System.Windows.Forms.Padding(3);
			this.SeasonsPage.Size = new System.Drawing.Size(315, 269);
			this.SeasonsPage.TabIndex = 5;
			this.SeasonsPage.Text = "Seasons";
			this.SeasonsPage.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columnHeaderCollections = this.SeasonList.Columns;
			ColumnHeader[] seasonHdr = new ColumnHeader[] { this.SeasonHdr, this.SeasonDateHdr };
			columnHeaderCollections.AddRange(seasonHdr);
			this.SeasonList.Dock = DockStyle.Fill;
			this.SeasonList.FullRowSelect = true;
			this.SeasonList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SeasonList.HideSelection = false;
			this.SeasonList.Location = new Point(3, 28);
			this.SeasonList.MultiSelect = false;
			this.SeasonList.Name = "SeasonList";
			this.SeasonList.Size = new System.Drawing.Size(309, 238);
			this.SeasonList.TabIndex = 7;
			this.SeasonList.UseCompatibleStateImageBehavior = false;
			this.SeasonList.View = View.Details;
			this.SeasonList.DoubleClick += new EventHandler(this.SeasonEditBtn_Click);
			this.SeasonHdr.Text = "Season";
			this.SeasonHdr.Width = 150;
			this.SeasonDateHdr.Text = "Date";
			this.SeasonDateHdr.Width = 120;
			ToolStripItemCollection items1 = this.SeasonToolbar.Items;
			ToolStripItem[] seasonAddBtn = new ToolStripItem[] { this.SeasonAddBtn, this.SeasonRemoveBtn, this.SeasonEditBtn };
			items1.AddRange(seasonAddBtn);
			this.SeasonToolbar.Location = new Point(3, 3);
			this.SeasonToolbar.Name = "SeasonToolbar";
			this.SeasonToolbar.Size = new System.Drawing.Size(309, 25);
			this.SeasonToolbar.TabIndex = 6;
			this.SeasonToolbar.Text = "toolStrip2";
			this.SeasonAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SeasonAddBtn.Image = (Image)componentResourceManager.GetObject("SeasonAddBtn.Image");
			this.SeasonAddBtn.ImageTransparentColor = Color.Magenta;
			this.SeasonAddBtn.Name = "SeasonAddBtn";
			this.SeasonAddBtn.Size = new System.Drawing.Size(33, 22);
			this.SeasonAddBtn.Text = "Add";
			this.SeasonAddBtn.Click += new EventHandler(this.SeasonAddBtn_Click);
			this.SeasonRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SeasonRemoveBtn.Image = (Image)componentResourceManager.GetObject("SeasonRemoveBtn.Image");
			this.SeasonRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.SeasonRemoveBtn.Name = "SeasonRemoveBtn";
			this.SeasonRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.SeasonRemoveBtn.Text = "Remove";
			this.SeasonRemoveBtn.Click += new EventHandler(this.SeasonRemoveBtn_Click);
			this.SeasonEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SeasonEditBtn.Image = (Image)componentResourceManager.GetObject("SeasonEditBtn.Image");
			this.SeasonEditBtn.ImageTransparentColor = Color.Magenta;
			this.SeasonEditBtn.Name = "SeasonEditBtn";
			this.SeasonEditBtn.Size = new System.Drawing.Size(31, 22);
			this.SeasonEditBtn.Text = "Edit";
			this.SeasonEditBtn.Click += new EventHandler(this.SeasonEditBtn_Click);
			this.EventsPage.Controls.Add(this.EventList);
			this.EventsPage.Controls.Add(this.EventToolbar);
			this.EventsPage.Location = new Point(4, 22);
			this.EventsPage.Name = "EventsPage";
			this.EventsPage.Padding = new System.Windows.Forms.Padding(3);
			this.EventsPage.Size = new System.Drawing.Size(315, 269);
			this.EventsPage.TabIndex = 3;
			this.EventsPage.Text = "Events";
			this.EventsPage.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columns1 = this.EventList.Columns;
			ColumnHeader[] eventHdr = new ColumnHeader[] { this.EventHdr, this.DateHdr };
			columns1.AddRange(eventHdr);
			this.EventList.Dock = DockStyle.Fill;
			this.EventList.FullRowSelect = true;
			this.EventList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EventList.HideSelection = false;
			this.EventList.Location = new Point(3, 28);
			this.EventList.MultiSelect = false;
			this.EventList.Name = "EventList";
			this.EventList.Size = new System.Drawing.Size(309, 238);
			this.EventList.TabIndex = 5;
			this.EventList.UseCompatibleStateImageBehavior = false;
			this.EventList.View = View.Details;
			this.EventList.DoubleClick += new EventHandler(this.EventEditBtn_Click);
			this.EventHdr.Text = "Event";
			this.EventHdr.Width = 150;
			this.DateHdr.Text = "Date";
			this.DateHdr.Width = 120;
			ToolStripItemCollection toolStripItemCollections1 = this.EventToolbar.Items;
			ToolStripItem[] eventAddBtn = new ToolStripItem[] { this.EventAddBtn, this.EventRemoveBtn, this.EventEditBtn };
			toolStripItemCollections1.AddRange(eventAddBtn);
			this.EventToolbar.Location = new Point(3, 3);
			this.EventToolbar.Name = "EventToolbar";
			this.EventToolbar.Size = new System.Drawing.Size(309, 25);
			this.EventToolbar.TabIndex = 4;
			this.EventToolbar.Text = "toolStrip2";
			this.EventAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EventAddBtn.Image = (Image)componentResourceManager.GetObject("EventAddBtn.Image");
			this.EventAddBtn.ImageTransparentColor = Color.Magenta;
			this.EventAddBtn.Name = "EventAddBtn";
			this.EventAddBtn.Size = new System.Drawing.Size(33, 22);
			this.EventAddBtn.Text = "Add";
			this.EventAddBtn.Click += new EventHandler(this.EventAddBtn_Click);
			this.EventRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EventRemoveBtn.Image = (Image)componentResourceManager.GetObject("EventRemoveBtn.Image");
			this.EventRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.EventRemoveBtn.Name = "EventRemoveBtn";
			this.EventRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.EventRemoveBtn.Text = "Remove";
			this.EventRemoveBtn.Click += new EventHandler(this.EventRemoveBtn_Click);
			this.EventEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EventEditBtn.Image = (Image)componentResourceManager.GetObject("EventEditBtn.Image");
			this.EventEditBtn.ImageTransparentColor = Color.Magenta;
			this.EventEditBtn.Name = "EventEditBtn";
			this.EventEditBtn.Size = new System.Drawing.Size(31, 22);
			this.EventEditBtn.Text = "Edit";
			this.EventEditBtn.Click += new EventHandler(this.EventEditBtn_Click);
			this.SatellitePage.Controls.Add(this.SatelliteList);
			this.SatellitePage.Controls.Add(this.SatelliteToolbar);
			this.SatellitePage.Location = new Point(4, 22);
			this.SatellitePage.Name = "SatellitePage";
			this.SatellitePage.Padding = new System.Windows.Forms.Padding(3);
			this.SatellitePage.Size = new System.Drawing.Size(315, 269);
			this.SatellitePage.TabIndex = 4;
			this.SatellitePage.Text = "Satellites";
			this.SatellitePage.UseVisualStyleBackColor = true;
			this.SatelliteList.Columns.AddRange(new ColumnHeader[] { this.SatelliteHdr });
			this.SatelliteList.Dock = DockStyle.Fill;
			this.SatelliteList.FullRowSelect = true;
			this.SatelliteList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SatelliteList.HideSelection = false;
			this.SatelliteList.Location = new Point(3, 28);
			this.SatelliteList.MultiSelect = false;
			this.SatelliteList.Name = "SatelliteList";
			this.SatelliteList.Size = new System.Drawing.Size(309, 238);
			this.SatelliteList.TabIndex = 7;
			this.SatelliteList.UseCompatibleStateImageBehavior = false;
			this.SatelliteList.View = View.Details;
			this.SatelliteList.DoubleClick += new EventHandler(this.SatelliteEditBtn_Click);
			this.SatelliteHdr.Text = "Satellite";
			this.SatelliteHdr.Width = 150;
			ToolStripItemCollection items2 = this.SatelliteToolbar.Items;
			ToolStripItem[] satelliteAddBtn = new ToolStripItem[] { this.SatelliteAddBtn, this.SatelliteRemoveBtn, this.SatelliteEditBtn };
			items2.AddRange(satelliteAddBtn);
			this.SatelliteToolbar.Location = new Point(3, 3);
			this.SatelliteToolbar.Name = "SatelliteToolbar";
			this.SatelliteToolbar.Size = new System.Drawing.Size(309, 25);
			this.SatelliteToolbar.TabIndex = 6;
			this.SatelliteToolbar.Text = "toolStrip2";
			this.SatelliteAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SatelliteAddBtn.Image = (Image)componentResourceManager.GetObject("SatelliteAddBtn.Image");
			this.SatelliteAddBtn.ImageTransparentColor = Color.Magenta;
			this.SatelliteAddBtn.Name = "SatelliteAddBtn";
			this.SatelliteAddBtn.Size = new System.Drawing.Size(33, 22);
			this.SatelliteAddBtn.Text = "Add";
			this.SatelliteAddBtn.Click += new EventHandler(this.SatelliteAddBtn_Click);
			this.SatelliteRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SatelliteRemoveBtn.Image = (Image)componentResourceManager.GetObject("SatelliteRemoveBtn.Image");
			this.SatelliteRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.SatelliteRemoveBtn.Name = "SatelliteRemoveBtn";
			this.SatelliteRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.SatelliteRemoveBtn.Text = "Remove";
			this.SatelliteRemoveBtn.Click += new EventHandler(this.SatelliteRemoveBtn_Click);
			this.SatelliteEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SatelliteEditBtn.Image = (Image)componentResourceManager.GetObject("SatelliteEditBtn.Image");
			this.SatelliteEditBtn.ImageTransparentColor = Color.Magenta;
			this.SatelliteEditBtn.Name = "SatelliteEditBtn";
			this.SatelliteEditBtn.Size = new System.Drawing.Size(31, 22);
			this.SatelliteEditBtn.Text = "Edit";
			this.SatelliteEditBtn.Click += new EventHandler(this.SatelliteEditBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(347, 400);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.YearBox);
			base.Controls.Add(this.YearLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CalendarForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Calendar";
			((ISupportInitialize)this.YearBox).EndInit();
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.MonthsPage.ResumeLayout(false);
			this.MonthsPage.PerformLayout();
			this.MonthToolbar.ResumeLayout(false);
			this.MonthToolbar.PerformLayout();
			this.DaysPage.ResumeLayout(false);
			this.DaysPage.PerformLayout();
			this.DayToolbar.ResumeLayout(false);
			this.DayToolbar.PerformLayout();
			this.SeasonsPage.ResumeLayout(false);
			this.SeasonsPage.PerformLayout();
			this.SeasonToolbar.ResumeLayout(false);
			this.SeasonToolbar.PerformLayout();
			this.EventsPage.ResumeLayout(false);
			this.EventsPage.PerformLayout();
			this.EventToolbar.ResumeLayout(false);
			this.EventToolbar.PerformLayout();
			this.SatellitePage.ResumeLayout(false);
			this.SatellitePage.PerformLayout();
			this.SatelliteToolbar.ResumeLayout(false);
			this.SatelliteToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void MonthAddBtn_Click(object sender, EventArgs e)
		{
			MonthForm monthForm = new MonthForm(new MonthInfo()
			{
				Name = "New Month"
			});
			if (monthForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fCalendar.Months.Add(monthForm.MonthInfo);
				this.update_months();
				this.update_seasons();
				this.update_events();
			}
		}

		private void MonthDownBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMonth != null)
			{
				int selectedMonth = this.fCalendar.Months.IndexOf(this.SelectedMonth);
				if (selectedMonth == this.fCalendar.Months.Count - 1)
				{
					return;
				}
				MonthInfo item = this.fCalendar.Months[selectedMonth + 1];
				this.fCalendar.Months[selectedMonth + 1] = this.SelectedMonth;
				this.fCalendar.Months[selectedMonth] = item;
				this.update_months();
				this.update_seasons();
				this.update_events();
				this.MonthList.Items[selectedMonth + 1].Selected = true;
			}
		}

		private void MonthEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMonth != null)
			{
				int monthInfo = this.fCalendar.Months.IndexOf(this.SelectedMonth);
				MonthForm monthForm = new MonthForm(this.SelectedMonth);
				if (monthForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fCalendar.Months[monthInfo] = monthForm.MonthInfo;
					this.update_months();
					this.update_seasons();
					this.update_events();
				}
			}
		}

		private void MonthRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMonth != null)
			{
				this.fCalendar.Months.Remove(this.SelectedMonth);
				this.update_months();
				this.update_seasons();
				this.update_events();
			}
		}

		private void MonthUpBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMonth != null)
			{
				int selectedMonth = this.fCalendar.Months.IndexOf(this.SelectedMonth);
				if (selectedMonth == 0)
				{
					return;
				}
				MonthInfo item = this.fCalendar.Months[selectedMonth - 1];
				this.fCalendar.Months[selectedMonth - 1] = this.SelectedMonth;
				this.fCalendar.Months[selectedMonth] = item;
				this.update_months();
				this.update_seasons();
				this.update_events();
				this.MonthList.Items[selectedMonth - 1].Selected = true;
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fCalendar.Name = this.NameBox.Text;
			this.fCalendar.CampaignYear = (int)this.YearBox.Value;
			this.fCalendar.Details = this.DetailsBox.Text;
		}

		private void SatelliteAddBtn_Click(object sender, EventArgs e)
		{
			SatelliteForm satelliteForm = new SatelliteForm(new Satellite()
			{
				Name = "New Satellite"
			});
			if (satelliteForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fCalendar.Satellites.Add(satelliteForm.Satellite);
				this.update_satellites();
			}
		}

		private void SatelliteEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSatellite != null)
			{
				int satellite = this.fCalendar.Satellites.IndexOf(this.SelectedSatellite);
				SatelliteForm satelliteForm = new SatelliteForm(this.SelectedSatellite);
				if (satelliteForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fCalendar.Satellites[satellite] = satelliteForm.Satellite;
					this.update_satellites();
				}
			}
		}

		private void SatelliteRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSatellite != null)
			{
				this.fCalendar.Satellites.Remove(this.SelectedSatellite);
				this.update_satellites();
			}
		}

		private void SeasonAddBtn_Click(object sender, EventArgs e)
		{
			CalendarEvent calendarEvent = new CalendarEvent()
			{
				Name = "New Season",
				MonthID = this.fCalendar.Months[0].ID
			};
			SeasonForm seasonForm = new SeasonForm(calendarEvent, this.fCalendar);
			if (seasonForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fCalendar.Seasons.Add(seasonForm.Season);
				this.update_seasons();
			}
		}

		private void SeasonEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSeason != null)
			{
				int season = this.fCalendar.Seasons.IndexOf(this.SelectedSeason);
				SeasonForm seasonForm = new SeasonForm(this.SelectedSeason, this.fCalendar);
				if (seasonForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fCalendar.Seasons[season] = seasonForm.Season;
					this.update_seasons();
				}
			}
		}

		private void SeasonRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSeason != null)
			{
				this.fCalendar.Seasons.Remove(this.SelectedSeason);
				this.update_seasons();
			}
		}

		private void update_days()
		{
			this.DayList.Items.Clear();
			foreach (DayInfo day in this.fCalendar.Days)
			{
				ListViewItem listViewItem = this.DayList.Items.Add(day.Name);
				listViewItem.Tag = day;
			}
			if (this.DayList.Items.Count == 0)
			{
				ListViewItem grayText = this.DayList.Items.Add("(no days)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_events()
		{
			this.EventList.Items.Clear();
			foreach (CalendarEvent @event in this.fCalendar.Events)
			{
				MonthInfo monthInfo = this.fCalendar.FindMonth(@event.MonthID);
				int dayIndex = @event.DayIndex + 1;
				ListViewItem listViewItem = this.EventList.Items.Add(@event.Name);
				listViewItem.SubItems.Add(string.Concat(monthInfo.Name, " ", dayIndex));
				listViewItem.Tag = @event;
			}
			if (this.EventList.Items.Count == 0)
			{
				ListViewItem grayText = this.EventList.Items.Add("(no events)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.EventList.Sort();
		}

		private void update_months()
		{
			this.MonthList.Items.Clear();
			foreach (MonthInfo month in this.fCalendar.Months)
			{
				string str = month.DayCount.ToString();
				if (month.LeapModifier != 0 && month.LeapPeriod != 0)
				{
					int dayCount = month.DayCount + month.LeapModifier;
					str = string.Concat(str, " / ", dayCount.ToString());
				}
				ListViewItem listViewItem = this.MonthList.Items.Add(month.Name);
				listViewItem.SubItems.Add(str);
				listViewItem.Tag = month;
			}
			if (this.MonthList.Items.Count == 0)
			{
				ListViewItem grayText = this.MonthList.Items.Add("(no months)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_satellites()
		{
			this.SatelliteList.Items.Clear();
			foreach (Satellite satellite in this.fCalendar.Satellites)
			{
				ListViewItem listViewItem = this.SatelliteList.Items.Add(satellite.Name);
				listViewItem.Tag = satellite;
			}
			if (this.SatelliteList.Items.Count == 0)
			{
				ListViewItem grayText = this.SatelliteList.Items.Add("(no satellites)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_seasons()
		{
			this.SeasonList.Items.Clear();
			foreach (CalendarEvent season in this.fCalendar.Seasons)
			{
				MonthInfo monthInfo = this.fCalendar.FindMonth(season.MonthID);
				int dayIndex = season.DayIndex + 1;
				ListViewItem listViewItem = this.SeasonList.Items.Add(season.Name);
				listViewItem.SubItems.Add(string.Concat(monthInfo.Name, " ", dayIndex));
				listViewItem.Tag = season;
			}
			if (this.SeasonList.Items.Count == 0)
			{
				ListViewItem grayText = this.SeasonList.Items.Add("(no seasons)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.SeasonList.Sort();
		}

		private class EventSorter : IComparer
		{
			private Masterplan.Data.Calendar fCalendar;

			public EventSorter(Masterplan.Data.Calendar c)
			{
				this.fCalendar = c;
			}

			public int Compare(object x, object y)
			{
				ListViewItem listViewItem = x as ListViewItem;
				ListViewItem listViewItem1 = y as ListViewItem;
				CalendarEvent tag = listViewItem.Tag as CalendarEvent;
				CalendarEvent calendarEvent = listViewItem1.Tag as CalendarEvent;
				if (tag == null || calendarEvent == null)
				{
					return 0;
				}
				MonthInfo monthInfo = this.fCalendar.FindMonth(tag.MonthID);
				MonthInfo monthInfo1 = this.fCalendar.FindMonth(calendarEvent.MonthID);
				int num = this.fCalendar.Months.IndexOf(monthInfo);
				int num1 = this.fCalendar.Months.IndexOf(monthInfo1);
				int num2 = num.CompareTo(num1);
				if (num2 == 0)
				{
					num2 = tag.DayIndex.CompareTo(calendarEvent.DayIndex);
				}
				return num2;
			}
		}
	}
}
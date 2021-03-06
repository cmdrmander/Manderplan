using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Controls;

namespace Masterplan.UI
{
	internal class TrapCountermeasureForm : Form
	{
		private Button CancelBtn;

		private Button OKBtn;

		private TabControl Pages;

		private TabPage DetailsPage;

		private DefaultTextBox DetailsBox;

		private TabPage AdvicePage;

		private ListView AdviceList;

		private ColumnHeader AdviceHdr;

		private ColumnHeader InfoHdr;

		private int fLevel = 1;

		public string Countermeasure
		{
			get
			{
				if (this.DetailsBox.Text == this.DetailsBox.DefaultText)
				{
					return "";
				}
				return this.DetailsBox.Text;
			}
		}

		public TrapCountermeasureForm(string cm, int level)
		{
			this.InitializeComponent();
			this.DetailsBox.Text = cm;
			this.fLevel = level;
			this.update_advice();
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Skill DCs", HorizontalAlignment.Left);
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new DefaultTextBox();
			this.AdvicePage = new TabPage();
			this.AdviceList = new ListView();
			this.AdviceHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.AdvicePage.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(306, 201);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(225, 201);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.AdvicePage);
			this.Pages.Location = new Point(12, 12);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(369, 183);
			this.Pages.TabIndex = 2;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(361, 157);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.DefaultText = "(enter countermeasure details)";
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new System.Drawing.Size(355, 151);
			this.DetailsBox.TabIndex = 0;
			this.DetailsBox.Text = "(enter countermeasure details)";
			this.AdvicePage.Controls.Add(this.AdviceList);
			this.AdvicePage.Location = new Point(4, 22);
			this.AdvicePage.Name = "AdvicePage";
			this.AdvicePage.Padding = new System.Windows.Forms.Padding(3);
			this.AdvicePage.Size = new System.Drawing.Size(361, 157);
			this.AdvicePage.TabIndex = 1;
			this.AdvicePage.Text = "Advice";
			this.AdvicePage.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columns = this.AdviceList.Columns;
			ColumnHeader[] adviceHdr = new ColumnHeader[] { this.AdviceHdr, this.InfoHdr };
			columns.AddRange(adviceHdr);
			this.AdviceList.Dock = DockStyle.Fill;
			this.AdviceList.FullRowSelect = true;
			listViewGroup.Header = "Skill DCs";
			listViewGroup.Name = "listViewGroup1";
			this.AdviceList.Groups.AddRange(new ListViewGroup[] { listViewGroup });
			this.AdviceList.HeaderStyle = ColumnHeaderStyle.None;
			this.AdviceList.HideSelection = false;
			this.AdviceList.Location = new Point(3, 3);
			this.AdviceList.MultiSelect = false;
			this.AdviceList.Name = "AdviceList";
			this.AdviceList.Size = new System.Drawing.Size(355, 151);
			this.AdviceList.TabIndex = 2;
			this.AdviceList.UseCompatibleStateImageBehavior = false;
			this.AdviceList.View = View.Details;
			this.AdviceHdr.Text = "Advice";
			this.AdviceHdr.Width = 150;
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 100;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(393, 236);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TrapCountermeasureForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Countermeasure";
			base.Shown += new EventHandler(this.TrapCountermeasureForm_Shown);
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.AdvicePage.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void TrapCountermeasureForm_Shown(object sender, EventArgs e)
		{
			this.DetailsBox.Focus();
			this.DetailsBox.SelectAll();
		}

		private void update_advice()
		{
			ListViewItem item = this.AdviceList.Items.Add("Skill DC (easy)");
			ListViewItem.ListViewSubItemCollection subItems = item.SubItems;
			int skillDC = AI.GetSkillDC(Difficulty.Easy, this.fLevel);
			subItems.Add(skillDC.ToString());
			item.Group = this.AdviceList.Groups[0];
			ListViewItem listViewItem = this.AdviceList.Items.Add("Skill DC (moderate)");
			ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
			int num = AI.GetSkillDC(Difficulty.Moderate, this.fLevel);
			listViewSubItemCollections.Add(num.ToString());
			listViewItem.Group = this.AdviceList.Groups[0];
			ListViewItem item1 = this.AdviceList.Items.Add("Skill DC (hard)");
			ListViewItem.ListViewSubItemCollection subItems1 = item1.SubItems;
			int skillDC1 = AI.GetSkillDC(Difficulty.Hard, this.fLevel);
			subItems1.Add(skillDC1.ToString());
			item1.Group = this.AdviceList.Groups[0];
		}
	}
}
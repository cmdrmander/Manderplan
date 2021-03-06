using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MagicItemSectionForm : Form
	{
		private MagicItemSection fSection;

		private Label HeaderLbl;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Button OKBtn;

		private Button CancelBtn;

		private ComboBox HeaderBox;

		public MagicItemSection Section
		{
			get
			{
				return this.fSection;
			}
		}

		public MagicItemSectionForm(MagicItemSection section)
		{
			this.InitializeComponent();
			this.HeaderBox.Items.Add("Price");
			this.HeaderBox.Items.Add("Enhancement");
			this.HeaderBox.Items.Add("Property");
			this.HeaderBox.Items.Add("Power");
			this.fSection = section.Copy();
			this.HeaderBox.Text = this.fSection.Header;
			this.DetailsBox.Text = this.fSection.Details;
		}

		private void InitializeComponent()
		{
			this.HeaderLbl = new Label();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.HeaderBox = new ComboBox();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			base.SuspendLayout();
			this.HeaderLbl.AutoSize = true;
			this.HeaderLbl.Location = new Point(12, 15);
			this.HeaderLbl.Name = "HeaderLbl";
			this.HeaderLbl.Size = new System.Drawing.Size(45, 13);
			this.HeaderLbl.TabIndex = 0;
			this.HeaderLbl.Text = "Header:";
			this.Pages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Location = new Point(12, 39);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(307, 146);
			this.Pages.TabIndex = 2;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new System.Windows.Forms.Padding(3);
			this.DetailsPage.Size = new System.Drawing.Size(299, 120);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new System.Drawing.Size(293, 114);
			this.DetailsBox.TabIndex = 0;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(163, 191);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(244, 191);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.HeaderBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.HeaderBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.HeaderBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.HeaderBox.FormattingEnabled = true;
			this.HeaderBox.Location = new Point(63, 12);
			this.HeaderBox.Name = "HeaderBox";
			this.HeaderBox.Size = new System.Drawing.Size(256, 21);
			this.HeaderBox.Sorted = true;
			this.HeaderBox.TabIndex = 1;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(331, 226);
			base.Controls.Add(this.HeaderBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.HeaderLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MagicItemSectionForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Magic Item Section";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fSection.Header = this.HeaderBox.Text;
			this.fSection.Details = this.DetailsBox.Text;
		}
	}
}
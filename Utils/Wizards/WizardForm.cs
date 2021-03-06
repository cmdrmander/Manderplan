using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Utils.Wizards
{
	internal class WizardForm : Form
	{
		private Button BackBtn;

		private Button NextBtn;

		private Button CancelBtn;

		private Panel ContentPnl;

		private Button FinishBtn;

		private PictureBox ImageBox;

		private Wizard fWizard;

		public IWizardPage CurrentPage
		{
			get
			{
				if (this.ContentPnl.Controls.Count == 0)
				{
					return null;
				}
				return this.ContentPnl.Controls[0] as IWizardPage;
			}
		}

		public WizardForm(Wizard wiz)
		{
			this.InitializeComponent();
			this.fWizard = wiz;
			this.Text = this.fWizard.Title;
			if (!this.fWizard.MaxSize.IsEmpty)
			{
				WizardForm wizardForm = this;
				int width = wizardForm.Width;
				System.Drawing.Size maxSize = this.fWizard.MaxSize;
				wizardForm.Width = width + (Math.Max(maxSize.Width, this.ContentPnl.Width) - this.ContentPnl.Width);
				WizardForm wizardForm1 = this;
				int height = wizardForm1.Height;
				System.Drawing.Size size = this.fWizard.MaxSize;
				wizardForm1.Height = height + (Math.Max(size.Height, this.ContentPnl.Height) - this.ContentPnl.Height);
				this.ImageBox.Height = this.ContentPnl.Height;
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			if (this.fWizard.Pages.Count != 0)
			{
				this.set_page(0);
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			IWizardPage currentPage = this.CurrentPage;
			if (currentPage != null)
			{
				this.BackBtn.Enabled = currentPage.AllowBack;
				this.NextBtn.Enabled = currentPage.AllowNext;
				this.FinishBtn.Enabled = currentPage.AllowFinish;
				if (currentPage.AllowFinish)
				{
					base.AcceptButton = this.FinishBtn;
					return;
				}
				if (currentPage.AllowNext)
				{
					base.AcceptButton = this.NextBtn;
					return;
				}
				base.AcceptButton = null;
			}
		}

		private void BackBtn_Click(object sender, EventArgs e)
		{
			if (this.CurrentPage != null)
			{
				if (!this.CurrentPage.AllowBack)
				{
					return;
				}
				if (!this.CurrentPage.OnBack())
				{
					return;
				}
				int num = this.fWizard.Pages.IndexOf(this.CurrentPage);
				int num1 = this.fWizard.BackPageIndex(num);
				if (num1 == -1)
				{
					num1 = num - 1;
				}
				this.set_page(num1);
			}
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			if (this.CurrentPage != null)
			{
				this.fWizard.OnCancel();
			}
			base.Close();
		}

		private void FinishBtn_Click(object sender, EventArgs e)
		{
			if (this.CurrentPage != null)
			{
				if (!this.CurrentPage.AllowFinish)
				{
					return;
				}
				if (!this.CurrentPage.OnFinish())
				{
					return;
				}
				this.fWizard.OnFinish();
				base.Close();
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WizardForm));
			this.BackBtn = new Button();
			this.NextBtn = new Button();
			this.CancelBtn = new Button();
			this.ContentPnl = new Panel();
			this.FinishBtn = new Button();
			this.ImageBox = new PictureBox();
			((ISupportInitialize)this.ImageBox).BeginInit();
			base.SuspendLayout();
			this.BackBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.BackBtn.Location = new Point(148, 277);
			this.BackBtn.Name = "BackBtn";
			this.BackBtn.Size = new System.Drawing.Size(75, 23);
			this.BackBtn.TabIndex = 1;
			this.BackBtn.Text = "< Back";
			this.BackBtn.UseVisualStyleBackColor = true;
			this.BackBtn.Click += new EventHandler(this.BackBtn_Click);
			this.NextBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.NextBtn.Location = new Point(229, 277);
			this.NextBtn.Name = "NextBtn";
			this.NextBtn.Size = new System.Drawing.Size(75, 23);
			this.NextBtn.TabIndex = 2;
			this.NextBtn.Text = "Next >";
			this.NextBtn.UseVisualStyleBackColor = true;
			this.NextBtn.Click += new EventHandler(this.NextBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(391, 277);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Click += new EventHandler(this.CancelBtn_Click);
			this.ContentPnl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.ContentPnl.Location = new Point(149, 12);
			this.ContentPnl.Name = "ContentPnl";
			this.ContentPnl.Size = new System.Drawing.Size(317, 259);
			this.ContentPnl.TabIndex = 0;
			this.FinishBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.FinishBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.FinishBtn.Location = new Point(310, 277);
			this.FinishBtn.Name = "FinishBtn";
			this.FinishBtn.Size = new System.Drawing.Size(75, 23);
			this.FinishBtn.TabIndex = 3;
			this.FinishBtn.Text = "Finish";
			this.FinishBtn.UseVisualStyleBackColor = true;
			this.FinishBtn.Click += new EventHandler(this.FinishBtn_Click);
			this.ImageBox.Image = (Image)componentResourceManager.GetObject("ImageBox.Image");
			this.ImageBox.Location = new Point(12, 12);
			this.ImageBox.Name = "ImageBox";
			this.ImageBox.Size = new System.Drawing.Size(131, 259);
			this.ImageBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.ImageBox.TabIndex = 13;
			this.ImageBox.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(478, 312);
			base.Controls.Add(this.ImageBox);
			base.Controls.Add(this.FinishBtn);
			base.Controls.Add(this.ContentPnl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.NextBtn);
			base.Controls.Add(this.BackBtn);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "WizardForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Wizard";
			((ISupportInitialize)this.ImageBox).EndInit();
			base.ResumeLayout(false);
		}

		private void NextBtn_Click(object sender, EventArgs e)
		{
			if (this.CurrentPage != null)
			{
				if (!this.CurrentPage.AllowNext)
				{
					return;
				}
				if (!this.CurrentPage.OnNext())
				{
					return;
				}
				int num = this.fWizard.Pages.IndexOf(this.CurrentPage);
				int num1 = this.fWizard.NextPageIndex(num);
				if (num1 == -1)
				{
					num1 = num + 1;
				}
				this.set_page(num1);
			}
		}

		private void set_page(int pageindex)
		{
			IWizardPage item = this.fWizard.Pages[pageindex];
			Control control = item as Control;
			if (control != null)
			{
				this.ContentPnl.Controls.Clear();
				this.ContentPnl.Controls.Add(control);
				control.Dock = DockStyle.Fill;
				item.OnShown(this.fWizard.Data);
			}
		}
	}
}
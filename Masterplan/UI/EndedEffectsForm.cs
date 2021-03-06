using Masterplan.Data;
using Masterplan.Commands;
using Masterplan.Commands.Combat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class EndedEffectsForm : Form
	{
		private Button OKBtn;

		private Label label1;

		private Panel EffectPanel;

		private ListView EffectList;

		private ToolStrip Toolbar;

		private ToolStripButton ExtendBtn;

		private ColumnHeader CreatureHdr;

		private ColumnHeader EffectHdr;

		private Button CancelBtn;

		private List<Pair<CombatData, OngoingCondition>> fEndedConditions;

		private List<Pair<CombatData, OngoingCondition>> fExtendedConditions;

		private Encounter fEncounter;

		public List<Pair<CombatData, OngoingCondition>> EndedConditions
		{
			get
			{
				return this.fEndedConditions;
			}
			set
			{
				this.fEndedConditions = value;
			}
		}

		public List<Pair<CombatData, OngoingCondition>> ExtendedConditions
		{
			get
			{
				return this.fExtendedConditions;
			}
			set
			{
				this.fExtendedConditions = value;
			}
		}

		public Pair<CombatData, OngoingCondition> SelectedCondition
		{
			get
			{
				if (this.EffectList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.EffectList.SelectedItems[0].Tag as Pair<CombatData, OngoingCondition>;
			}
		}

		public EndedEffectsForm(List<Pair<CombatData, OngoingCondition>> conditions, Encounter enc)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fEndedConditions = conditions;
			this.fExtendedConditions = new List<Pair<CombatData, OngoingCondition>>();
			this.fEncounter = enc;
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ExtendBtn.Enabled = this.SelectedCondition != null;
			if (this.SelectedCondition != null)
			{
				if (this.fEndedConditions.Contains(this.SelectedCondition))
				{
					this.ExtendBtn.Text = "Extend this effect";
					return;
				}
				this.ExtendBtn.Text = "End this effect";
			}
		}

		private void ExtendBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCondition == null)
			{
				return;
			}
			if (this.fEndedConditions.Contains(this.SelectedCondition))
			{
				this.fEndedConditions.Remove(this.SelectedCondition);
				this.fExtendedConditions.Add(this.SelectedCondition);
			}
			else if (this.fExtendedConditions.Contains(this.SelectedCondition))
			{
				this.fExtendedConditions.Remove(this.SelectedCondition);
				this.fEndedConditions.Add(this.SelectedCondition);
			}
			this.update_list();
		}

		private void InitializeComponent()
		{
			ListViewGroup listViewGroup = new ListViewGroup("Ended effects", HorizontalAlignment.Left);
			ListViewGroup listViewGroup1 = new ListViewGroup("These effects will not be ended this turn", HorizontalAlignment.Left);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(EndedEffectsForm));
			this.OKBtn = new Button();
			this.label1 = new Label();
			this.EffectPanel = new Panel();
			this.EffectList = new ListView();
			this.CreatureHdr = new ColumnHeader();
			this.EffectHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.ExtendBtn = new ToolStripButton();
			this.CancelBtn = new Button();
			this.EffectPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(360, 261);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 8;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(504, 33);
			this.label1.TabIndex = 9;
			this.label1.Text = "The following ongoing effects are due to end now. If you want to extend one for another turn, select it and press the Extend button.";
			this.EffectPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.EffectPanel.Controls.Add(this.EffectList);
			this.EffectPanel.Controls.Add(this.Toolbar);
			this.EffectPanel.Location = new Point(12, 45);
			this.EffectPanel.Name = "EffectPanel";
			this.EffectPanel.Size = new System.Drawing.Size(504, 210);
			this.EffectPanel.TabIndex = 10;
			ListView.ColumnHeaderCollection columns = this.EffectList.Columns;
			ColumnHeader[] creatureHdr = new ColumnHeader[] { this.CreatureHdr, this.EffectHdr };
			columns.AddRange(creatureHdr);
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			listViewGroup.Header = "Ended effects";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup1.Header = "These effects will not be ended this turn";
			listViewGroup1.Name = "listViewGroup2";
			this.EffectList.Groups.AddRange(new ListViewGroup[] { listViewGroup, listViewGroup1 });
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(0, 25);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new System.Drawing.Size(504, 185);
			this.EffectList.TabIndex = 1;
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Details;
			this.CreatureHdr.Text = "Affected Creature";
			this.CreatureHdr.Width = 150;
			this.EffectHdr.Text = "Effect";
			this.EffectHdr.Width = 323;
			this.Toolbar.Items.AddRange(new ToolStripItem[] { this.ExtendBtn });
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(504, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.ExtendBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ExtendBtn.Image = (Image)componentResourceManager.GetObject("ExtendBtn.Image");
			this.ExtendBtn.ImageTransparentColor = Color.Magenta;
			this.ExtendBtn.Name = "ExtendBtn";
			this.ExtendBtn.Size = new System.Drawing.Size(101, 22);
			this.ExtendBtn.Text = "Extend this effect";
			this.ExtendBtn.Click += new EventHandler(this.ExtendBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(441, 261);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 11;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(528, 296);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.EffectPanel);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EndedEffectsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Ended Effects";
			this.EffectPanel.ResumeLayout(false);
			this.EffectPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			foreach (Pair<CombatData, OngoingCondition> fEndedCondition in this.fEndedConditions)
			{
                CommandManager.GetInstance().ExecuteCommand(new RemoveEffectCommand(fEndedCondition.First, fEndedCondition.Second));
			}
		}

		private void update_list()
		{
			this.EffectList.Items.Clear();
			foreach (Pair<CombatData, OngoingCondition> fEndedCondition in this.fEndedConditions)
			{
				ListViewItem item = this.EffectList.Items.Add(fEndedCondition.First.ToString());
				item.SubItems.Add(fEndedCondition.Second.ToString(this.fEncounter, false));
				item.Group = this.EffectList.Groups[0];
				item.Tag = fEndedCondition;
			}
			if (this.fEndedConditions.Count == 0)
			{
				ListViewItem grayText = this.EffectList.Items.Add("(none)");
				grayText.Group = this.EffectList.Groups[0];
				grayText.ForeColor = SystemColors.GrayText;
			}
			foreach (Pair<CombatData, OngoingCondition> fExtendedCondition in this.fExtendedConditions)
			{
				ListViewItem listViewItem = this.EffectList.Items.Add(fExtendedCondition.First.ToString());
				listViewItem.SubItems.Add(fExtendedCondition.Second.ToString(this.fEncounter, false));
				listViewItem.Group = this.EffectList.Groups[1];
				listViewItem.Tag = fExtendedCondition;
			}
			if (this.fExtendedConditions.Count == 0)
			{
				ListViewItem item1 = this.EffectList.Items.Add("(none)");
				item1.Group = this.EffectList.Groups[1];
				item1.ForeColor = SystemColors.GrayText;
			}
		}
	}
}
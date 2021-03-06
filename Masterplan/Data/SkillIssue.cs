using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class SkillIssue : IIssue
	{
		private PlotPoint fPoint;

		public PlotPoint Point
		{
			get
			{
				return this.fPoint;
			}
		}

		public string Reason
		{
			get
			{
				if (this.fPoint.State != PlotPointState.Normal)
				{
					return "";
				}
				SkillChallenge element = this.fPoint.Element as SkillChallenge;
				if (element == null)
				{
					return "";
				}
				if (element.Skills.Count == 0)
				{
					return "No skills are defined for this skill challenge.";
				}
				return "";
			}
		}

		public SkillIssue(PlotPoint point)
		{
			this.fPoint = point;
		}

		public override string ToString()
		{
			return string.Concat(this.fPoint.Name, ": ", this.Reason);
		}
	}
}
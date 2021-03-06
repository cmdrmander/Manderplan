using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncyclopediaLink
	{
		private List<Guid> fIDs = new List<Guid>();

		public List<Guid> EntryIDs
		{
			get
			{
				return this.fIDs;
			}
			set
			{
				this.fIDs = value;
			}
		}

		public EncyclopediaLink()
		{
		}

		public EncyclopediaLink Copy()
		{
			EncyclopediaLink encyclopediaLink = new EncyclopediaLink();
			foreach (Guid fID in this.fIDs)
			{
				encyclopediaLink.EntryIDs.Add(fID);
			}
			return encyclopediaLink;
		}
	}
}
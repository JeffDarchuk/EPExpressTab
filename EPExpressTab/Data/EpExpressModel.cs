using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPExpressTab.Data.Interface;
using Sitecore.Analytics.Tracking;

namespace EPExpressTab.Data
{
	public abstract class EpExpressModel : IEpExpressModel
	{
		public abstract string RenderToString(Contact contact);
		public abstract string Heading { get; }
		public virtual bool UseDefaultWrapper => true;
		public abstract string TabLabel { get; }
	}
}

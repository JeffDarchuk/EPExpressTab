using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Analytics.Data.Bulk.Contact.Pipelines.BeforePersist;
using Sitecore.Analytics.Tracking;

namespace EPExpressTab.Data.Interface
{
	public interface IEpExpressModel
	{
		bool UseDefaultWrapper { get; }
		string Heading { get; }
		string TabLabel { get; }
		string RenderToString(Contact contact);

	}
}

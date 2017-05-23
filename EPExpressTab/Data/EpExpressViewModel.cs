using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Analytics.Tracking;

namespace EPExpressTab.Data
{
	public abstract class EpExpressViewModel : EpExpressModel
	{
		public sealed override string RenderToString(Contact contact)
		{
			var model = GetModel(contact);
			return ViewRenderer.RenderView(GetFullViewPath(model), model);
		}

		public abstract object GetModel(Contact contact);
		public abstract string GetFullViewPath(object model);
	}
}

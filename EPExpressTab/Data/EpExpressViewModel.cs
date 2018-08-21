using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPExpressTab.Data
{
	public abstract class EpExpressViewModel : EpExpressModel
	{
		public sealed override string RenderToString(Guid contactId)
		{
			var model = GetModel(contactId);
			return ViewRenderer.RenderView(GetFullViewPath(model), model);
		}

		public abstract object GetModel(Guid contactId);
		public abstract string GetFullViewPath(object model);
	}
}

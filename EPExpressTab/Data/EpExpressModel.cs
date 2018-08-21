using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPExpressTab.Data.Interface;

namespace EPExpressTab.Data
{
	public abstract class EpExpressModel : IEpExpressModel
	{
		public abstract string RenderToString(Guid contactId);
		public abstract string Heading { get; }
		public virtual bool UseDefaultWrapper => true;
		public abstract string TabLabel { get; }
	}
}

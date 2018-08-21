using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPExpressTab.Data.Interface
{
	public interface IEpExpressModel
	{
		bool UseDefaultWrapper { get; }
		string Heading { get; }
		string TabLabel { get; }
		string RenderToString(Guid contactId);

	}
}

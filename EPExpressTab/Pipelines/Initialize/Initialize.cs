using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EPExpressTab.Data;
using EPExpressTab.Data.Interface;
using EPExpressTab.Handlers;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Pipelines;
using Sitecore.SecurityModel;

namespace EPExpressTab.Pipelines.Initialize
{
	public class Initialize
	{
		public void Process(PipelineArgs args)
		{
			Database db = Factory.GetDatabase("master", false);
			if (db == null)
				return;
			Database core = Factory.GetDatabase("core", false);
			if (core == null)
				return;
			EpContext.Tabs = AppDomain.CurrentDomain.GetAssemblies().SelectMany(GetEpTabs).Select(t => (EpExpressModel)Activator.CreateInstance(t)).ToDictionary(x => x.GetType().AssemblyQualifiedName);
			using (new SecurityDisabler())
			{
				Item tabs = core.GetItem(Constants.EpTabsFolder);
				Item rendering = core.GetItem(Constants.EpExpressRendering);
				if (rendering == null)
				{
					Item renderingsContainer = core.GetItem(Constants.ContainersRenderingFolder);
					rendering = core.DataManager.DataEngine.CreateItem("EpExpress", renderingsContainer,
						new ID(Constants.EpExpressRenderingTemplate), new ID(Constants.EpExpressRendering));
					rendering.Editing.BeginEdit();
					rendering["Method"] = "Render";
					rendering["Class"] = "EPExpressTab.Handlers.EpExpressTabRenderer";
					rendering["Assembly"] = "EpExpressTab";
					rendering.Editing.EndEdit();
				}
				Dictionary<string, Item> foundTabs = new Dictionary<string, Item>();
				foreach (Item tab in tabs.Children)
				{
					if (tab["Placeholder Name"].StartsWith("epe"))
						foundTabs.Add(tab["Placeholder Name"].Substring(3), tab);
				}

				foreach (string key in EpContext.Tabs.Keys)
				{
					Item tab;
					if (foundTabs.ContainsKey(key))
					{
						tab = foundTabs[key];
						foundTabs.Remove(key);
					}
					else
					{
						tab = tabs.Add(ItemUtil.ProposeValidItemName(key), new TemplateID(new ID(Constants.EpTabTemplate)));
					}
					ValidateTab(tab, EpContext.Tabs[key], key);
				}
				foreach (Item old in foundTabs.Values)
					old.Recycle();
			}
		}
		private void ValidateTab(Item tabItem, IEpExpressModel tab, string key)
		{
			if (tabItem[FieldIDs.DisplayName] == tab.TabLabel &&
				tabItem["Placeholder Name"] == $"epe{key}" &&
				tabItem[FieldIDs.LayoutField] == GetLayoutXml(key)
			)
				return;
			tabItem.Editing.BeginEdit();
			tabItem[FieldIDs.DisplayName] = tab.TabLabel;
			tabItem["Placeholder Name"] = $"epe{key}";
			tabItem[FieldIDs.LayoutField] = GetLayoutXml(key);
			tabItem.Editing.EndEdit();
		}
		private string GetLayoutXml(string key)
		{
			return $@"<r>
				<d id=""{{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}}"">
					<r id=""{{F6C9F461-FCAF-47DC-AA01-C8C64C2EFFB8}}"" par=""Id={key}"" uid=""{ID.NewID}"" />
					<r id=""{Constants.EpExpressRendering}"" ph=""{key}.Content"" uid=""{ID.NewID}"" />
				</d>
			</r>";
		}

		private IEnumerable<Type> GetEpTabs(Assembly a)
		{
			IEnumerable<Type> types = null;
			try
			{
				types = a.GetTypes().Where(t => t.IsSubclassOf(typeof(EpExpressModel)) && !t.IsAbstract);
			}
			catch (ReflectionTypeLoadException e)
			{
				types = e.Types.Where(t => t != null && t.IsSubclassOf(typeof(EpExpressModel)) && !t.IsAbstract);
			}
			foreach (var type in types)
				yield return type;
		}
	}
}

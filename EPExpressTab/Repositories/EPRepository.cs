using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Diagnostics;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;

namespace EPExpressTab.Repositories
{
    public static class EPRepository
    {
        /// <summary>
        /// Gets the Contact and its specified facets
        /// </summary>
        /// <param name="contactId">Contact's GUID</param>
        /// <param name="facets">Facets that should be loaded with contact</param>
        /// <returns>Sitecore.XConnect.Contact</returns>
        public static Contact GetContact(Guid contactId, params string[] facets)
        {
            Assert.IsNotNull(contactId, "contactId is null");

            Contact contact = null;

            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                try
                {
                    contact = client.Get<Contact>(new ContactReference(contactId), new ContactExpandOptions(facets));
                }
                catch (XdbExecutionException ex)
                {
                    Log.Error($"Was not able load the contact with the Id of {contactId.ToString()}", ex, typeof(EPRepository));
                }
            }

            return contact;
        }
    }
}

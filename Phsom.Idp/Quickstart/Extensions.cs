namespace IdentityServer4.Quickstart.UI
{
    using System.Threading.Tasks;

    using IdentityServer4.Stores;

    public static class Extensions
    {
        /// <summary>
        /// Determines whether the client is configured to use PKCE.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string clientId)
        {
            if (!string.IsNullOrWhiteSpace(clientId))
            {
                var client = await store.FindEnabledClientByIdAsync(clientId).ConfigureAwait(false);
                return client?.RequirePkce == true;
            }

            return false;
        }
    }
}

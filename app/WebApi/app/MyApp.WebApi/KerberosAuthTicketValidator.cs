using Kerberos.NET;
using Kerberos.NET.Crypto;
using Kerberos.NET.Entities;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyApp.WebApi
{
    public class KerberosAuthTicketValidator
    {
        public async Task<ClaimsIdentity> IsValid(string ticket, string keytabPath)
        {
            if (!string.IsNullOrEmpty(keytabPath) || !string.IsNullOrEmpty(ticket))
            {
                var kerberosAuth = new KerberosAuthenticator(new KeyTable(File.ReadAllBytes(keytabPath))) { UserNameFormat = UserNameFormat.DownLevelLogonName };

                Console.WriteLine("keytab key auth");
                var identity = await kerberosAuth.Authenticate(ticket);
                PrintClaims(identity);

                return identity;
            }
            return null;
        }

        private void PrintClaims(ClaimsIdentity claimsIdentity)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(claimsIdentity.Name));
            foreach (var claim in claimsIdentity.Claims)
            {
                Console.WriteLine(claim.Value);
                Console.WriteLine(claim.ValueType);
            }
            Console.WriteLine(new string('-', 50));
        }
    }
}

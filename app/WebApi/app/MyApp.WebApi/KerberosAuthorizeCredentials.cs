namespace MyApp.WebApi
{
    public class KerberosAuthorizeCredentials
    {
        public string Ticket { get; }

        public KerberosAuthorizeCredentials(string ticket)
        {
            Ticket = ticket;
        }
    }
}

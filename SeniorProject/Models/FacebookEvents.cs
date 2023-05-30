namespace SeniorProject
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.OAuth;

    public class FacebookEvents : OAuthEvents
    {
        public Func<OAuthCreatingTicketContext, Task> OnCreatingTicket { get; set; } = context => Task.CompletedTask;

        public override Task CreatingTicket(OAuthCreatingTicketContext context) => OnCreatingTicket(context);
    }
}
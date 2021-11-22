using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Filters.V2
{
    public class Ticket_EnsureDescriptionPresentActionFilterAttribute: ActionFilterAttribute
    {
        public override  void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        var ticket=context.ActionArguments["ticket"] as Ticket;
            if(ticket is not null && !ticket.ValidateDescription())
            {
                context.ModelState.AddModelError("description", "Description is required");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }              
        }
    }
}

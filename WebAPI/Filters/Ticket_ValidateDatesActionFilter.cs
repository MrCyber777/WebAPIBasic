using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
//namespace WebAPI.Filters
//{
//    public class Ticket_ValidateDatesActionFilter:ActionFilterAttribute
//    {
//        //public override void OnActionExecuting(ActionExecutingContext context)
//        //{
//        //    base.OnActionExecuting(context);
//        //    var ticket = context.ActionArguments["ticket"] as Ticket;
//        //    if(ticket is not null && !string.IsNullOrWhiteSpace(ticket.Owner))
//        //    {
//        //        bool isValid = true;
//        //        if (!ticket.EnteredDate.HasValue)
//        //        {
//        //            context.ModelState.AddModelError("EnteredDate", "Entered date is required");
//        //            isValid = false;
//        //        }
//        //        if (ticket.EnteredDate.HasValue && ticket.EventDate.HasValue && ticket.EnteredDate > ticket.EventDate)
//        //        {
//        //            context.ModelState.AddModelError("EventDate", "Event date has to be later then the entered date");
//        //            isValid = false;
//        //        }
//        //        if(!isValid)                  
//        //            context.Result = new BadRequestObjectResult(context.ModelState);
//        //    }

//        //}
//    }
//}

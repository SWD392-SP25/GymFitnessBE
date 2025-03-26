using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class UserSubscriptionBuyDto
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string SubscriptionPlanName { get; set; }
    }
}

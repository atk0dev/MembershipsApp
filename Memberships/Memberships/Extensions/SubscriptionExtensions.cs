using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Extensions
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Memberships.Entities;
    using Memberships.Models;

    public static class SubscriptionExtensions
    {
        public static async Task<int> GetSubscriptionIdByRegistrationCode(this IDbSet<Subscription> subscription, string code)
        {
            try
            {
                if (subscription == null || string.IsNullOrWhiteSpace(code))
                {
                    return int.MinValue;
                }

                var subscriptionId = await (from s in subscription
                    where s.RegistrationCode.Equals(code)
                    select s.Id).FirstOrDefaultAsync();

                return subscriptionId;
            }
            catch (Exception)
            {
                return int.MinValue;
            }
        }

        public static async Task Register(
            this IDbSet<UserSubscription> userSubscription,
            int subscriptionId,
            string userId)
        {
            if (userSubscription == null || subscriptionId == int.MinValue || string.IsNullOrWhiteSpace(userId))
            {
                return;
            }

            try
            {
            var exist = await Task.Run<int>(() => userSubscription.CountAsync(s => 
                            s.SubscriptionId.Equals(subscriptionId) && s.UserId.Equals(userId))) > 0;
            if (!exist)
            {
                await Task.Run(() => userSubscription.Add(new UserSubscription
                                                          {
                                                              SubscriptionId = subscriptionId,
                                                              UserId = userId,
                                                              StartDate = DateTime.Now,
                                                              EndDate = DateTime.MaxValue
                                                          }));
            }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static async Task<bool> RegisterUserSubscriptionCode(string userId, string code)
        {
            try
            {
                var db = ApplicationDbContext.Create();
                var id = await db.Subscriptions.GetSubscriptionIdByRegistrationCode(code);
                if (id <= 0)
                {
                    return false;
                }

                await db.UserSubscriptions.Register(id, userId);

                if (db.ChangeTracker.HasChanges())
                {
                    await db.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
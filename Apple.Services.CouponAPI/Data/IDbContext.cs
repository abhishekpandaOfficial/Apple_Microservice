using Apple.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Apple.Services.CouponAPI.Data
{
    public interface IAppDbContext
    {
        DbSet<Coupon> Coupons { get; set; }

        

         Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
          int SaveChanges(CancellationToken cancellationToken = default);
    }
}


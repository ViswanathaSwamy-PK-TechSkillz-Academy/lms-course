using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.IdentityPersistence;

public class LMSIdentityDbContext(DbContextOptions<LMSIdentityDbContext> options) : IdentityDbContext(options)
{
}

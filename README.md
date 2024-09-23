# Learning Management System

I am learning ASP.NET Core MVC Application from different Video Courses, Books, and Websites.

## Reference(s)

> 1. <https://www.udemy.com/course/complete-aspnet-core-31-and-entity-framework-development/learn/lecture/17700164#overview>

## Few Commands

```powershell
dotnet tool update --global dotnet-ef

C:\> CD D:\TSA\lms-course\src\LMS.Web

# Add migration for LMSIdentityDbContext
dotnet ef migrations add CreateIdentitySchema --project ../LMS.IdentityPersistence --startup-project . --context LMSIdentityDbContext
dotnet ef migrations add SeedingDefaultRolesAndUser --project ../LMS.IdentityPersistence --startup-project . --context LMSIdentityDbContext
dotnet ef migrations add ExtendedUseTable --project ../LMS.IdentityPersistence --startup-project . --context LMSIdentityDbContext
dotnet ef migrations add ApplicationUserToDbo --project ../LMS.IdentityPersistence --startup-project . --context LMSIdentityDbContext

# Update database for LMSIdentityDbContext
dotnet ef database update --context LMSIdentityDbContext

C:\> CD D:\TSA\lms-course\src\LMS.Web>

# Add migration for LMSDbContext
dotnet ef migrations add LmsAddingLeaveTypeTable --project ../LMS.Persistence --startup-project . --context LMSDbContext
dotnet ef migrations add LmsChangingTheSchema --project ../LMS.Persistence --startup-project . --context LMSDbContext
dotnet ef migrations add LmsAddedLeaveAllocation --project ../LMS.Persistence --startup-project . --context LMSDbContext

# Update database for LMSDbContext
dotnet ef database update --context LMSDbContext
```

## To do List

> 1. 
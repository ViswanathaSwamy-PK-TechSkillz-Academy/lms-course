# Learning Management System

I am learning ASP.NET Core MVC Application from different Video Courses, Books, and Websites.

## Few Commands

```powershell
dotnet tool update --global dotnet-ef

C:\> CD D:\TSA\lms-course\src\LMS.Web

# Add migration for LMSIdentityDbContext
dotnet ef migrations add CreateIdentitySchema --project ../LMS.IdentityPersistence --startup-project . --context LMSIdentityDbContext
dotnet ef migrations add SeedingDefaultRolesAndUser --project ../LMS.IdentityPersistence --startup-project . --context LMSIdentityDbContext
dotnet ef migrations add ExtendedUseTable --project ../LMS.IdentityPersistence --startup-project . --context LMSIdentityDbContext

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
# Learning Management System

I am learning ASP.NET Core MVC Application from different Video Courses, Books, and Websites.

```powershell
dotnet tool update --global dotnet-ef

C:\> CD D:\TSA\lms-course\src\LMS.Web

# Add migration for LMSIdentityDbContext
dotnet ef migrations add CreateIdentitySchema --project ../LMS.IdentityPersistence --startup-project . --context LMSIdentityDbContext

# Update database for LMSIdentityDbContext
dotnet ef database update --context LMSIdentityDbContext

C:\> CD D:\TSA\lms-course\src\LMS.Web>

# Add migration for LMSDbContext
dotnet ef migrations add AddingLeaveTypeTable --project ../LMS.Persistence --startup-project . --context LMSDbContext
dotnet ef migrations add ChangingTheSchema --project ../LMS.Persistence --startup-project . --context LMSDbContext
dotnet ef migrations add ChangingTheSchemaV1 --project ../LMS.Persistence --startup-project . --context LMSDbContext

# Update database for LMSDbContext
dotnet ef database update --context LMSDbContext
```


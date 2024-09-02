# Learning Management System

I am learning ASP.NET Core MVC Application from different Video Courses, Books, and Websites.

```powershell
dotnet tool update --global dotnet-ef

# D:\TSA\lms-course\src\LMS.Web>
dotnet ef migrations add CreateIdentitySchema --project ../LMS.IdentityPersistence --startup-project . --context LMSIdentityDbContext
dotnet ef database update --context LMSIdentityDbContext

# D:\TSA\lms-course\src\LMS.Web>
dotnet ef migrations add AddingLeaveTypeTable --project ../LMS.Persistence --startup-project . --context LMSDbContext
dotnet ef database update --context LMSDbContext
```

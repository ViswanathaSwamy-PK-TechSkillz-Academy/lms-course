using AutoMapper;
using LMS.Data.Entities;
using LMS.Persistence;
using LMS.Web.Controllers;
using LMS.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Services.LeaveTypes;

public class LeaveTypesService(LMSDbContext lmsDbContext, ILogger<LeaveTypesController> logger, IMapper mapper) : ILeaveTypesService
{

    public async Task<IEnumerable<LeaveTypeReadOnlyVM>> GetAllAsync()
    {
        logger.LogInformation("LeaveTypesService::GetAllAsync() visited at {time}", DateTime.Now);

        List<LeaveType> leaveTypes = await lmsDbContext.LeaveTypes.ToListAsync();

        IEnumerable<LeaveTypeReadOnlyVM> leaveTypesVM = mapper.Map<IEnumerable<LeaveTypeReadOnlyVM>>(leaveTypes);

        return leaveTypesVM;
    }

    public async Task<T?> GetAsync<T>(int id) where T : class
    {
        logger.LogInformation("LeaveTypesService::GetAsync<T>() visited at {time}", DateTime.Now);

        LeaveType? leaveType = await lmsDbContext.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (leaveType == null)
        {
            return null;
        }

        T? leaveTypeData = mapper.Map<T>(leaveType);

        return leaveTypeData;
    }

    public async Task RemoveAsync(int id)
    {
        logger.LogInformation("LeaveTypesService::RemoveAsync() visited at {time}", DateTime.Now);

        LeaveType? leaveType = await lmsDbContext.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (leaveType != null)
        {
            lmsDbContext.Remove(leaveType);

            await lmsDbContext.SaveChangesAsync();
        }
    }

    public async Task EditAsync(LeaveTypeEditVM model)
    {
        logger.LogInformation("LeaveTypesService::EditAsync() visited at {time}", DateTime.Now);

        LeaveType? leaveType = mapper.Map<LeaveType>(model);

        lmsDbContext.Update(leaveType);
        await lmsDbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(LeaveTypeCreateVM model)
    {
        logger.LogInformation("LeaveTypesService::Create() visited at {time}", DateTime.Now);

        LeaveType? leaveType = mapper.Map<LeaveType>(model);

        lmsDbContext.Add(leaveType);
        await lmsDbContext.SaveChangesAsync();
    }

    public bool LeaveTypeExists(int id)
    {
        logger.LogInformation("LeaveTypesService::LeaveTypeExists(int id) visited at {time}", DateTime.Now);

        return lmsDbContext.LeaveTypes.Any(e => e.Id == id);
    }

    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        logger.LogInformation("LeaveTypesService::CheckIfLeaveTypeNameExists(string name) visited at {time}", DateTime.Now);

        return await lmsDbContext.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(name.ToLower()));
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
    {
        logger.LogInformation("LeaveTypesService::CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit) visited at {time}", DateTime.Now);

        return await lmsDbContext.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(leaveTypeEdit.Name.ToLower()) && q.Id != leaveTypeEdit.Id);
    }

    public async Task<bool> DaysExceedMaximum(int leaveTypeId, int days)
    {
        logger.LogInformation("LeaveTypesService::DaysExceedMaximum(int leaveTypeId, int days) visited at {time}", DateTime.Now);

        LeaveType? leaveType = await lmsDbContext.LeaveTypes.FindAsync(leaveTypeId);

        return leaveType?.NumberOfDays < days;
    }

}

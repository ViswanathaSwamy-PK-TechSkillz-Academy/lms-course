using AutoMapper;
using LMS.Data.Entities;
using LMS.Persistence;
using LMS.Web.Controllers;
using LMS.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Services;

public class LeaveTypesService(LMSDbContext lmsDbContext, ILogger<LeaveTypesController> logger, IMapper mapper)
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
        logger.LogInformation("LeaveTypesService::GetAsync<T>(int id) visited at {time}", DateTime.Now);

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
        logger.LogInformation("LeaveTypesService::RemoveAsync(int id) visited at {time}", DateTime.Now);

        LeaveType? leaveType = await lmsDbContext.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (leaveType != null)
        {
            lmsDbContext.Remove(leaveType);

            await lmsDbContext.SaveChangesAsync();
        }
    }

}

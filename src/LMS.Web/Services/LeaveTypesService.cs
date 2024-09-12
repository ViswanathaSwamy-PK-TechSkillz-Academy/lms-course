using AutoMapper;
using LMS.Data.Entities;
using LMS.Persistence;
using LMS.Web.Controllers;
using LMS.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Services;

public class LeaveTypesService(LMSDbContext lmsDbContext, ILogger<LeaveTypesController> logger, IMapper mapper)
{

    public async Task<IEnumerable<LeaveTypeReadOnlyVM>> GetAll()
    {
        logger.LogInformation("LeaveTypesService::GetAll() visited at {time}", DateTime.Now);

        List<LeaveType> leaveTypes = await lmsDbContext.LeaveTypes.ToListAsync();

        IEnumerable<LeaveTypeReadOnlyVM> leaveTypesVM = mapper.Map<IEnumerable<LeaveTypeReadOnlyVM>>(leaveTypes);

        return leaveTypesVM;
    }

}

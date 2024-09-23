using LMS.Web.Models.LeaveTypes;

namespace LMS.Web.Services.LeaveTypes;

public interface ILeaveTypesService
{
    Task<bool> CheckIfLeaveTypeNameExists(string name);

    Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit);

    Task CreateAsync(LeaveTypeCreateVM model);

    Task<bool> DaysExceedMaximum(int leaveTypeId, int days);

    Task EditAsync(LeaveTypeEditVM model);

    Task<IEnumerable<LeaveTypeReadOnlyVM>> GetAllAsync();

    Task<T?> GetAsync<T>(int id) where T : class;

    bool LeaveTypeExists(int id);

    Task RemoveAsync(int id);
}
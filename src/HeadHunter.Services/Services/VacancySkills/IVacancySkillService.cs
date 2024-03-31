using HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;

namespace HeadHunter.Services.Services.VacancySkills;

public interface IVacancySkillService
{
    /// <summary>
    /// Asynchronously creates a new vacancy skill based on the provided vacancy skill creation model.
    /// </summary>
    /// <param name="vacancySkill">The model containing information for creating the vacancy skill.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `VacancySkillViewModel` representing the newly created vacancy skill.</returns>
    Task<VacancySkillViewModel> CreateAsync(VacancySkillCreateModel vacancySkill);

    /// <summary>
    /// Asynchronously updates an existing vacancy skill identified by its ID, based on the provided vacancy skill update model.
    /// </summary>
    /// <param name="id">The unique identifier of the vacancy skill to be updated.</param>
    /// <param name="vacancySkill">The model containing updated information for the vacancy skill.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `VacancySkillViewModel` representing the updated vacancy skill.</returns>
    Task<VacancySkillViewModel> UpdateAsync(long id, VacancySkillUpdateModel vacancySkill);

    /// <summary>
    /// Asynchronously deletes a vacancy skill by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the vacancy skill to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves a vacancy skill by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the vacancy skill to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `VacancySkillViewModel` representing the retrieved vacancy skill.</returns>
    Task<VacancySkillViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all vacancy skills.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `VacancySkillViewModel`, representing all vacancy skills in the system.</returns>
    Task<IEnumerable<VacancySkillViewModel>> GetAllAsync();

}

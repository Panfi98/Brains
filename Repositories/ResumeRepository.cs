using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class ResumeRepository(DataContext context, IMapper mapper)
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        
        public async Task<Resume> AddResume(Resume resume, int userId)
        {
            try
            {
                if (resume == null)
                    throw new ArgumentNullException(nameof(resume), "Resume cannot be null");

                if (userId <= 0)
                    throw new ArgumentException("Invalid user ID", nameof(userId));

                var personExists = await _context.User.AnyAsync(u => u.Id == userId);
                
                if (!personExists)
                    throw new KeyNotFoundException($"User with ID {userId} not found");

                resume.UserId = userId;
                
                var resumeTemplate = await _context.ResumeTemplate.FirstOrDefaultAsync();
               
                if (resumeTemplate == null)
                    throw new KeyNotFoundException("No resume templates found in database");

                resume.ResumeTemplateId = resumeTemplate.Id;
                
                await _context.Resume.AddAsync(resume);
                await _context.SaveChangesAsync();
                
                return resume;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while adding resume", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding resume", ex);
            }
        }
        
        public async Task<Education> AddEducation(Education education, int resumeId)
        {
            try
            {
                if (education == null)
                {
                    throw new ArgumentNullException(nameof(education), "Education cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

                if (!resumeExists)
                {
                    throw new KeyNotFoundException($"Resume with ID {resumeId} not found");
                }

                education.ResumeId = resumeId;
                
                await _context.Education.AddAsync(education);
                await _context.SaveChangesAsync();
                
                return education;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while adding education", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding education", ex);
            }
        }
        
        public async Task<Certification> AddCertification(Certification certification, int resumeId)
        {
            try
            {
                if (certification == null)
                {
                    throw new ArgumentNullException(nameof(certification), "Certification cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

                if (!resumeExists)
                {
                    throw new KeyNotFoundException($"Resume with ID {resumeId} not found");
                }

                certification.ResumeId = resumeId;
                
                await _context.Certification.AddAsync(certification);
                await _context.SaveChangesAsync();
                
                return certification;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while adding certification", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding certification", ex);
            }
        }
        
        public async Task<Experience> AddExperience(Experience experience, int resumeId)
        {
            try
            {
                if (experience == null)
                {
                    throw new ArgumentNullException(nameof(experience), "Experience cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

                if (!resumeExists)
                {
                    throw new KeyNotFoundException($"Resume with ID {resumeId} not found");
                }

                experience.ResumeId = resumeId;
                
                await _context.Experience.AddAsync(experience);
                await _context.SaveChangesAsync();
                
                return experience;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while adding experience", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding experience", ex);
            }
        }
        
        public async Task<Project> AddProject(Project project, int resumeId)
        {
            try
            {
                if (project == null)
                    throw new ArgumentNullException(nameof(project), "Project cannot be null");

                if (resumeId <= 0)
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));

                var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

                if (!resumeExists)
                    throw new KeyNotFoundException($"Resume with ID {resumeId} not found");

                project.ResumeId = resumeId;
                
                await _context.Project.AddAsync(project);
                await _context.SaveChangesAsync();
                
                return project;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while adding project", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding project", ex);
            }
        }

        public async Task<InfoSkill> AddSkill(InfoSkill skill, int resumeId)
        {
            try
            {
                if (skill == null)
                {
                    throw new ArgumentNullException(nameof(skill), "Skill cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

                if (!resumeExists)
                {
                    throw new KeyNotFoundException($"Resume with ID {resumeId} not found");
                }

                skill.ResumeId = resumeId;
                
                await _context.InfoSkill.AddAsync(skill);
                await _context.SaveChangesAsync();
                
                return skill;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while adding skill", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding skill", ex);
            }
        }
        
        public async Task<Reference> AddReference(Reference reference, int resumeId)
        {
            try
            {
                if (reference == null)
                {
                    throw new ArgumentNullException(nameof(reference), "Reference cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

                if (!resumeExists)
                {
                    throw new KeyNotFoundException($"Resume with ID {resumeId} not found");
                }

                reference.ResumeId = resumeId;
                
                await _context.Reference.AddAsync(reference);
                await _context.SaveChangesAsync();
                
                return reference;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while adding reference", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding reference", ex);
            }
        }

        public async Task<List<Resume>> GetAllResumesByUserID(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    throw new ArgumentException("Invalid user ID", nameof(userId));
                }

                return await _context.Resume
                    .AsNoTracking()
                    .Where(r => r.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving resumes", ex);
            }
        }
        
        public async Task<GetFullResumesDTO> GetFullResume(int resumeId)
        {
            try
            {
                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var resume = await _context.Resume
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.Id == resumeId);

                if (resume == null)
                {
                    throw new KeyNotFoundException($"Resume with ID {resumeId} not found");
                }
                
                var educations = await _context.Education
                    .Where(e => e.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();

                var experiences = await _context.Experience
                    .Where(e => e.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();

                var skills = await _context.InfoSkill
                    .Where(s => s.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();

                var projects = await _context.Project
                    .Where(p => p.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();

                var certifications = await _context.Certification
                    .Where(c => c.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();
                
                return new GetFullResumesDTO
                {
                    Resume = _mapper.Map<GetResumeDTO>(resume),
                    Educations = _mapper.Map<List<PostEducationDTO>>(educations),
                    Experiences = _mapper.Map<List<PostExperienceDTO>>(experiences),
                    InfoSkills = _mapper.Map<List<PostInfoSkillDTO>>(skills),
                    Projects = _mapper.Map<List<PostProjectDTO>>(projects),
                    Certifications = _mapper.Map<List<PostCertificationDTO>>(certifications)
                };
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping error occurred while getting full resume", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting full resume", ex);
            }
        }
        
        public async Task<List<Education>> GetEducationsByResumeId(int resumeId)
        {
            try
            {
                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                return await _context.Education
                    .Where(e => e.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving educations", ex);
            }
        }
        
        public async Task<List<Certification>> GetCertificationsByResumeId(int resumeId)
        {
            try
            {
                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                return await _context.Certification
                    .Where(e => e.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving certifications", ex);
            }
        }
        
        public async Task<List<Project>> GetProjectsByResumeId(int resumeId)
        {
            try
            {
                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                return await _context.Project
                    .Where(e => e.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving projects", ex);
            }
        }
        
        public async Task<List<Experience>> GetExperiencesByResumeId(int resumeId)
        {
            try
            {
                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                return await _context.Experience
                    .Where(e => e.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving experiences", ex);
            }
        }
        
        public async Task<List<InfoSkill>> GetInfoSkillsByResumeId(int resumeId)
        {
            try
            {
                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                return await _context.InfoSkill
                    .Where(e => e.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving skills", ex);
            }
        }
        
        public async Task<List<Reference>> GetReferencesByResumeId(int resumeId)
        {
            try
            {
                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                return await _context.Reference
                    .Where(e => e.ResumeId == resumeId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving references", ex);
            }
        }
        
        public async Task<Resume?> UpdateResumeById(int id, Resume resume)
        {
            try
            {
                if (resume == null)
                {
                    throw new ArgumentNullException(nameof(resume), "Resume cannot be null");
                }

                if (id <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(id));
                }

                var oldResume = await _context.Resume.FindAsync(id);

                if (oldResume == null)
                {
                    throw new KeyNotFoundException($"Resume with ID {id} not found");
                }
                
                oldResume.FirstName = resume.FirstName;
                oldResume.LastName = resume.LastName;
                oldResume.Email = resume.Email;
                oldResume.PhoneNumber = resume.PhoneNumber;
                oldResume.Address = resume.Address;
                oldResume.BirthDate = resume.BirthDate;
                oldResume.PictureURL = resume.PictureURL;
                oldResume.Summary = resume.Summary;
                oldResume.updatedAt = DateTime.UtcNow;
                
                _context.Resume.Update(oldResume);
                await _context.SaveChangesAsync();
                return oldResume;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while updating resume", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating resume", ex);
            }
        }
        
        public async Task<Education?> UpdateEducationByResumeId(int resumeId, Education education)
        {
            try
            {
                if (education == null)
                {
                    throw new ArgumentNullException(nameof(education), "Education cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var oldEducation = await _context.Education.FindAsync(resumeId);

                if (oldEducation == null)
                {
                    throw new KeyNotFoundException($"Education for resume ID {resumeId} not found");
                }
                
                oldEducation.Name = education.Name;
                oldEducation.Type = education.Type;
                oldEducation.StartDate = education.StartDate;
                oldEducation.EndDate = education.EndDate;
                oldEducation.Description = education.Description;
                oldEducation.Degree = education.Degree;
                oldEducation.Place = education.Place;
                oldEducation.Active = education.Active;
                oldEducation.updatedAt = DateTime.UtcNow;
                
                _context.Education.Update(oldEducation);
                await _context.SaveChangesAsync();
                return oldEducation;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while updating education", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating education", ex);
            }
        }
        
        public async Task<Certification?> UpdateCertificationsByResumeId(int resumeId, Certification certification)
        {
            try
            {
                if (certification == null)
                {
                    throw new ArgumentNullException(nameof(certification), "Certification cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var oldCertification = await _context.Certification.FindAsync(resumeId);

                if (oldCertification == null)
                {
                    throw new KeyNotFoundException($"Certification for resume ID {resumeId} not found");
                }
                
                oldCertification.Name = certification.Name;
                oldCertification.Description = certification.Description;
                oldCertification.Date = certification.Date;
                oldCertification.Type = certification.Type;
                oldCertification.Url = certification.Url;
                oldCertification.ValidTo = certification.ValidTo;
                oldCertification.updatedAt = DateTime.UtcNow;
                
                _context.Certification.Update(oldCertification);
                await _context.SaveChangesAsync();
                return oldCertification;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while updating certification", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating certification", ex);
            }
        }
        
        public async Task<Project?> UpdateProjectsByResumeId(int resumeId, Project project)
        {
            try
            {
                if (project == null)
                {
                    throw new ArgumentNullException(nameof(project), "Project cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var oldProject = await _context.Project.FindAsync(resumeId);

                if (oldProject == null)
                {
                    throw new KeyNotFoundException($"Project for resume ID {resumeId} not found");
                }
                
                oldProject.Name = project.Name;
                oldProject.Description = project.Description;
                oldProject.StartDate = project.StartDate;
                oldProject.EndDate = project.EndDate;
                oldProject.Completed = project.Completed;
                oldProject.updatedAt = DateTime.UtcNow;
                
                _context.Project.Update(oldProject);
                await _context.SaveChangesAsync();
                return oldProject;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while updating project", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating project", ex);
            }
        }
        
        public async Task<Experience?> UpdateExperiencesByResumeId(int resumeId, Experience experience)
        {
            try
            {
                if (experience == null)
                {
                    throw new ArgumentNullException(nameof(experience), "Experience cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var oldExperience = await _context.Experience.FindAsync(resumeId);

                if (oldExperience == null)
                {
                    throw new KeyNotFoundException($"Experience for resume ID {resumeId} not found");
                }
                
                oldExperience.Name = experience.Name;
                oldExperience.Organisation = experience.Organisation;
                oldExperience.Type = experience.Type;
                oldExperience.Position = experience.Position;
                oldExperience.Description = experience.Description;
                oldExperience.StartedAt = experience.StartedAt;
                oldExperience.EndedAt = experience.EndedAt;
                oldExperience.Active = experience.Active;
                oldExperience.updatedAt = DateTime.UtcNow;
                
                _context.Experience.Update(oldExperience);
                await _context.SaveChangesAsync();
                return oldExperience;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while updating experience", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating experience", ex);
            }
        }
        
        public async Task<InfoSkill?> UpdateInfoSkillsByResumeId(int resumeId, InfoSkill infoSkill)
        {
            try
            {
                if (infoSkill == null)
                {
                    throw new ArgumentNullException(nameof(infoSkill), "InfoSkill cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var oldInfoSkill = await _context.InfoSkill.FindAsync(resumeId);

                if (oldInfoSkill == null)
                {
                    throw new KeyNotFoundException($"InfoSkill for resume ID {resumeId} not found");
                }
                
                oldInfoSkill.Name = infoSkill.Name;
                oldInfoSkill.Description = infoSkill.Description;
                oldInfoSkill.Type = infoSkill.Type;
                oldInfoSkill.Level = infoSkill.Level;
                oldInfoSkill.updatedAt = DateTime.UtcNow;
                
                _context.InfoSkill.Update(oldInfoSkill);
                await _context.SaveChangesAsync();
                return oldInfoSkill;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while updating skill", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating skill", ex);
            }
        }
        
        public async Task<Reference?> UpdateReferencesByResumeId(int resumeId, Reference reference)
        {
            try
            {
                if (reference == null)
                {
                    throw new ArgumentNullException(nameof(reference), "Reference cannot be null");
                }

                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var oldReference = await _context.Reference.FindAsync(resumeId);

                if (oldReference == null)
                {
                    throw new KeyNotFoundException($"Reference for resume ID {resumeId} not found");
                }
                
                oldReference.FirstName = reference.FirstName;
                oldReference.LastName = reference.LastName;
                oldReference.Position = reference.Position;
                oldReference.Email = reference.Email;
                oldReference.PhoneNumber = reference.PhoneNumber;
                oldReference.updatedAt = DateTime.UtcNow;
                
                _context.Reference.Update(oldReference);
                await _context.SaveChangesAsync();
                return oldReference;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error occurred while updating reference", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating reference", ex);
            }
        }
        
        public async Task DeleteResumeWithRelatedData(int resumeId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
    
            try
            {
                if (resumeId <= 0)
                {
                    throw new ArgumentException("Invalid resume ID", nameof(resumeId));
                }

                var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);
                
                if (!resumeExists)
                {
                    throw new KeyNotFoundException($"Resume with ID {resumeId} not found");
                }

                await _context.Education
                    .Where(e => e.ResumeId == resumeId)
                    .ExecuteDeleteAsync();

                await _context.Experience
                    .Where(e => e.ResumeId == resumeId)
                    .ExecuteDeleteAsync();

                await _context.InfoSkill
                    .Where(s => s.ResumeId == resumeId)
                    .ExecuteDeleteAsync();

                await _context.Project
                    .Where(p => p.ResumeId == resumeId)
                    .ExecuteDeleteAsync();

                await _context.Certification
                    .Where(c => c.ResumeId == resumeId)
                    .ExecuteDeleteAsync();
                
                await _context.Resume
                    .Where(r => r.Id == resumeId)
                    .ExecuteDeleteAsync();
                
                await transaction.CommitAsync();
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Database error occurred while deleting resume", ex);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("An error occurred while deleting resume", ex);
            }
        }
    }
}
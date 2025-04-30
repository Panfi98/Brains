using System.Security.Claims;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BrainsToDo.Repositories
{
    public class ResumeRepository(DataContext context,  IMapper mapper)
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        
        public async Task<Resume> AddResume(Resume resume, int userId)
        {
            if (resume == null)
            {
                throw new ArgumentNullException(nameof(resume));
            }

            if (userId <= 0)
            {
                throw new ArgumentException("Invalid person", nameof(userId));
            }

            var personExists = await _context.User.AnyAsync(u => u.Id == userId);
            
            if (!personExists)
            {
                throw new KeyNotFoundException("User not found");
            }

            resume.UserId = userId;
            
            var ResumeTemplate = await _context.ResumeTemplate.FirstOrDefaultAsync();
           
            if (ResumeTemplate == null)
            {
                throw new KeyNotFoundException("Resume template not found");
            }
            resume.ResumeTemplateId = ResumeTemplate.Id;
            
            await _context.Resume.AddAsync(resume);
            await _context.SaveChangesAsync();
            
            return resume;
        }
        
        public async Task<Education> AddEducation(Education education, int resumeId)
        {
            if (education == null)
            {
                throw new ArgumentNullException(nameof(education));
            }
            
            if (resumeId <= 0)
            {
                throw new ArgumentException("Invalid resume", nameof(resumeId));
            }

            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);
            education.ResumeId = resumeId;
            
            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found");
            }
            
            await _context.Education.AddAsync(education);
            await _context.SaveChangesAsync();
            
            return education;
        }
        
        public async Task<Certification> AddCertification(Certification certification, int resumeId)
        {
            if (certification == null)
            {
                throw new ArgumentNullException(nameof(certification));
            }

            if (resumeId <= 0)
            {
                throw new ArgumentException("Invalid resume", nameof(resumeId));
            }

            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found");
            }

            certification.ResumeId = resumeId;
            
            await _context.Certification.AddAsync(certification);
            await _context.SaveChangesAsync();
            
            return certification;
        }
        
        public async Task<Experience> AddExperience(Experience experience, int resumeId)
        {
            if (experience == null)
            {
                throw new ArgumentNullException(nameof(experience));
            }

            if (resumeId <= 0)
            {
                throw new ArgumentException("Invalid resume", nameof(resumeId));
            }

            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found");
            }

            experience.ResumeId = resumeId;
            
            await _context.Experience.AddAsync(experience);
            await _context.SaveChangesAsync();
            
            return experience;
        }
        
        public async Task<Project> AddProject(Project project, int resumeId)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (resumeId <= 0)
            {
                throw new ArgumentException("Invalid resume", nameof(resumeId));
            }

            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found");
            }

            project.ResumeId = resumeId;
            
            await _context.Project.AddAsync(project);
            await _context.SaveChangesAsync();
            
            return project;
        }

   
        public async Task<InfoSkill> AddSkill(InfoSkill skill, int resumeId)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }

            if (resumeId <= 0 )
            {
                throw new ArgumentException("Resume not found ");
            }

            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);
            

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found");
            }

            skill.ResumeId = resumeId;
            
            await _context.InfoSkill.AddAsync(skill);
            await _context.SaveChangesAsync();
            
            return skill;
        }
        
        public async Task<Reference> AddReference(Reference reference, int resumeId)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            if (resumeId <= 0)
            {
                throw new ArgumentException("Invalid resume", nameof(resumeId));
            }

            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found");
            }

            reference.ResumeId = resumeId;
            
            await _context.Reference.AddAsync(reference);
            await _context.SaveChangesAsync();
            
            return reference;
        }

        public async Task<List<Resume>> GetAllResumesByUserID(int userId)
        {
            return  await _context.Resume
                .AsNoTracking()
                .Where(r => r.UserId == userId).ToListAsync();
        }
        
        public async Task<GetFullResumesDTO> GetFullResume(int resumeId)
        {
            var resume = await _context.Resume
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == resumeId);

            if (resume == null)
                return null;
            
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
        
        public async Task<List<Education>> GetEducationsByResumeId(int resumeId)
        {
            return await _context.Education
                .Where(e => e.ResumeId == resumeId)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<List<Certification>> GetCertificationsByResumeId(int resumeId)
        {
            return await _context.Certification
                .Where(e => e.ResumeId == resumeId)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<List<Project>> GetProjectsByResumeId(int resumeId)
        {
            return await _context.Project
                .Where(e => e.ResumeId == resumeId)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<List<Experience>> GetExperiencesByResumeId(int resumeId)
        {
            return await _context.Experience
                .Where(e => e.ResumeId == resumeId)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<List<InfoSkill>> GetInfoSkillsByResumeId(int resumeId)
        {
            return await _context.InfoSkill
                .Where(e => e.ResumeId == resumeId)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<List<Reference>> GetReferencesByResumeId(int resumeId)
        {
            return await _context.Reference
                .Where(e => e.ResumeId == resumeId)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<Resume?> UpdateResumeById(int id, Resume resume)
        {
            var oldResume = await _context.Resume.FindAsync(id);
            if(oldResume == null)
            {
                throw new KeyNotFoundException("Resume not found");
            };
            
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
        
        public async Task<Education?> UpdateEducationByResumeId(int resumeId, Education education)
        {
            var oldEducation = await _context.Education.FindAsync(resumeId);
            if(oldEducation == null)
            {
                throw new KeyNotFoundException("Education not found");
            };
            
            oldEducation.Name = education.Name;
            oldEducation.Type = education.Type;
            oldEducation.StartDate = education.StartDate;
            oldEducation.EndDate = education.EndDate;
            oldEducation.Description = education.Description;
            oldEducation.Description =  education.Description;
            oldEducation.Degree = education.Degree;
            oldEducation.Place = education.Place;
            oldEducation.Active = education.Active;
            oldEducation.updatedAt = DateTime.UtcNow;
            
            _context.Education.Update(oldEducation);
            await _context.SaveChangesAsync();
            return oldEducation;
        }
        
        public async Task<Certification?> UpdateCertificationsByResumeId(int resumeId, Certification certification)
        {
            var oldCertification = await _context.Certification.FindAsync(resumeId);
            if(oldCertification == null)
            {
                throw new KeyNotFoundException("Certification not found");
            };
            
            oldCertification.Name = certification.Name;
            oldCertification.Description =  certification.Description;
            oldCertification.Date = certification.Date;
            oldCertification.Type = certification.Type;
            oldCertification.Url = certification.Url;
            oldCertification.ValidTo = certification.ValidTo;
            oldCertification.updatedAt = DateTime.UtcNow;
            
            _context.Certification.Update(oldCertification);
            await _context.SaveChangesAsync();
            return oldCertification;
        }
        
        public async Task<Project?> UpdateProjectsByResumeId(int resumeId, Project project)
        {
            var oldProject = await _context.Project.FindAsync(resumeId);
            if(oldProject == null)
            {
                throw new KeyNotFoundException("Project not found");
            };
            
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
        
        public async Task<Experience?> UpdateExperiencesByResumeId(int resumeId, Experience experience)
        {
            var oldExperience = await _context.Experience.FindAsync(resumeId);
            if(oldExperience == null)
            {
                throw new KeyNotFoundException("Experience not found");
            };
            
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
        
        public async Task<InfoSkill?> UpdateInfoSkillsByResumeId(int resumeId, InfoSkill infoSkill)
        {
            var oldInfoSkill = await _context.InfoSkill.FindAsync(resumeId);
            if(oldInfoSkill == null)
            {
                throw new KeyNotFoundException("InfoSkill not found");
            };
            
            oldInfoSkill.Name = infoSkill.Name;
            oldInfoSkill.Description =  infoSkill.Description;
            oldInfoSkill.Type = infoSkill.Type;
            oldInfoSkill.Level = infoSkill.Level;
            oldInfoSkill.updatedAt = DateTime.UtcNow;
            
            _context.InfoSkill.Update(oldInfoSkill);
            await _context.SaveChangesAsync();
            return oldInfoSkill;
        }
        
        public async Task<Reference?> UpdateReferencesByResumeId(int resumeId, Reference reference)
        {
            var oldReference = await _context.Reference.FindAsync(resumeId);
            if(oldReference == null)
            {
                throw new KeyNotFoundException("InfoSkill not found");
            };
            
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
        
        public async Task DeleteResumeWithRelatedData(int resumeId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
    
            try
            {
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
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
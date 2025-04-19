using System.Security.Claims;
using BrainsToDo.Data;
using BrainsToDo.Models;


namespace BrainsToDo.Repositories
{
    public class ResumeMakerRepository(DataContext context)
    {
        private readonly DataContext _context = context;
        
        public async Task<Resume> AddResume(Resume dto, int personId)
        {
            dto.PersonId = personId;
            dto.ResumeTemplateId = 1; 
            
            _context.Resume.Add(dto);
            await _context.SaveChangesAsync();
            return dto;;
        }
        
        public async Task<Education> AddEducation(Education dto, int personId)
        {
            dto.PersonId = personId;
            _context.Education.Add(dto);
            await _context.SaveChangesAsync();
            return  dto;;
        }

        public async Task<Certification> AddCertification(Certification dto, int resumeId)
        {
            dto.ResumeId = resumeId;
            _context.Certification.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<Experience> AddExperience(Experience dto, int resumeId)
        {
            dto.ResumeId = resumeId;
            _context.Experience.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<Project> AddProject(Project dto,  int resumeId)
        {
            dto.ResumeId = resumeId;
            _context.Project.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<Skill> AddSkill(Skill dto,int resumeId, int educationId, int experienceId, int projectId )
        {
            dto.ResumeId = resumeId;
            dto.EducationId = educationId;
            dto.ExperienceId = experienceId;
            dto.ProjectId = projectId;
            _context.Skill.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<Reference> AddReference(Reference dto, int resumeId)
        {
            dto.ResumeId = resumeId;
            _context.Reference.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }
        
    }
}
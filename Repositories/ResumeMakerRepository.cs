using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class ResumeMakerRepository(DataContext context) 
    {
        private readonly DataContext _context = context;
      
        public async Task<Resume> AddResume(Resume dto, int personId)
        {
            dto.PersonId = personId;
            dto.ResumeTemplateId = 1; 
            dto.createdAt = DateTime.UtcNow;
            dto.updatedAt = DateTime.UtcNow;
            
            _context.Resume.Add(dto);
            await _context.SaveChangesAsync();
            return await _context.Resume.Include(r => r.Person)
                .Include(r => r.ResumeTemplate)
                .FirstOrDefaultAsync(r => r.Id == dto.Id);;
        }
        
        public async Task<Education> AddEducationList(Education dto, int personId)
        {
            dto.PersonId = personId;
            dto.createdAt = DateTime.UtcNow;
            dto.updatedAt = DateTime.UtcNow;
            _context.Education.Add(dto);
            await _context.SaveChangesAsync();
            return  _context.Education.Include(e => e.Person).FirstOrDefault(e => e.Id == dto.Id);;
        }

        public async Task<Certification> AddCertifications(Certification dto, int resumeId)
        {
            dto.ResumeId = resumeId;
            dto.createdAt = DateTime.UtcNow;
            dto.updatedAt = DateTime.UtcNow;
            _context.Certification.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<Experience> AddExperienceList(Experience dto)
        {
            dto.ResumeId = dto.Resume.Id;
            dto.createdAt = DateTime.UtcNow;
            dto.updatedAt = DateTime.UtcNow;
            _context.Experience.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<Project> AddProjects(Project dto)
        {
            dto.ResumeId = dto.Resume.Id;
            dto.createdAt = DateTime.UtcNow;
            dto.updatedAt = DateTime.UtcNow;
            _context.Project.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<Skill> AddSkills(Skill dto)
        {
            dto.ResumeId = dto.Resume.Id;
            dto.createdAt = DateTime.UtcNow;
            dto.updatedAt = DateTime.UtcNow;
            _context.Skill.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<Reference> AddReferences(Reference dto)
        {
            dto.ResumeId = dto.Resume.Id;
            dto.createdAt = DateTime.UtcNow;
            dto.updatedAt = DateTime.UtcNow;
            _context.Reference.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }
        
    }
}
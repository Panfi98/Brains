using System.Security.Claims;
using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BrainsToDo.Repositories
{
    public class ResumeRepository(DataContext context)
    {
       
        private readonly DataContext _context = context;
        
        
        
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

            var personExists = await _context.Person.AnyAsync(p => p.Id == userId);
            
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

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found");
            }
            
            await _context.Education.AddAsync(education);
            await _context.SaveChangesAsync();
            
            return education;
        }
        
        public async Task<Certification> AddCertification(Certification dto, int resumeId)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
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

            dto.ResumeId = resumeId;
            
            await _context.Certification.AddAsync(dto);
            await _context.SaveChangesAsync();
            
            return dto;
        }
        
        public async Task<Experience> AddExperience(Experience dto, int resumeId)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
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

            dto.ResumeId = resumeId;
            
            await _context.Experience.AddAsync(dto);
            await _context.SaveChangesAsync();
            
            return dto;
        }
        
        public async Task<Project> AddProject(Project dto, int resumeId)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
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

            dto.ResumeId = resumeId;
            
            await _context.Project.AddAsync(dto);
            await _context.SaveChangesAsync();
            
            return dto;
        }

   
        public async Task<Skill> AddSkill(Skill dto, int resumeId, int educationId, int experienceId, int projectId)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (resumeId <= 0 || educationId <= 0 || experienceId <= 0 || projectId <= 0)
            {
                throw new ArgumentException("Invalid entity");
            }

            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId);
            var educationExists = await _context.Education.AnyAsync(e => e.Id == educationId);
            var experienceExists = await _context.Experience.AnyAsync(e => e.Id == experienceId);
            var projectExists = await _context.Project.AnyAsync(p => p.Id == projectId);

            if (!resumeExists || !educationExists || !experienceExists || !projectExists)
            {
                throw new KeyNotFoundException("One or more related entities not found");
            }

            dto.ResumeId = resumeId;
            dto.EducationId = educationId;
            dto.ExperienceId = experienceId;
            dto.ProjectId = projectId;
            
            await _context.Skill.AddAsync(dto);
            await _context.SaveChangesAsync();
            
            return dto;
        }
        
        public async Task<Reference> AddReference(Reference dto, int resumeId)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
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

            dto.ResumeId = resumeId;
            
            await _context.Reference.AddAsync(dto);
            await _context.SaveChangesAsync();
            
            return dto;
        }
    }
}
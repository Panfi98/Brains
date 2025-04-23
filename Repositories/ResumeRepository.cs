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

   
        public async Task<Skill> AddSkill(Skill skill, int resumeId, int educationId, int experienceId, int projectId)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
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

            skill.ResumeId = resumeId;
            skill.EducationId = educationId;
            skill.ExperienceId = experienceId;
            skill.ProjectId = projectId;
            
            await _context.Skill.AddAsync(skill);
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
    }
}
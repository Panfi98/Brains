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
    }
}
using BrainsToDo.Data;
using BrainsToDo.DTOModels;

namespace BrainsToDo.Repositories
{
    public class ResumeMakerRepository(DataContext context) : IResumeMaker<PostResumeForResumeMaker, PostEducationForResumeMaker, PostCertificationForResumeMaker, PostExperienceForResumeMaker,PostExperienceForResumeMaker, PostProjectForResumeMaker>
    {
        private readonly DataContext _context = context;

        public async Task<PostResumeForResumeMaker> AddResume(PostResumeForResumeMaker dto, int personId)
        {
            dto.PersonId = personId;
            _context.Resume.Add(dto);
            await _context.SaveChangesAsync();
           
            return dto;
        }
        
        public async Task<ResumeMakerDTO> AddEducationList(ResumeMakerDTO dto, int personId)
        {
            foreach (var edu in dto.EducationList)
            {
                edu.PersonId = personId;
                _context.Education.Add(edu);
            }
            
            return dto;
        }

        public async Task<ResumeMakerDTO> AddCertifications(ResumeMakerDTO dto)
        {
            foreach (var cert in dto.Certifications)
            {
                cert.ResumeId = dto.Resume.Id;
                _context.Certification.Add(cert);
            }
            
            return dto;
        }

        public async Task<ResumeMakerDTO> AddExperienceList(ResumeMakerDTO dto)
        {
            foreach (var exp in dto.ExperienceList)
            {
                exp.ResumeId = dto.Resume.Id;
                _context.Experience.Add(exp);
            }
            
            return dto;
        }

        public async Task<ResumeMakerDTO> AddProjects(ResumeMakerDTO dto)
        {
            foreach (var proj in dto.Projects)
            {
                proj.ResumeId = dto.Resume.Id;
                _context.Project.Add(proj);
            }
            
            return dto;
        }

        public async Task<ResumeMakerDTO> AddSkills(ResumeMakerDTO dto)
        {
            foreach (var skill in dto.Skills)
            {
                skill.ResumeId = dto.Resume.Id;
                _context.Skill.Add(skill);
            }
            
            return dto;
        }

        public async Task<ResumeMakerDTO> AddReferences(ResumeMakerDTO dto)
        {
            foreach (var reference in dto.References)
            {
                reference.ResumeId = dto.Resume.Id;
                _context.Reference.Add(reference);
            }
            
            return dto;
        }
    }
}
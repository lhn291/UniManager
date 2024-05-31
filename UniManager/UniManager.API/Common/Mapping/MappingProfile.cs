using AutoMapper;
using UniManager.Application.DTOs.Authentications;
using UniManager.Application.DTOs.Courses;
using UniManager.Application.DTOs.CourseStudents;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.DTOs.Students;
using UniManager.ApplicationCommon.DTOs.Students;
using UniManager.Domain.Entities;

namespace UniManager.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();

            CreateMap<Student, StudentRequestDto>().ReverseMap();

            CreateMap<Student, StudentByCourseDto>().ReverseMap();

            CreateMap<Student, StudentByEmailDto>().ReverseMap();

            CreateMap<Student, CreateStudentDto>().ReverseMap();

            CreateMap<CourseStudent, CourseStudentDto>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();

            CreateMap<Course, CreateCourseDto>().ReverseMap();

            CreateMap<Lecturer, LecturerByEmailDto>().ReverseMap();

            CreateMap<Lecturer, LecturerDto>().ReverseMap();

            CreateMap<Lecturer, CreateLecturerDto>().ReverseMap();
        }
    }
}

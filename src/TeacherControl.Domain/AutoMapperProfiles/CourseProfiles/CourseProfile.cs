﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDTO>()
                .ForMember(i => i.Professor, i => i.Ignore())
                .ForMember(i => i.Tags, i => i.MapFrom(src => src.Tags.Select(t => t.Name)));

            CreateMap<CourseDTO, Course>()
                .ForPath(i => i.Professor.Id, i => i.MapFrom(m => m.Professor))
                .ForMember(i => i.Tags, i => i.MapFrom(m => m.Tags.Select(t => new CourseTag { Name = t })));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HabitApp.Models.EntityLayer;

namespace HabitApp.ViewModels.Mappings
{
    public class DomainToViewModelMappingProfile:Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(vm => vm.Password, map => map.MapFrom(u => u.HashedPassword));
            CreateMap<Habit, HabitViewModel>();
            CreateMap<Answer, AnswerViewModel>();
            CreateMap<Question, QuestionViewModel>();
        }
    }
}

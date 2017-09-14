using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HabitApp.Models.EntityLayer;

namespace HabitApp.ViewModels.Mappings
{
    public class ViewModelToDomainMappingProfile:Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, User>()
                .ForMember(u => u.HashedPassword, map => map.MapFrom(vm => vm.Password));
            CreateMap<HabitViewModel, Habit>();
            CreateMap<AnswerViewModel, Answer>();
            CreateMap<QuestionViewModel, Question>();
        }
    }
}

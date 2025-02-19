using AutoMapper;
using OnlineQuizSystem.Dto_s;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserScoreDto, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<QuizDto, Quiz>().ReverseMap();
            CreateMap<CreateQuizDto, Quiz>().ReverseMap();
            CreateMap<QuizWithAnswersAndOptionsDto, Quiz>().ReverseMap();
            CreateMap<QuestionDto, QuizQuestion>().ReverseMap();
            CreateMap<CreateQuestionDto, QuizQuestion>().ReverseMap();
            CreateMap<CreateOptionDto, QuestionOption>().ReverseMap();
            CreateMap<OptionDto, QuestionOption>().ReverseMap();
            CreateMap<CreateUserAnswerDto, QuizUserAnswer>().ReverseMap();
            CreateMap<UpdateAnswerDto, QuizUserAnswer>().ReverseMap();
            CreateMap<QuestionWithOptionDto, QuizQuestion>().ReverseMap();
            CreateMap<UserAnswerDto, QuizUserAnswer>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(q => q.Question))
                .ForMember(dest => dest.QuestionOption, opt => opt.MapFrom(q => q.Option))
                .ReverseMap();
            CreateMap<SessionDto, Session>().ReverseMap();
         
        }
    }
}

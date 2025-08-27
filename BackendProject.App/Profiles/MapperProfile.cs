using AutoMapper;
using BackendProject.App.Models;
using BackendProject.App.ViewModels;

namespace BackendProject.App.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            //CreateMap<Source, Destination>();
            //CreateMap<Destination, Source>();
            // Example:
             CreateMap<Book, BookTestVm>()
                //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)) ozu basha dusur
                //.ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.BookImageUrls, opt => opt.MapFrom(src => src.BookImages.Select(bi => bi.ImageUrl).ToList()))
                 .ForMember(dest => dest.TagNames, opt => opt.MapFrom(src => src.BookTags.Select(bt => bt.Tag.Name).ToList()));
        }
    }
}

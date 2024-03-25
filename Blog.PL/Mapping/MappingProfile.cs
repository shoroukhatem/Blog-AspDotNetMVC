using AutoMapper;
using Blog.DAL.Entities;
using Blog.PL.Models;
using Blog.PL.Models.PostViewModels;

namespace Demo.PL.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<PostTagViewModel, Post>().ReverseMap();
            CreateMap<PostCreateViewModel, Post>().ReverseMap();
            CreateMap<PostViewModel, Post>().ReverseMap();
            CreateMap<DeletedPost, Post>().ReverseMap();
            CreateMap<DeletedPost, PostTagViewModel>().ReverseMap();
            CreateMap<TagViewModel,Tag>().ReverseMap();
            
        }
    }
}

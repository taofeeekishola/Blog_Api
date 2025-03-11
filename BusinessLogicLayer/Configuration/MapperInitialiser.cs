using AutoMapper;
using DomainLayer.Model;
using DomainLayer.PostDto;
using DomainLayer.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Configuration
{
    public class MapperInitialiser: Profile
    {
        public MapperInitialiser()
        {
            //mappers for users
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

            //mappers for posts
           
            CreateMap<Post, CreatePostDto>().ReverseMap();

            CreateMap<Post, UpdatePostDto>().ReverseMap();

            CreateMap<Post, PostDto>().ReverseMap();
        }
    }
}

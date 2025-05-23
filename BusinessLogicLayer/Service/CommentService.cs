﻿using BusinessLogicLayer.IServices;
using DataAccessLayer.UnitOfWorkFolder;
using DomainLayer.Model;


namespace BusinessLogicLayer.Service
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //function to create a comment
        public Comment? CreateComment(Comment comment, out string message)
        {
            //checking if the user enters values
            if (string.IsNullOrEmpty(comment.Content))
            {
                message = "Content can not be empty";
                return null;
            }
            if (string.IsNullOrEmpty(comment.UserId))
            {
                message = "User can not be empty";
                return null;
            }
            if (comment.PostId <= 0) 
            {
                message = "Post ID cannot be empty or zero";
                return null;
            }

            message = "Comment created successfully";
            return _unitOfWork.commentRepository.Create(comment);

        }

        //function to delete a comment
        public bool DeleteComment(int id, out string message)
        {
            //checking if the id is valid
            if (id <= 0)
            {
                message = "Invalid ID";
                return false;
            }

            Comment? comment = _unitOfWork.commentRepository.Get(id);

            //checking if comment exists
            if (comment == null)
            {
                message = "Comment not found";
                return false;
            }

            _unitOfWork.commentRepository.Delete(comment);

            message = "Comment successfully deleted";
            return true;
        }

        //function to get all comments
        public List<Comment> GetAllComments()
        {
            return _unitOfWork.commentRepository.GetAll();
        }

        //function to get a single comment
        public Comment? GetComment(int id)
        {
            //checking if the id is valid
            if (id <= 0)
            {
               
                return null;
            }

            Comment? comment = _unitOfWork.commentRepository.Get(id);

            //checking if comment exists
            if (comment == null)
            {
                return null;
            }

            return comment;
        }

        //function to update comments
        public Comment? UpdateComment(Comment comment, out string message)
        {
            //checking if the user enters values
            if (string.IsNullOrEmpty(comment.Content))
            {
                message = "Content can not be empty";
                return null;
            }

            Comment? updatedComment = _unitOfWork.commentRepository.Update(comment);

            //checking if post exists
            if (updatedComment is null)
            {
                message = "Comment does not exist";
                return null;
            }

            message = "Post updated successfully";
            return updatedComment;
        }
    }
}

using BusinessLogicLayer.IServices;
using DataAccessLayer.UnitOfWorkFolder;
using DomainLayer.Model;


namespace BusinessLogicLayer.Service
{
    public class LikeService : ILikeService
    {

        private readonly IUnitOfWork _unitOfWork;

        public LikeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //function to create a like
        public Like? CreateLike(Like like, out string message)
        {
            //checking if the user enters values
            if (string.IsNullOrEmpty(like.UserId))
            {
                message = "User can not be empty";
                return null;
            }
            if (like.PostId <= 0)
            {
                message = "Post ID cannot be empty or zero";
                return null;
            }

            message = "Like created successfully";
            return _unitOfWork.likeRepository.Create(like);
        }

        //function to delete a like
        public bool DeleteLike(int id, out string message)
        {
            //checking if the id is valid
            if (id <= 0)
            {
                message = "Invalid ID";
                return false;
            }

            Like? like = _unitOfWork.likeRepository.Get(id);

            //checking if comment exists
            if (like == null)
            {
                message = "Like not found";
                return false;
            }

            _unitOfWork.likeRepository.Delete(like);

            message = "Like successfully deleted";
            return true;

        }

        //function to get all likes
        public List<Like> GetAllLike()
        {
            return _unitOfWork.likeRepository.GetAll();
        }

        //function to get a single like
        public Like? GetLike(int id)
        {
            //checking if the id is valid
            if (id <= 0)
            {

                return null;
            }

            Like? like = _unitOfWork.likeRepository.Get(id);

            //checking if comment exists
            if (like == null)
            {
                return null;
            }

            return like;
        }
    }
}

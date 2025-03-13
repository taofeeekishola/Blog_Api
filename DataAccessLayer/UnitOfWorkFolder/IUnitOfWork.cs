using DataAccessLayer.Repository;


namespace DataAccessLayer.UnitOfWorkFolder
{
    public interface IUnitOfWork
    {
        UserRepository userRepository { get; }

        PostRepository postRepository { get; }

        CommentRepository commentRepository { get; }

        LikeRepository likeRepository { get;  }
    }
}

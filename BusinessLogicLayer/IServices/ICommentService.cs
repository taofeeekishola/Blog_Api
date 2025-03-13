using DomainLayer.Model;


namespace BusinessLogicLayer.IServices
{
    public interface ICommentService
    {
        /// <summary>
        /// Create a Comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>Object of Comment</returns>
        Comment? CreateComment(Comment comment, out string message);

        /// <summary>
        /// Get all Comments
        /// </summary>
        /// <returns>List of Comment</returns>
        List<Comment> GetAllComments();

        /// <summary>
        /// Get a comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object of comment</returns>
        Comment? GetComment(int id);

        /// <summary>
        /// Update Comment
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="message"></param>
        /// <returns>Object of Comment</returns>
        Comment? UpdateComment(Comment comment, out string message);

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Bolean true or false</returns>
        bool DeleteComment(int id, out string message);
    }
}

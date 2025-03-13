using DomainLayer.Model;


namespace BusinessLogicLayer.IServices
{
    public interface ILikeService
    {
        /// <summary>
        /// Create Like
        /// </summary>
        /// <param name="like"></param>
        /// <returns>Object of Like</returns>
        Like? CreateLike(Like like, out string message);

        /// <summary>
        /// Get all Likes
        /// </summary>
        /// <returns>Object of Like</returns>
        List<Like> GetAllLike();

        /// <summary>
        /// Get a single Like
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object of Like</returns>
        Like? GetLike(int id);

        /// <summary>
        /// Delete Like
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean true or false</returns>
        bool DeleteLike(int id, out string message);

        /// <summary>
        /// Update Like
        /// </summary>
        /// <param name="like"></param>
        /// <param name="message"></param>
        /// <returns>Object of Like</returns>
        //Like? UpdateLike(Like like, out string message);
    }
}

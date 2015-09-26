using System.Collections.Generic;
using HotCar.Entities;

namespace HotCar.Repositories.Abstract
{
    public interface ICommentRepository
    {
        List<Comment> GetCommentsDriversAboutPassengerByPassengerId(int id);
        List<Comment> GetCommentsPassengersAboutDriverByDriverId(int id);
    }
}

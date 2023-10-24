using Blog.CommentsService.Domain.Repositories;
using Blog.CommentsService.Domain.Users;
using Blog.Common.Domain.Repositories;
using Blog.Contracts.ApplicationUsers;
using Blog.Contracts.Posts;
using MassTransit;

namespace Blog.CommentsService.Application.Users.Created
{
    public class CommentsServiceUserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CommentsServiceUserCreatedConsumer(IUserRepository userRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _userRepository = userRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var user = new User
            {
                Id = UserId.Create(context.Message.UserId),
                UserName = context.Message.UserName
            };

            await _userRepository.CreateUserAsync(user);

            await unitOfWork.CommitAsync();
        }
    }
}

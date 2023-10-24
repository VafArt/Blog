using Blog.Common.Domain.Repositories;
using Blog.Contracts.ApplicationUsers;
using Blog.PostsService.Domain.Repositories;
using Blog.PostsService.Domain.Users;
using MassTransit;

namespace Blog.PostsService.Application.Users.Created
{
    public class PostsServiceUserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public PostsServiceUserCreatedConsumer(IUserRepository userRepository, IUnitOfWorkFactory unitOfWorkFactory)
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

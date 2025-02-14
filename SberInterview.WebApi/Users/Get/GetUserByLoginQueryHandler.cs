using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SberInterview.WebApi.Users.Get
{
    public class GetUserByLoginQueryHandler : IRequestHandler<GetUserByLoginQuery, User>
    {
        private readonly UsersRepository _usersRepository;
        private readonly ILogger<GetUserByLoginQueryHandler> _logger;

        public GetUserByLoginQueryHandler(UsersRepository usersRepository, ILoggerFactory loggerFactory)
        {
            _usersRepository = usersRepository;
            _logger = loggerFactory.CreateLogger<GetUserByLoginQueryHandler>();
        }

        public async Task<User> Handle(GetUserByLoginQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            try
            {
                var user = await _usersRepository.GetUserByLoginAsync(request.Login, cancellationToken);
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}